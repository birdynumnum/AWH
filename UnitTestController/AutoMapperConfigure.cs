using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAWH.ViewModels;
using Domain;
using AutoMapper;

namespace UnitTestController
{
    public class AutoMapperConfigure
    {
        public static void Configure()
        {
            Mapper.CreateMap<Parent, ParentVM>();
        }
    }
}
