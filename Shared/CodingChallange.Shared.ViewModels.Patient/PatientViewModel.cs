﻿using CodingChallange.Shared.Patient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using CodingChallange.Shared.JsonConverter;

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
        [JsonConverter(typeof(DateOfBirthConverter))]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("email")]
        public String Email { get; set; }
        [JsonProperty("phone")]
        public String Phone { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime CreateTime { get; set; }
        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime UpdateTime { get; set; }

    }
}
