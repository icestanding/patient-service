using CodingChallange.Shared.Patient;
using Newtonsoft.Json;
using System;

namespace CodingChallange.Shared.ViewModels.Patient
{
    public class PatientRequestViewModel
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastNanme { get; set; }
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("email")]
        public String Email { get; set; }
        [JsonProperty("phone")]
        public String Phone { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }

}
