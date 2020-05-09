using Autofac;
using System;

namespace Lifetime_Scope
{
    class Worker
    {
        public Guid Id { get; set; }

        public Worker()
        {
            Id = Guid.NewGuid();
        }

        public void DoWork()
        {
            Console.WriteLine($"{Id} is working!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //https://autofaccn.readthedocs.io/en/latest/lifetime/instance-scope.html#instance-per-matching-lifetime-scope
            var builder = new ContainerBuilder();
            builder.RegisterType<Worker>();
            const string scope_name = "scope1";
            //builder.RegisterType<Worker>().InstancePerLifetimeScope();
            builder.RegisterType<Worker>().InstancePerMatchingLifetimeScope(scope_name);

            var container = builder.Build();
            using (var scope1 = container.BeginLifetimeScope(scope_name))
            {
                var w1 = scope1.Resolve<Worker>();
                w1.DoWork();
                using (var scope2 = scope1.BeginLifetimeScope())
                {
                    var w2 = scope2.Resolve<Worker>();
                    w2.DoWork();
                }
            }
            using (var scope3 = container.BeginLifetimeScope(scope_name))
            {
                var w3 = scope3.Resolve<Worker>();
                w3.DoWork();
                using (var scope4 = scope3.BeginLifetimeScope())
                {
                    var w4 = scope4.Resolve<Worker>();
                    w4.DoWork();
                }
            }

            //// You can't resolve a per-matching-lifetime-scope component
            //// if there's no matching scope.
            //using (var noTagScope = container.BeginLifetimeScope())
            //{
            //    // This throws an exception because this scope doesn't
            //    // have the expected tag and neither does any parent scope!
            //    var fail = noTagScope.Resolve<Worker>();
            //}
        }
    }
}
