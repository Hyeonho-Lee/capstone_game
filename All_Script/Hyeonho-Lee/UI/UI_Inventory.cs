using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public GameObject inventory_ui;
    public GameObject status;
    public GameObject skill_slot;

    private PlayerMovement movement;
    private Invenyoty_Change inventory_change;

    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (movement.is_talk || movement.is_inventory) {
            status.SetActive(false);
            skill_slot.SetActive(false);
        }else {
            status.SetActive(true);
            skill_slot.SetActive(true);
        }
    }

    public void Inventory_Start()
    {
        inventory_ui.SetActive(true);
        movement.is_inventory = true;
        inventory_change = GameObject.Find("all_slot").GetComponent<Invenyoty_Change>();
        inventory_change.Change_Update();
    }

    public void Inventory_Exit()
    {
        inventory_ui.SetActive(false);
        movement.is_inventory = false;
    }
}
