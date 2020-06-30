using System;
namespace HttpRpc
{
    /// <summary>
    /// 基于Json.Net的序列化器
    /// </summary>
    public class Serializer : ISerializer
    {
        protected Newtonsoft.Json.JsonSerializerSettings Settings { get; set; }
            = new Newtonsoft.Json.JsonSerializerSettings()
            { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };

        public string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Settings);
        }

        public object Deserialize(string data, Type type)
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(data, type, Settings);
            if (type != null && (type.IsPrimitive || type.IsEnum))
                return Convert.ChangeType(result, type);
            else
                return result;
        }
    }
}
