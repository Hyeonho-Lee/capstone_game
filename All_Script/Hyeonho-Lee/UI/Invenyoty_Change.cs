using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Invenyoty_Change : MonoBehaviour
{
    public Sprite item_image_1;
    public Sprite item_image_2;
    public Sprite item_image_3;

    private GameObject all_slot;
    private Inventory_Table inventory_table;

    public void Change_Update()
    {
        all_slot = this.gameObject;
        inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();

        for (int i = 0; i < all_slot.transform.GetChildCount(); i++) {
            GameObject value = all_slot.transform.GetChild(i).gameObject;
            Image image = value.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI text_mesh = value.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            if (inventory_table.inventory[i].item_index == 0) {
                image.sprite = null;
            } else if (inventory_table.inventory[i].item_index == 1) {
                image.sprite = item_image_1;
            } else if (inventory_table.inventory[i].item_index == 2) {
                image.sprite = item_image_3;
            } else if (inventory_table.inventory[i].item_index == 3) {
                image.sprite = item_image_2;
            }

            if (inventory_table.inventory[i].item_count == 0) {
                text_mesh.text = "";
            }else {
                text_mesh.text = inventory_table.inventory[i].item_count.ToString();
            }
        }
    }
}
