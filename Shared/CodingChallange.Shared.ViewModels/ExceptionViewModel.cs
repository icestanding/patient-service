using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallange.Shared.ViewModels.Patient
{
    public class ExceptionViewModel
    {
        public int StatusCode { set; get; }
        public string ErrorMessage { set; get; }

        public ExceptionViewModel() { }
        public ExceptionViewModel(int statusCode, string errorMessage) {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
