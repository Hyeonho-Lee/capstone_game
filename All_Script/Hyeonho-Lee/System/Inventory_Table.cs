using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class Inventory
{
    public int inv_index;
    public int item_index;
    public int item_count;
}

public class Inventory_Table : MonoBehaviour
{
    public Inventory[] inventory = new Inventory[20];

    void Start()
    {
        Inventory_Reset();

        FileInfo fi = new FileInfo(Application.dataPath + "/Resources/InventoryJson.json");

        if (fi.Exists) {
            //Debug.Log("파일 있음");
            Inventory_Load();
        } else {
            //Debug.Log("파일 없음");
            Inventory_Create();
            Inventory_Load();
        }
    }

    public void Inventory_Reset()
    {
        for (int i = 0; i < inventory.Length; i++) 
        {
            inventory[i].inv_index = 0;
            inventory[i].item_index = 0;
            inventory[i].item_count = 0;
        }
    }

    [ContextMenu("Inventory_Create")]
    public void Inventory_Create()
    {
        JObject status = new JObject(
            new JProperty("inv_index", 0),
            new JProperty("item_index", 0),
            new JProperty("item_count", 0)
        );

        JArray inventory_array = new JArray();
        for (int i = 0; i < inventory.Length; i++) 
        {
            inventory_array.Add(status);
        }

        string text = Application.dataPath + "/Resources/InventoryJson.json";
        File.WriteAllText(text, inventory_array.ToString());
    }

    [ContextMenu("Inventory_Load")]
    public void Inventory_Load()
    {
        string text = Application.dataPath + "/Resources/InventoryJson.json";
        string text_data = File.ReadAllText(text);

        JArray data = JArray.Parse(text_data);

        for (int i = 0; i < data.Count; i++) 
        {
            JObject j_data = (JObject)data[i];
            inventory[i].inv_index = i;
            inventory[i].item_index = int.Parse(j_data["item_index"].ToString());
            inventory[i].item_count = int.Parse(j_data["item_count"].ToString());
        }
    }

    [ContextMenu("Inventory_Save")]
    public void Inventory_Save()
    {

        JArray inventory_array = new JArray();
        for (int i = 0; i < inventory.Length; i++) {
            inventory_array.Add(new JObject(
                new JProperty("inv_index", inventory[i].inv_index),
                new JProperty("item_index", inventory[i].item_index),
                new JProperty("item_count", inventory[i].item_count)
            ));
        }

        string text = Application.dataPath + "/Resources/InventoryJson.json";
        File.WriteAllText(text, inventory_array.ToString());
    }
}
