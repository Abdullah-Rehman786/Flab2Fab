using Autofac;
using Flab2Fab.App_Data;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flab2Fab.Config
{
    public class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register your services here
            builder.RegisterType<WeightDataAccess>().As<IWeightDataAccess>().InstancePerRequest();
            builder.RegisterType<TrackWeight>();
            var container = builder.Build();

            // Set the dependency resolver implementation.
            //container.Resolve<IWeightDataAccess>();
        }
    }
}