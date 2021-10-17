using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class Item
{
    public int index;
    public string name;
    public string desc;
    public string image;
    public int count;
    //public float drop_percent;
}
public class Item_Table : MonoBehaviour
{
    public List<Item> itemtable = new List<Item>();

    void Start()
    {
        //Item_Load();
    }

    [ContextMenu("Item_Load")]
    public void Item_Load()
    {
        itemtable = new List<Item>();
        string text = Application.dataPath + "/Script/All_Script/Hyeonho-Lee/System/ItemJson.json";
        string text_data = File.ReadAllText(text);

        JObject data = JObject.Parse(text_data);
        JArray json_data = (JArray)data["items"];

        for (int i = 0; i < json_data.Count; i++)
        {
            itemtable.Add(new Item()
            {
                index = int.Parse(json_data[i]["index"].ToString()),
                name = json_data[i]["name"].ToString(),
                desc = json_data[i]["desc"].ToString(),
                image = json_data[i]["image"].ToString(),
                count = int.Parse(json_data[i]["count"].ToString())
            });
        }
    }

    [ContextMenu("Item_Save")]
    public void Item_Save()
    {
        // 클래스를 Json 형식으로 만들어 준다.
        var data = JsonConvert.SerializeObject(itemtable, Formatting.Indented);
      
        Debug.Log(data);

        //string text_data = data.ToString();
        //string text = Application.dataPath + "/Script/All_Script/Hyeonho-Lee/System/ItemJson.json";

        //Debug.Log(text_data);
        //File.WriteAllText(text, text_data);
        

        
        /*foreach (var i in itemtable)
        {
            data = JObject.FromObject(i);
            json_data.Add(i);
        }

        json_data2.Add("item", json_data);

        string _text = json_data2.ToString();
        string _path = Application.dataPath + "/TestJson.json";

        Debug.Log(_text); */
        //File.WriteAllText(_path, _text);
    }
}
