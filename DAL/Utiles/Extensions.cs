using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utiles
{

    public static class Extensions
    {
        public static T DeepClone<T>(this T obj)
        {
            // 객체를 JSON 문자열로 직렬화
            var serialized = JsonConvert.SerializeObject(obj);
            // JSON 문자열을 다시 객체로 역직렬화
            return JsonConvert.DeserializeObject<T>(serialized)!;
        }

    }
}