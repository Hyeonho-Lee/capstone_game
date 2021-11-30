using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Index : MonoBehaviour
{
    public int index;

    private GameObject system;

    private Inventory_Table inventory_table;

    void Start()
    {
        system = GameObject.Find("System");
        inventory_table = system.GetComponent<Inventory_Table>();
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
    }
}
