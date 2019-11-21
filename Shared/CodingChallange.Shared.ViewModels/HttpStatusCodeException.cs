using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallange.Shared.ViewModels
{
    public class HttpStatusCodeException : Exception
    {

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public HttpStatusCodeException(int statusCode, string errorMessage) {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

    }
}
