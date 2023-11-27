using UnityEngine;
using System.Collections;
using LitJson;
using System.Text;   //使用StringBuilder
using System.IO;     //使用文件流

public class JsonHelper : MonoBehaviour
{
    public static string GetJson()
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        int width = Manager.Instance.width;
        int height = Manager.Instance.height;
        Number[,] numbers = Manager.Instance.OnGetNumbers();
        MapData map = new MapData();
        map.width = width;
        map.height = height;
        map.numbers = new int[width * height];
        string str = "";    // 打印用
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map.numbers[i * width + j] = numbers[i, j] ? numbers[i, j].num : 0;
                str = str + (numbers[i, j] ? numbers[i, j].num : 0);
            }
        }
        writer.WriteObjectStart();//字典开始
        writer.WritePropertyName("Map");  // 记录宽度
        writer.Write(JsonMapper.ToJson(map));
        writer.WriteObjectEnd();

        return sb.ToString();  //返回Json格式的字符串
    }

    //保存Json格式字符串
    public static void SaveJsonString(string JsonString)
    {
        FileInfo file = new FileInfo(Application.persistentDataPath + "/JsonData.Json");
        StreamWriter writer = file.CreateText();
        writer.Write(JsonString);
        writer.Close();
        writer.Dispose();
    }

    //从文件里面读取json数据
    public static bool TryGetJsonString(out string jsonData)
    {
        jsonData = "";
        try
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + "/JsonData.Json");
            jsonData = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class MapData
{
    public int width;
    public int height;
    public int[] numbers;
}