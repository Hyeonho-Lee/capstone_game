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
        if (npc_name == "복숭아 나무") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();
            player_data.PlayerDataSave();
            player_data.PlayerDataLoad();
            inventory_table.Inventory_Save();
            inventory_table.Inventory_Load();
            Debug.Log("저장 완료");
        }

        if (npc_name == "모루") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();

            dialogue.image_index = 1;
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = "덩그러니 남겨진 허름한 모루이다.\n파편을 3개 가져오면 장비를 강화할수 있어 보인다.";

            for (int i = 0; i < inventory_table.inventory.Length; i++) {
                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 1 && !player_data.playerDataTable.is_stone_1) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "화염의 파편 3개를 강화하여 완전한 불의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "공격력이 1 > 2가 됩니다.";
                    player_data.playerDataTable.is_stone_1 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                }else if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 3 && !player_data.playerDataTable.is_stone_2) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "바위의 파편 3개를 강화하여 완전한 바위의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "체력이 10 > 14가 됩니다.";
                    player_data.playerDataTable.is_stone_3 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                } else if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 2 && !player_data.playerDataTable.is_stone_3) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[2];
                    dialogue.sentences[0] = "번개의 파편 3개를 강화하여 완전한 번개의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "이동속도가 1(15) > 1.4(21) 가 됩니다.";
                    player_data.playerDataTable.is_stone_2 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                }
            }
        }

        manager.Start_Dialogue(dialogue);
    }
}
