using System.Collections;

namespace ProjectBookShop.Areas.Api.Classes
{
    public class Assert
    {
       
      
        public static void NotNull<T>(T obj,string name,string message=null)
            where T:class
        {
            var c = obj;
            var a = nameof(T);
            var b = nameof(name);
            var n = name.GetType();
            if(obj is null)
            {
                throw new ArgumentNullException($"{name}{nameof(T)}", message);
            }
        }
        public static void NotNull<T>(T? obj,string name,string message=null) where T:struct
        {
            //(obj==null)
            if (!obj.HasValue)
            {
                throw new ArgumentException($"({name}{typeof(T)})",message);
            }
        }
        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null) where T :class
        {
            if (obj==defaultValue
                ||(obj is string str && string.IsNullOrWhiteSpace(str))
                ||(obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentNullException("Argument is empty:" + message, $"{name}:{typeof(T)}");
            }
        }
    }

    
}
