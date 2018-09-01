﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure
{
    public class ErrorCodes
    {
        public static string InvalidCredentials => "invalid_credentials";
        public static string EmailInUse => "email_in_use";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidFirstName => "invalid_firstname";
        public static string InvalidLastName => "invalid_lastname";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidPassword => "invalid_password";
        public static string UserNotFound => "user_not_found";

    }
}
