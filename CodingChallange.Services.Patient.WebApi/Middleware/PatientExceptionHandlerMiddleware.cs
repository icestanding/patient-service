using System.IO;
using System.Threading.Tasks;
using CodingChallange.Shared.ViewModels;
using CodingChallange.Shared.ViewModels.Patient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace CodingChallange.Services.Patient.WebApi.Middleware
{
    public class PatientExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public PatientExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException HttpStatusCodeException)
        {
            context.Response.StatusCode = HttpStatusCodeException.StatusCode;

            var errorMessage = new ExceptionViewModel()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = HttpStatusCodeException.ErrorMessage
            };
            var stringWriter = new StringWriter();
            using (JsonWriter textWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(textWriter, errorMessage);
                textWriter.Flush();
            }

            string jSerializeObjectstring = stringWriter.ToString();
            await context.Response.WriteAsync(jSerializeObjectstring);
        }

    }
}
