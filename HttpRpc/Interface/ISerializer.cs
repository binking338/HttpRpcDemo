using System;

namespace HttpRpc
{
    public interface ISerializer
    {
        object Deserialize(string json, Type type);
        string Serialize(object obj);
    }
}