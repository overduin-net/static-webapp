using System;
using AutoMapper;

namespace StaticWebApp.Template
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();
        }
    }
}