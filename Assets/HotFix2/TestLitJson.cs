using System;
using System.Reflection;
using Database;
using UnityEngine;
using UnityEngine.UI;

public class TestLitJson
{
    public static async void Test()
    {
        AssetBundle dllAB = LoadDll.AssemblyAssetBundle;
        var text = dllAB.LoadAsset<TextAsset>("gm.json").text;
        Debug.Log($"gm text length {text.Length}");
        ParseConfig("GM", text);
        Debug.Log($"CfgGM records count: {CfgGM.Instance.All().Count}");
    }

    private static void ParseConfig(string tableName, string text)
    {
        Debug.Log($"ConfigManager:LoadConfig 开始解析配置表{tableName}");
        var className = $"Database.Cfg{tableName.Substring(0, 1).ToUpper()}{tableName.Substring(1)}";
        Type nowType = Type.GetType(className);
        if (nowType == null)
        {
            Debug.LogError($"Type.GetType({className}) 出错");
            return;
        }

        PropertyInfo property = nowType.GetProperty("Instance");
        if (property == null)
        {
            Debug.LogError($"Type({className}) Instance 未定义");
            return;
        }

        var obj = property.GetMethod.Invoke(null, null);
        MethodInfo method = nowType.GetMethod("Parse");
        if (method == null)
        {
            Debug.LogError($"Type({className}) Parse 未定义");
            return;
        }

        try
        {
            method.Invoke(obj, new object[] {text});
        }
        catch (Exception e)
        {
            Debug.LogError($"Parse {tableName} error, msg: {e}");
        }
    }
}
