﻿namespace exercise.wwwapi.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }

        public ErrorMessage(string message)
        {
            Message = message;
        }
    }
}
