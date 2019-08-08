using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    public static class LoggerHelper
    {
        public static bool IsLoggerEnabled(TypeInfo type)
        {
            var b1 = HasCustomAttribute(type);
            var b2 = GetStartLog(type);
            return false;
        }
        private static bool HasCustomAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(CustomAttribute), true);
        }

        private static bool GetStartLog(MemberInfo methodInfo)
        //private static bool GetStartLog(MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<CustomAttribute>().ToArray();
            if (attrs.Any())
            {
                CustomAttribute customAttribute = attrs.First();
                return customAttribute.StartLog;
            }
            return false;
        }


    }
}
