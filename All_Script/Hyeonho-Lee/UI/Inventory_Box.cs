using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory_Box : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject info_images;
    public GameObject info_texts;

    public Sprite info_image_0;
    public Sprite info_image_1;
    public Sprite info_image_2;
    public Sprite info_image_3;

    private Inventory_Table inventory_table;
    private Item_Table item_table;
    

    void Start()
    {
        inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();
        item_table = GameObject.Find("System").GetComponent<Item_Table>();
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        int item_index = this.transform.GetSiblingIndex();
        if (inventory_table.inventory[item_index].item_index == 1) {
            Image result = info_images.transform.GetChild(0).GetComponent<Image>();
            result.sprite = info_image_1;
            TextMeshProUGUI text_mesh = info_texts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text_mesh.text = item_table.itemtable[0].desc;
        } else if (inventory_table.inventory[item_index].item_index == 2) {
            Image result = info_images.transform.GetChild(0).GetComponent<Image>();
            result.sprite = info_image_3;
            TextMeshProUGUI text_mesh = info_texts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text_mesh.text = item_table.itemtable[1].desc;
        } else if (inventory_table.inventory[item_index].item_index == 3) {
            Image result = info_images.transform.GetChild(0).GetComponent<Image>();
            result.sprite = info_image_2;
            TextMeshProUGUI text_mesh = info_texts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text_mesh.text = item_table.itemtable[2].desc;
        } else {
            Image result = info_images.transform.GetChild(0).GetComponent<Image>();
            result.sprite = info_image_0;
            TextMeshProUGUI text_mesh = info_texts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text_mesh.text = "";
        }

        Transform child = this.transform.GetChild(2);
        child.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        Transform child = this.transform.GetChild(2);
        child.gameObject.SetActive(false);
    }
}
