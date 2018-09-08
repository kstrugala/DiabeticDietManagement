using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public PatientService(IUserService userService, IPatientRepository patientRepository, IUserRepository userRepository, IProductService productService,  IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _patientRepository = patientRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<PagedResult<PatientDto>> BrowseAsync(PatientQuery query)
        {
            var repositoryResults = await _patientRepository.GetPatientsAsync(query);
            var result = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientDto>>(repositoryResults.Results);

            foreach (var patient in result)
            {
                var user = await _userRepository.GetAsync(patient.Id);
                patient.Email = user.Email;
            }

            return new PagedResult<PatientDto>(result, repositoryResults.Pagination.TotalCount,
                                              repositoryResults.Pagination.CurrentPage, repositoryResults.Pagination.PageSize,
                                              repositoryResults.Pagination.TotalPages);
        }

        public async Task CreateAsync(CreatePatient patient)
        {
            Guid userID = Guid.NewGuid();

            if (String.IsNullOrWhiteSpace(patient.Email))
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"Email {patient.Email} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.FirstName))
            {
                throw new ServiceException(ErrorCodes.InvalidFirstName, $"First {patient.FirstName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.LastName))
            {
                throw new ServiceException(ErrorCodes.InvalidLastName, $"First {patient.LastName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.Username))
            {
                throw new ServiceException(ErrorCodes.InvalidUsername, $"First {patient.Username} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.Password))
            {
                throw new ServiceException(ErrorCodes.InvalidPassword, $"Password cannot be blank.");
            }


            await _userService.RegisterAsync(userID, patient.Email, patient.Username, patient.Password, "Patient");
            var user = await _userRepository.GetAsync(userID);
            var p = new Patient(user, patient.FirstName, patient.LastName);

            var plan = new MealPlan("Brak planu");
            var json = JsonConvert.SerializeObject(plan);
            p.SetRecommendedMealPlan(json);
            
            await _patientRepository.AddAsync(p);
        }

        public async Task<PatientDto> GetAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if (user != null)
            {
                var patient = await _patientRepository.GetAsync(user.Id);
                if (patient != null)
                {
                    var patientDto = _mapper.Map<Patient, PatientDto>(patient);
                    return _mapper.Map<UserDto, PatientDto>(user, patientDto);
                }
            }
            return null;
        }

        public async Task<PatientDto> GetAsync(Guid id)
        {
            var user = await _userService.GetAsync(id);

            if (user != null)
            {
                var patient = await _patientRepository.GetAsync(user.Id);
                if (patient != null)
                {
                    var patientDto = _mapper.Map<Patient, PatientDto>(patient);
                    return _mapper.Map<UserDto, PatientDto>(user, patientDto);
                }
            }
            return null;
        }

        
        public async Task RemoveAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if (user != null)
            {
                // Remove patient
                await _patientRepository.RemoveAsync(user.Id);
                // Remove user
                await _userService.RemoveAsync(user.Id);
            }
            else
                throw new ServiceException(ErrorCodes.InvalidEmail, $"User with email: {email} doesn't exist.");
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await _userService.GetAsync(id);

            if (user != null)
            {
                // Remove patient
                await _patientRepository.RemoveAsync(user.Id);
                // Remove user
                await _userService.RemoveAsync(user.Id);
            }
            else
                throw new ServiceException(ErrorCodes.InvalidEmail, $"User with id: {id} doesn't exist.");
        }

        public async Task UpdateAsync(UpdatePatient patient)
        {
            var user = await _userService.GetAsync(patient.Id);

            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with id: {patient.Id} doesn't exist.");
            }

            if (user.Role.Equals("Patient"))
            {
                var pac = await _patientRepository.GetAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(patient.FirstName))
                    pac.SetFirstName(patient.FirstName);
                if (!String.IsNullOrWhiteSpace(patient.LastName))
                    pac.SetLastName(patient.LastName);

                if (patient.Email != null)
                {
                    await _userService.ChangeEmail(user.Email, patient.Email);
                }
                await _patientRepository.UpdateAsync(pac);
            }
            else
                throw new ServiceException(ErrorCodes.UserNotFound, $"Patient with email:{patient.Email} doesn't exist.");
        }

        public async Task<MealPlanDto> GetMealPlanAsync(Guid id)
        {
            var patient = await _patientRepository.GetAsync(id);

            if (patient != null)
            {
                MealPlan plan = JsonConvert.DeserializeObject<MealPlan>(patient.RecommendedMealPlan);

                var planDto = new MealPlanDto { Name = plan.Name, DailyPlans = new HashSet<DailyPlanDto>() };

                foreach (var dp in plan.DailyPlans)
                {
                    var breakfast = new MealDto();
                    var snap = new MealDto();
                    var lunch = new MealDto();
                    var dinner = new MealDto();
                    var supper = new MealDto();

                    foreach (var p in dp.Breakfast.Products)
                    {
                        var product = await _productService.GetAsync(p.ProductId);
                        var portionDto = new PortionDto
                        {
                            Id = p.ProductId,
                            Quantity = p.Quantity,
                            ProductName = product.ProductName,
                            GlycemicIndex = product.GlycemicIndex,
                            GlycemicLoad = product.GlycemicLoad,
                            Carbohydrates = product.Carbohydrates,
                            ServeSize = product.ServeSize
                        };
                        breakfast.Products.Add(portionDto);
                    }
                    foreach (var p in dp.Snap.Products)
                    {
                        var product = await _productService.GetAsync(p.ProductId);
                        var portionDto = new PortionDto
                        {
                            Id = p.ProductId,
                            Quantity = p.Quantity,
                            ProductName = product.ProductName,
                            GlycemicIndex = product.GlycemicIndex,
                            GlycemicLoad = product.GlycemicLoad,
                            Carbohydrates = product.Carbohydrates,
                            ServeSize = product.ServeSize
                        };
                        snap.Products.Add(portionDto);
                    }
                    foreach (var p in dp.Lunch.Products)
                    {
                        var product = await _productService.GetAsync(p.ProductId);
                        var portionDto = new PortionDto
                        {
                            Id = p.ProductId,
                            Quantity = p.Quantity,
                            ProductName = product.ProductName,
                            GlycemicIndex = product.GlycemicIndex,
                            GlycemicLoad = product.GlycemicLoad,
                            Carbohydrates = product.Carbohydrates,
                            ServeSize = product.ServeSize
                        };
                        lunch.Products.Add(portionDto);
                    }
                    foreach (var p in dp.Dinner.Products)
                    {
                        var product = await _productService.GetAsync(p.ProductId);
                        var portionDto = new PortionDto
                        {
                            Id = p.ProductId,
                            Quantity = p.Quantity,
                            ProductName = product.ProductName,
                            GlycemicIndex = product.GlycemicIndex,
                            GlycemicLoad = product.GlycemicLoad,
                            Carbohydrates = product.Carbohydrates,
                            ServeSize = product.ServeSize
                        };
                        dinner.Products.Add(portionDto);
                    }
                    foreach (var p in dp.Supper.Products)
                    {
                        var product = await _productService.GetAsync(p.ProductId);
                        var portionDto = new PortionDto
                        {
                            Id = p.ProductId,
                            Quantity = p.Quantity,
                            ProductName = product.ProductName,
                            GlycemicIndex = product.GlycemicIndex,
                            GlycemicLoad = product.GlycemicLoad,
                            Carbohydrates = product.Carbohydrates,
                            ServeSize = product.ServeSize
                        };
                        supper.Products.Add(portionDto);
                    }

                    planDto.DailyPlans.Add(new DailyPlanDto { Day = dp.Day, Breakfast = breakfast, Snap = snap, Lunch = lunch, Dinner = dinner, Supper = supper });
                }

                return planDto;
            }
            else
                throw new ServiceException(ErrorCodes.InvalidId, $"Patient with id:{id} doesn't exist");
            
        }

        public async Task UpdateMealPlanAsync(UpdateRecommendedMealPlan command)
        {
            var patient = await _patientRepository.GetAsync(command.Id);

            if (patient != null)
            {
                var plan = new MealPlan(command.Name);
                
                if(command.DailyPlans.Count==2) // TODO change to 30
                {
                    foreach (var dp in command.DailyPlans)
                    {
                        var breakfast = new Core.Domain.Meal();
                        var snap = new Core.Domain.Meal();
                        var lunch = new Core.Domain.Meal();
                        var dinner = new Core.Domain.Meal();
                        var supper = new Core.Domain.Meal();

                        foreach (var p in dp.Breakfast.Products)
                        {
                            if(await _productService.GetAsync(p.ProductId)==null)
                            {
                                throw new  ServiceException(ErrorCodes.InvalidId, $"Product with id:{p.ProductId} doesn't exist.");
                            }

                            breakfast.Products.Add(new Portion { ProductId = p.ProductId, Quantity = p.Quantity });
                        }

                        foreach (var p in dp.Snap.Products)
                        {
                            if (await _productService.GetAsync(p.ProductId) == null)
                            {
                                throw new ServiceException(ErrorCodes.InvalidId, $"Product with id:{p.ProductId} doesn't exist.");
                            }
                            snap.Products.Add(new Portion { ProductId = p.ProductId, Quantity = p.Quantity });
                        }

                        foreach (var p in dp.Lunch.Products)
                        {
                            if (await _productService.GetAsync(p.ProductId) == null)
                            {
                                throw new ServiceException(ErrorCodes.InvalidId, $"Product with id:{p.ProductId} doesn't exist.");
                            }
                            lunch.Products.Add(new Portion { ProductId = p.ProductId, Quantity = p.Quantity });
                        }

                        foreach (var p in dp.Dinner.Products)
                        {
                            if (await _productService.GetAsync(p.ProductId) == null)
                            {
                                throw new ServiceException(ErrorCodes.InvalidId, $"Product with id:{p.ProductId} doesn't exist.");
                            }
                            dinner.Products.Add(new Portion { ProductId = p.ProductId, Quantity = p.Quantity });
                        }

                        foreach (var p in dp.Supper.Products)
                        {
                            if (await _productService.GetAsync(p.ProductId) == null)
                            {
                                throw new ServiceException(ErrorCodes.InvalidId, $"Product with id:{p.ProductId} doesn't exist.");
                            }
                            supper.Products.Add(new Portion { ProductId = p.ProductId, Quantity = p.Quantity });
                        }

                        plan.DailyPlans.Add(new DailyMealPlan { Day = dp.Day, Breakfast = breakfast, Snap = snap, Lunch = lunch, Dinner = dinner, Supper = supper });
                    }
                }

                string json = JsonConvert.SerializeObject(plan);
                patient.SetRecommendedMealPlan(json);
                await _patientRepository.UpdateAsync(patient);
            }
            else
                throw new ServiceException(ErrorCodes.InvalidId, $"Patient with id:{command.Id} doesn't exist");
        }
    }
}
