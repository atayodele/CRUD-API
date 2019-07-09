using AutoMapper;
using CrudApi.Models;
using CrudApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<AddCustomerVM, Customer>();
            CreateMap<Customer, ListCustomerVM>()
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<UpdateCustomerVM, Customer>();
        }
    }

    public static class HelpMethod
    {
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}
