using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class World
{
    public string world_name;
    public bool is_world_enter;
}

public class World_Admin : MonoBehaviour
{
    public World[] world = new World[10];

    public string scene_name;

    public NPC_Dialogue dialogue;
    private PlayerData player_data;
    private NPC_Manager manager;

    void Start()
    {
        player_data = GameObject.Find("System").GetComponent<PlayerData>();
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        Check_Next();
        Input_World();
    }

    void Update()
    {
        Check_Stage();
    }

    void Check_Next()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (GameObject.FindGameObjectsWithTag("Get_Next").Length != 1) 
        {
            Destroy(GameObject.FindGameObjectsWithTag("Get_Next")[1]);
        }
    }

    void Check_Stage()
    {
        for (int i = 0; i < world.Length; i++) 
        {
            if (scene_name == world[i].world_name) 
            {
                world[i].is_world_enter = true;
            } else 
            {
                world[i].is_world_enter = false;
            }
        }

        if (world[5].is_world_enter && player_data.playerDataTable.wolf_boss) {
            if (GameObject.Find("Wolf_Patern") != null) {
                Destroy(GameObject.Find("Wolf_Patern"));
            }
        }

        if (!player_data.playerDataTable.town_talk && world[1].is_world_enter) {
            player_data.playerDataTable.town_talk = true;

            dialogue.image_index = 9;
            dialogue.name = "까마귀";
            dialogue.sentences = new string[5];
            dialogue.sentences[0] = "모모타로!!!! 지금 마을에 위기가 찾아왔어 ㅠㅠ";
            dialogue.sentences[1] = "갑자기 마을에 오니들이 습격을 했는데 수호신들이 당해버렷어...\n그렇게 저주를 얻어버렷지.....";
            dialogue.sentences[2] = "모모타로! 부디 수호신들의 저주를 풀고 오니들을 물리쳐다오!!!";
            dialogue.sentences[3] = "지역 곳곳에 놓인 복숭아 나무에게 대화를 걸면 축복을 걸어줄꺼야.\n그러니 꼭 대화를 걸어줘!!";
            dialogue.sentences[4] = "혹시나 진행이 막힌다면 마을에 허수아비에게 대화를 걸어봐\n그럼 이만!";

            manager.Start_Dialogue(dialogue);
        }

        if (!player_data.playerDataTable.wolf_boss_talk && world[5].is_world_enter) {
            player_data.playerDataTable.wolf_boss_talk = true;

            dialogue.image_index = 5;
            dialogue.name = "화염을\n이끄는 늑대";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "모모타로 녀석 살살 상대해줄 테니 걱정하지 마.";
            dialogue.sentences[1] = "물론 풀파워로 당신과 싸울 생각은 없으니깐 말이죠 하하하";

            manager.Start_Dialogue(dialogue);
        }
        
        if (!player_data.playerDataTable.bird_boss_talk && world[6].is_world_enter) {
            player_data.playerDataTable.bird_boss_talk = true;

            dialogue.image_index = 6;
            dialogue.name = "날카로운\n번개를 두른 학";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "나는 너를 다치게 할 생각이 없어..";
            dialogue.sentences[1] = "그냥 번개로 아픔없이 보내줄게";

            manager.Start_Dialogue(dialogue);
        }

        if (!player_data.playerDataTable.monkey_boss_talk && world[7].is_world_enter) {
            player_data.playerDataTable.monkey_boss_talk = true;

            dialogue.image_index = 7;
            dialogue.name = "돌로 몸을\n둘러싼 고릴라";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "음";
            dialogue.sentences[1] = "어";

            manager.Start_Dialogue(dialogue);
        }
    }

    void Input_World()
    {
        world[0].world_name = "MainPlayRoom";
        world[1].world_name = "Play_Town";
        world[2].world_name = "Play_Stage_1";
        world[3].world_name = "Play_Stage_2";
        world[4].world_name = "Play_Stage_3";
        world[5].world_name = "Play_Boss_Wolf";
        world[6].world_name = "Play_Boss_Bird";
        world[7].world_name = "Play_Boss_Monkey";
        world[8].world_name = "Play_Boss_End";
        world[9].world_name = "Load";
    }

    public void Check_Scene_Enter()
    {
        scene_name = SceneManager.GetActiveScene().name;

        //Debug.Log(scene_name + "진입");
    }

    public void Check_Scene_Exit()
    {
        scene_name = SceneManager.GetActiveScene().name;
        //Debug.Log(scene_name + "나감");
    }

    public void Load_Scene(int number)
    {
        SceneManager.LoadScene(world[number].world_name);
    }
}
