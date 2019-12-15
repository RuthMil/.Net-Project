using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Cloning
    {
        public static Clonable Clone(this Clonable source)
        {
            Clonable target = (Clonable)Activator.CreateInstance(source.GetType());
            var type = source.GetType();
            foreach (var sourceProperty in type.GetProperties())
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
            return target;
        }
    }
}
