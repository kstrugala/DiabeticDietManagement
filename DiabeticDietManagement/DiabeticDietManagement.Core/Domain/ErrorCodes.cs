using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public static class ErrorCodes
    {
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidRole => "invalid_role";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidFirstName => "invalid_firstname";
        public static string InvalidLastName => "invalid_lastname";
        public static string InvalidProductName => "invalid_productname";
        public static string InvalidPortion => "invalid_portion";
        public static string InvalidDailyMealPlan => "invalid_DailyMealPlan";
    }
}
