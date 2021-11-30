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

    public List<GameObject> item_prefab = new List<GameObject>();

    void Start()
    {
        //Item_Load();

        FileInfo fi = new FileInfo(Application.dataPath + "/Resources/ItemJson.json");

        if (fi.Exists) {
            //Debug.Log("파일 있음");
            Item_Load();
        } else {
            //Debug.Log("파일 없음");
            Item_Create();
            Item_Load();
        }
    }

    [ContextMenu("Item_Load")]
    public void Item_Load()
    {
        itemtable = new List<Item>();
        string text = Application.dataPath + "/Resources/ItemJson.json";
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

    [ContextMenu("Item_Create")]
    public void Item_Create()
    {
        JObject Item1 = new JObject(
           new JProperty("index", 1),
           new JProperty("name", "화염의 파편"),
           new JProperty("desc", "오니에서 흘러나온 파편으로 화염이 깃들어 있다.\n3개를 모아 마을로 가보자"),
           new JProperty("image", "../Image/red_particle.png"),
           new JProperty("count", 1));

        JObject Item2 = new JObject(
            new JProperty("index", 2),
            new JProperty("name", "바위의 파편"),
            new JProperty("desc", "오니에서 흘러나온 파편으로 바위가 깃들어 있다.\n3개를 모아 마을로 가보자"),
            new JProperty("image", "../Image/stone_particle.png"),
            new JProperty("count", 1));

        JObject Item3 = new JObject(
            new JProperty("index", 3),
            new JProperty("name", "번개의 파편"),
            new JProperty("desc", "오니에서 흘러나온 파편으로 번개가 깃들어 있다.\n3개를 모아 마을로 가보자"),
            new JProperty("image", "../Image/light_particle.png"),
            new JProperty("count", 1));

        JObject Item4 = new JObject(
            new JProperty("index", 4),
            new JProperty("name", "복숭아 씨앗"),
            new JProperty("desc", "복숭아에서 떨어진 씨앗으로 밝게 빛나고 있다.\n3개를 모아 마을로 가보자"),
            new JProperty("image", "../Image/peach_particle.png"),
            new JProperty("count", 1));

        JArray jArray = new JArray();

        jArray.Add(Item1);
        jArray.Add(Item2);
        jArray.Add(Item3);
        jArray.Add(Item4);

        JObject items = new JObject();
        items.Add("items", jArray);

        string text = Application.dataPath + "/Resources/ItemJson.json"; // 프로젝트 이름\Assets\Resources 필요
        File.WriteAllText(text, items.ToString()); // 실제 빌드 후 Resources 폴더에 저장됨

    }

    /*[ContextMenu("Item_Save")]
    public void Item_Save()
    {
        // 클래스를 Json 형식으로 만들어 준다.
        var data = JsonConvert.SerializeObject(itemtable, Formatting.Indented);
      
        Debug.Log(data);
    }*/
}
