using Autofac;
using System;

namespace AutofacOpenGeneric
{
    public interface IGeneralType<T1,T2> 
        where T1 : class
        where T2 : class
    {
        void Show();
    }

    public class GeneralType<T1,T2> : IGeneralType<T1,T2> 
        where T1:class 
        where T2:class
    {
        private readonly T1 _t1;
        private readonly T2 _t2;

        public GeneralType(T1 t1,T2 t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        public void Show()
        {
            Console.WriteLine(_t1.ToString()+"\n"+_t2.ToString());
        }
    }
    public class ClassA
    {
        private readonly ClassB _classB;

        public ClassA(ClassB classB)
        {
            _classB = classB;
        }

        public override string ToString()
        {
            return "this is class a";
        }
    }

    public class ClassB
    {
        public ClassB()
        {

        }

        public override string ToString()
        {
            return "this is class b";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(GeneralType<,>))
            .As(typeof(IGeneralType<,>))
            .InstancePerLifetimeScope();

            builder.RegisterType<ClassA>();
            builder.RegisterType<ClassB>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var component = scope.Resolve<IGeneralType<ClassA, ClassB>>();
                component.Show();
            }

        }
    }
}
