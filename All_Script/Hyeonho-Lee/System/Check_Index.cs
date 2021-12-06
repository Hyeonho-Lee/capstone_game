using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Check_Index : MonoBehaviour
{
    public int index;

    public Sprite image_0;
    public Sprite image_1;
    public Sprite image_2;

    private GameObject ui_name;
    private Image ui_image;

    private GameObject system;

    private Inventory_Table inventory_table;
    private Item_Table item_table;
    private TextMeshProUGUI textmesh;

    void Start()
    {
        system = GameObject.Find("System");
        inventory_table = system.GetComponent<Inventory_Table>();
        item_table = system.GetComponent<Item_Table>();
    }

    public void Drop_Inventory()
    {
        //inventory_table.inventory[0].item_index = index;
        //inventory_table.inventory[0].item_count += 1;
        int box_index;
        for(int i = 0; i < 20; i++) {
            if (inventory_table.inventory[i].item_count == 0) {
                box_index = i;
                inventory_table.inventory[i].item_index = index;
                inventory_table.inventory[i].item_count += 1;
                break;
            }else if (inventory_table.inventory[i].item_count != 0) {
                if (inventory_table.inventory[i].item_index == index) {
                    inventory_table.inventory[i].item_count += 1;
                    break;
                }
            }
        }

        textmesh = GameObject.Find("item_name").GetComponent<TextMeshProUGUI>();
        textmesh.text = item_table.itemtable[index-1].name + " x 1";

        ui_image = GameObject.Find("item_image").GetComponent<Image>();
        if (index == 1) {
            ui_image.sprite = image_0;
        }
        if (index == 2) {
            ui_image.sprite = image_2;
        }
        if (index == 3) {
            ui_image.sprite = image_1;
        }
    }
}
