using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace DAL
{
    public static class Cloning
    {

        public static T Clone<T>(this T source)
        {
            var isNotSerializable = !typeof(T).IsSerializable;
            if (isNotSerializable)
                throw new ArgumentException("The type must be serializable.", "source");
            var sourceIsNull = ReferenceEquals(source, null);
            if (sourceIsNull)
                return default(T);
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}

//public static BE.Clonable Clone(this BE.Clonable source)
//{ //לבדוק האם גט ואליו מקבל מחלקות
//    BE.Clonable target = (BE.Clonable)Activator.CreateInstance(source.GetType());
//    var type = source.GetType();
//    foreach (var sourceProperty in type.GetProperties())
//    {
//        var targetProperty = type.GetProperty(sourceProperty.Name);
//        targetProperty.SetValue(target, sourceProperty.GetValue(source));
//    }
//    return target;
//}