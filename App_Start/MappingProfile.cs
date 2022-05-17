using AutoMapper;
using CinemaSharpAuth.Dto;
using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaSharpAuth.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping for customers Dto's
            Mapper.CreateMap<Customer, CustomerDto>(); 
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            //Mapping for movies Dto's
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}