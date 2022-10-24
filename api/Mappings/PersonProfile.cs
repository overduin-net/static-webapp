public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));
        CreateMap<PersonDTO, Person>()
            .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id));
    }
}