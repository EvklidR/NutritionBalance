﻿using System;
using System.Collections.Generic;

namespace AuthorisationService.Application.Exceptions
{
    public class BadRequest : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BadRequest(string message)
        {
            Errors = new List<string> { message };
        }

        public BadRequest(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
