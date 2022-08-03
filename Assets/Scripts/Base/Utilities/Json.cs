using UnityEngine;
using System;

public static class Json
{
    public static string ConvertToJson<T>(T value)
    {
        return JsonUtility.ToJson(value);
    }
    //Type is required for abstract and interfaces
    public static T ConvertFromJson<T>(string value, Type type = null)
    {
        return (T)JsonUtility.FromJson(value, type != null ? type : typeof(T));
    }

}