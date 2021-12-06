using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_Trigger : MonoBehaviour
{
    public string npc_name;

    public NPC_Dialogue dialogue;
    private NPC_Manager manager;
    private TextMeshPro textmeshs;
    private PlayerData player_data;
    private Inventory_Table inventory_table;
    private Player_Skill player_skill;

    void Update()
    {
        if (transform.GetChild(0) != null) {
            if (transform.GetChild(0).name == "3D Canvas") {
                GameObject canvas = transform.GetChild(0).gameObject;
                canvas.transform.LookAt(GameObject.Find("Main Camera").transform);
                if (canvas.transform.GetChild(0).name == "Name") {
                    GameObject name = canvas.transform.GetChild(0).gameObject;
                    textmeshs = name.GetComponent<TextMeshPro>();
                    textmeshs.text = npc_name;
                }
            }
        }
    }

    public void Dialogue_Trigger()
    {
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        Talk_Trigger(dialogue.name);
    }

    public void Talk_Trigger(string npc_name)
    {
        if (npc_name == "������ ����") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();
            player_skill = GameObject.Find("Player").GetComponent<Player_Skill>();

            player_skill.heal_count = 3;
            player_skill.change_momo();

            player_data.PlayerDataSave();
            player_data.PlayerDataLoad();

            if (player_data.playerDataTable.player_position_x >= -50.0f && player_data.playerDataTable.player_position_x <= 50.0f &&
                player_data.playerDataTable.player_position_z >= 100.0f && player_data.playerDataTable.player_position_z <= 200.0f) {
                player_data.playerDataTable.town = true;
                player_data.playerDataTable.stage_2 = false;
            } else {
                player_data.playerDataTable.town = false;
            }

            if (player_data.playerDataTable.player_position_x >= -50.0f && player_data.playerDataTable.player_position_x <= 50.0f &&
                player_data.playerDataTable.player_position_z >= 500.0f && player_data.playerDataTable.player_position_z <= 600.0f) {
                player_data.playerDataTable.stage_2 = true;
                player_data.playerDataTable.town = false;
            } else {
                player_data.playerDataTable.stage_2 = false;
            }

            inventory_table.Inventory_Save();
            inventory_table.Inventory_Load();

            player_data.PlayerDataSave();
            player_data.PlayerDataLoad();

            Debug.Log("���� �Ϸ�");
        }

        if (npc_name == "���") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();

            dialogue.image_index = 1;
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = "���׷��� ������ �㸧�� ����̴�.\n������ 3�� �������� ��� ��ȭ�Ҽ� �־� ���δ�.";

            for (int i = 0; i < inventory_table.inventory.Length; i++) {
                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 3 && !player_data.playerDataTable.is_stone_3) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "������ ���� 3���� ��ȭ�Ͽ� ������ ������ ������ ������ϴ�.";
                    dialogue.sentences[1] = "4�� ��ų�� Ȱ��ȭ �Ǿ����ϴ�.";
                    player_data.playerDataTable.is_stone_2 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }

                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 2 && !player_data.playerDataTable.is_stone_2) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "������ ���� 3���� ��ȭ�Ͽ� ������ ������ ������ ������ϴ�.";
                    dialogue.sentences[1] = "3�� ��ų�� Ȱ��ȭ �Ǿ����ϴ�.";
                    player_data.playerDataTable.is_stone_3 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }

                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 1 && !player_data.playerDataTable.is_stone_1) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "ȭ���� ���� 3���� ��ȭ�Ͽ� ������ ���� ������ ������ϴ�.";
                    dialogue.sentences[1] = "2�� ��ų�� Ȱ��ȭ �Ǿ����ϴ�.";
                    player_data.playerDataTable.is_stone_1 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }
            }
        }

        manager.Start_Dialogue(dialogue);
    }
}
