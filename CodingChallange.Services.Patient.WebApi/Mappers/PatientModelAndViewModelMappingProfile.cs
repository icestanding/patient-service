using AutoMapper;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;
using System.IO;
using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.ViewModels.Patient;

namespace CodingChallange.Services.Patient.WebApi.Mappers
{
    public class PatientModelAndViewModelMappingProfile: Profile
    {
        public PatientModelAndViewModelMappingProfile()
        {
            CreateMap<PatientRequestViewModel, PatientModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastNanme))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));


            CreateMap<PatientModel, PatientViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
            .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime));
        }

    }
}
