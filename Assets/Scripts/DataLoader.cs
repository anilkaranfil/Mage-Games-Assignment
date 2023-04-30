using UnityEditor;
using UnityEngine;

public class DataLoader
{
    public static string LoadedData(string fileName)
    {
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        string json = Resources.Load<TextAsset>(fileName).text;
        json = json.Replace("\n", "").Replace("\r", "");
        return json;
    }
}