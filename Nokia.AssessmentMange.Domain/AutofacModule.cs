using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nokia.AssessmentMange.Domain
{
   public  class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t=>new List<string>{"Application","Service","Repository" }
                                .Where(x=>t.Name.EndsWith(x)).Count()>0 )
                .AsImplementedInterfaces();
            
                
        }
    }
}
