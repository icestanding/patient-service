using CodingChallange.Shared.Patient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingChallange.Shared.ViewModels.Patient
{
    public class PatientRequestViewModel
    {
        [Required]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [Required]
        [JsonProperty("last_name")]
        public string LastNanme { get; set; }
        
        [Required]
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        
        [Required]
        [JsonProperty("date_of_birth")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [JsonProperty("email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public String Email { get; set; }
        
        [JsonProperty("phone")]
        [DataType(DataType.PhoneNumber)]
        public String Phone { get; set; }
        
        [Required]
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }


    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
