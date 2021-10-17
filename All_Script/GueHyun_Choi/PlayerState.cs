using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class PlayerTable
{
    float health;
    float move_speed;
    float attack_speed;
    float attack_damage;
    int life_count;
    int potion_count;
    float potion_recovery;
}

public class PlayerState : MonoBehaviour
{



    private void Start()
    {
        //Load();
    }





    [ContextMenu("Item_Load")]
    public void Load()
    {
        /*
        itemtable = new List<Item>();
        string text = Application.dataPath + "/TestJson.json";
        string text_data = File.ReadAllText(text);

        JsonConvert.DeserializeObject

        JObject data = JObject.Parse(text_data);
        JArray json_data = (JArray)data["item"];

        Debug.Log(json_data);

        for (int i = 0; i < 4; i++)
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

        foreach (var s_item in itemtable)
        {
            Debug.Log(s_item.name);
        }
        */
    }

}
