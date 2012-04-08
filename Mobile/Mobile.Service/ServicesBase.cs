using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile.Service
{
    public abstract class ServicesBase<TBLL> : System.Web.Services.WebService where TBLL : class
    {
        protected TBLL GetBLL()
        {
            var type = typeof(TBLL);
            return (TBLL)type.InvokeMember(type.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { });
        }

        protected TTarget CopyValueProperties<TTarget>(TTarget target, object source) where TTarget : class
        {
            if (source == null) return null;
            var typeTarget = typeof(TTarget);
            var typeSource = source.GetType();
            TTarget result = target ?? (TTarget)typeTarget.InvokeMember(typeTarget.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { });
            var propertiesTarget = typeTarget.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            
            foreach (var p in propertiesTarget)
            {
                var pSource = typeSource.GetProperty(p.Name);
                if (pSource != null && pSource.PropertyType == p.PropertyType && p.CanWrite && pSource.CanRead)
                {
                    p.SetValue(result, pSource.GetValue(source, null), null);
                }
            }

            return result;
        }

        protected object SerializePreJSON(object source)
        {
            if (source == null) return null;
            var typeSource = source.GetType();
            var propertiesSource = typeSource.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var p in propertiesSource)
            {
                var a = new { };
            }

            return null;
        }
    }
}