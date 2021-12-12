using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NPC_Trigger : MonoBehaviour
{
    public string npc_name;

    private bool is_end;

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
        if (npc_name == "복숭아 나무") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();
            player_skill = GameObject.Find("Player").GetComponent<Player_Skill>();

            player_data.playerDataTable.tutorial_talk = true;
            player_data.playerDataTable.town_talk = true;

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

            Debug.Log("저장 완료");
        }

        if (npc_name == "모루") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();

            dialogue.image_index = 1;
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = "덩그러니 남겨진 허름한 모루이다.\n파편을 3개 가져오면 장비를 강화할수 있어 보인다.";

            for (int i = 0; i < inventory_table.inventory.Length; i++) {
                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 3 && !player_data.playerDataTable.is_stone_3) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[3];
                    dialogue.sentences[0] = "번개의 파편 3개를 강화하여 완전한 번개의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "4번 스킬이 활성화 되었습니다.";
                    dialogue.sentences[2] = "4번 스킬은 플레이어 주위의 강력한 검을 떨구워 적을 공격합니다.";
                    player_data.playerDataTable.is_stone_2 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }

                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 2 && !player_data.playerDataTable.is_stone_2) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[3];
                    dialogue.sentences[0] = "바위의 파편 3개를 강화하여 완전한 바위의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "3번 스킬이 활성화 되었습니다.";
                    dialogue.sentences[2] = "3번 스킬은 플레이어 정면에 에너지를 발사하여 적을 공격합니다.";
                    player_data.playerDataTable.is_stone_3 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }

                if (inventory_table.inventory[i].item_count >= 3 && inventory_table.inventory[i].item_index == 1 && !player_data.playerDataTable.is_stone_1) {
                    dialogue.image_index = 1;
                    dialogue.sentences = new string[3];
                    dialogue.sentences[0] = "화염의 파편 3개를 강화하여 완전한 불의 결정을 얻었습니다.";
                    dialogue.sentences[1] = "2번 스킬이 활성화 되었습니다.";
                    dialogue.sentences[2] = "2번 스킬은 20초 동안 플레이어의 체력 +1 / 이동속도 / 스테미나 회복속도를 증가시킵니다.";
                    player_data.playerDataTable.is_stone_1 = true;
                    inventory_table.inventory[i].item_count -= 3;
                    if (inventory_table.inventory[i].item_count <= 0) {
                        inventory_table.inventory[i].item_index = 0;
                    }
                    break;
                }
            }
        }

        if (npc_name == "????") {
            Debug.Log("엔딩과 대화중");
            StartCoroutine(End());
        }

        manager.Start_Dialogue(dialogue);
    }

    IEnumerator End()
    {
        if (!is_end) {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            inventory_table = GameObject.Find("System").GetComponent<Inventory_Table>();

            is_end = true;
            yield return new WaitForSeconds(5.0f);
            Animator end_ui = GameObject.Find("end_effect").GetComponent<Animator>();
            end_ui.Play("end_effect");
            yield return new WaitForSeconds(6.0f);
            player_data.PlayerDataReset();
            inventory_table.Inventory_Resets();
            SceneManager.LoadScene("Main_Lobby");
        }
    }
}
