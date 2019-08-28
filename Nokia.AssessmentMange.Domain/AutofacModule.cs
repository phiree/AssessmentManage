using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Infrastructure;
using Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure;

namespace Nokia.AssessmentMange.Domain
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {




            builder.RegisterAssemblyTypes(this.ThisAssembly)

                .Where(t => new List<string> { "Application", "Service", "Repository" }
                                .Where(x => t.Name.EndsWith(x)).Count() > 0)

                .AsImplementedInterfaces()
                ;
            builder.RegisterGeneric(typeof(EFCRepository<>))
        .As(typeof(IRepository<>))
        .InstancePerLifetimeScope();

            builder.RegisterType<GradeCalculater>()
   .As<IGradeCalculater>();
            builder.RegisterType<CodeRunner>()
 .As<ICodeRunner>();


        }
    }
}
