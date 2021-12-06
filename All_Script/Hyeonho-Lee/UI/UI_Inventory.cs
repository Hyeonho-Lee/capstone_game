using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public GameObject inventory_ui;
    public GameObject status;
    public GameObject skill_slot;

    public GameObject skill_2;
    public GameObject skill_3;
    public GameObject skill_4;

    private PlayerMovement movement;
    private Invenyoty_Change inventory_change;
    private PlayerData player_data;

    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player_data = GetComponent<PlayerData>();
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

        if (player_data.playerDataTable.is_stone_1 == true) {
            skill_2.SetActive(true);
        }

        if (player_data.playerDataTable.is_stone_1 == false) {
            skill_2.SetActive(false);
        }

        if (player_data.playerDataTable.is_stone_2 == true) {
            skill_4.SetActive(true);
        }

        if (player_data.playerDataTable.is_stone_2 == false) {
            skill_4.SetActive(false);
        }

        if (player_data.playerDataTable.is_stone_3 == true) {
            skill_3.SetActive(true);
        }

        if (player_data.playerDataTable.is_stone_3 == false) {
            skill_3.SetActive(false);
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
