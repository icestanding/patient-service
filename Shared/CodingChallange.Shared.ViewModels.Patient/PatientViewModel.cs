using CodingChallange.Shared.Patient;
using Newtonsoft.Json;
using System;


namespace CodingChallange.Shared.ViewModels.Patient
{
    public class PatientViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
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
        [JsonProperty("created_at")]
        public DateTime CreateTime { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdateTime { get; set; }

    }
}
