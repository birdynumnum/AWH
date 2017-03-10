using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWH.Infrastructure
{
    public static class IoC
    {
        public static IContainer Container {get; set; }
 
        static IoC()
        {
            Container = new Container();
        }
    }
}