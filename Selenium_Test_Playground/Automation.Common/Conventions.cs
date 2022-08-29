using System;
using Automation.Common;

namespace Automation.Common
{

    public static class Conventions
    {
        public static void Enforce<T>(T target, Predicate<T> condition, string message)
        {
            if (target is null)
                throw new ArgumentNullException("Argument cannot be null.");
            if (!condition.Invoke(target))
                throw new ConventionException($"[{target.GetFriendlyTypeName()}] Convention Error :{message}");
        }
    }
}