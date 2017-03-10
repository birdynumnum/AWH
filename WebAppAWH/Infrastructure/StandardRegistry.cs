using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using StructureMap;

namespace WebAppAWH.Infrastructure
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly("WebAppAWH");
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}

        