﻿namespace UserProfileService.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BadRequestException(string message)
        {
            Errors = new List<string>() { message };
        }

        public BadRequestException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
