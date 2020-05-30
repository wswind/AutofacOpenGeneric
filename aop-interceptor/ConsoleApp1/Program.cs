/*
 https://autofaccn.readthedocs.io/en/latest/advanced/interceptors.html
 this is a demo for autofac interceptors
 */
//#define TYPED_REGISTRATION

using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            // create builder
            var builder = new ContainerBuilder();
#if TYPED_REGISTRATION
            builder.Register(c => new CallLogger(Console.Out));
            builder.RegisterType<SomeType>()
                   .As<ISomeType>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(CallLogger));
#else // Named registration
            builder.Register(c => new CallLogger(Console.Out))
                .Named<IInterceptor>("log-calls");
            builder.RegisterType<SomeType>()
                .As<ISomeType>()
                .EnableInterfaceInterceptors()
                .InterceptedBy("log-calls");
           
#endif
            var container = builder.Build();
            var willBeIntercepted = container.Resolve<ISomeType>();
            willBeIntercepted.Show("this is a test");
        }
    }
}
