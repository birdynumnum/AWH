using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebAppAWH.Domain.Mapping;
using WebAppAWH.Infrastructure.Tasks;

namespace WebAppAWH.App_Start
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            LoadStandardMappings(types);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();


            var s = maps.ToString();

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
                var t = map.Source.ToString();
            }
        }
    }
}