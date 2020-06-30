using System;

namespace HttpRpc
{
    /// <summary>
    /// 序列化器
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Deserialize(string data, Type type);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(object obj);
    }
}