using AutoMapper;
using ResumeManagement_API.DTOs;
using ResumeManagement_API.Models;

namespace ResumeManagement_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<AddEditCandidateDto, Candidate>().ReverseMap();
            CreateMap<Candidate, Candidate>();

            CreateMap<CountryMaster, CountryDto>().ReverseMap();
            CreateMap<CountryDto, CountryDto>();
            CreateMap<CountryMaster, CountryMaster>();

            CreateMap<CityMaster, CityDto>().ReverseMap();
            CreateMap<CityMaster, CityMaster>();
            CreateMap<CityDto, CityDto>();

            CreateMap<StatusDto, StatusMaster>().ReverseMap();
            CreateMap<StatusDto, StatusDto>();
            CreateMap<StatusMaster, StatusMaster>();

        }
    }
}
