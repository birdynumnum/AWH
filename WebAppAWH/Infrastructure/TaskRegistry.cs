using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using WebAppAWH.Infrastructure.Tasks;

namespace WebAppAWH.Infrastructure
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("WebAppAWH"));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartUp>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
            });
        }
    }
}