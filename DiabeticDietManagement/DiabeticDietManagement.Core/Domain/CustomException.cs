﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public abstract class CustomException : Exception
    {
        public string Code { get; }

        protected CustomException()
        {
        }

        protected CustomException(string code)
        {
            Code = code;
        }

        protected CustomException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected CustomException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected CustomException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected CustomException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }

}
