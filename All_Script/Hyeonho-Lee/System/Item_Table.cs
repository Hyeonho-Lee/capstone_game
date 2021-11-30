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
            //Debug.Log("���� ����");
            Item_Load();
        } else {
            //Debug.Log("���� ����");
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
           new JProperty("name", "ȭ���� ����"),
           new JProperty("desc", "���Ͽ��� �귯���� �������� ȭ���� ���� �ִ�.\n3���� ��� ������ ������"),
           new JProperty("image", "../Image/red_particle.png"),
           new JProperty("count", 1));

        JObject Item2 = new JObject(
            new JProperty("index", 2),
            new JProperty("name", "������ ����"),
            new JProperty("desc", "���Ͽ��� �귯���� �������� ������ ���� �ִ�.\n3���� ��� ������ ������"),
            new JProperty("image", "../Image/stone_particle.png"),
            new JProperty("count", 1));

        JObject Item3 = new JObject(
            new JProperty("index", 3),
            new JProperty("name", "������ ����"),
            new JProperty("desc", "���Ͽ��� �귯���� �������� ������ ���� �ִ�.\n3���� ��� ������ ������"),
            new JProperty("image", "../Image/light_particle.png"),
            new JProperty("count", 1));

        JObject Item4 = new JObject(
            new JProperty("index", 4),
            new JProperty("name", "������ ����"),
            new JProperty("desc", "�����ƿ��� ������ �������� ��� ������ �ִ�.\n3���� ��� ������ ������"),
            new JProperty("image", "../Image/peach_particle.png"),
            new JProperty("count", 1));

        JArray jArray = new JArray();

        jArray.Add(Item1);
        jArray.Add(Item2);
        jArray.Add(Item3);
        jArray.Add(Item4);

        JObject items = new JObject();
        items.Add("items", jArray);

        string text = Application.dataPath + "/Resources/ItemJson.json"; // ������Ʈ �̸�\Assets\Resources �ʿ�
        File.WriteAllText(text, items.ToString()); // ���� ���� �� Resources ������ �����

    }

    /*[ContextMenu("Item_Save")]
    public void Item_Save()
    {
        // Ŭ������ Json �������� ����� �ش�.
        var data = JsonConvert.SerializeObject(itemtable, Formatting.Indented);
      
        Debug.Log(data);
    }*/
}
