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
            dialogue.name = "���";
            dialogue.sentences = new string[5];
            dialogue.sentences[0] = "���Ÿ��!!!! ���� ������ ���Ⱑ ã�ƿԾ� �Ф�";
            dialogue.sentences[1] = "���ڱ� ������ ���ϵ��� ������ �ߴµ� ��ȣ�ŵ��� ���ع��Ǿ�...\n�׷��� ���ָ� ��������.....";
            dialogue.sentences[2] = "���Ÿ��! �ε� ��ȣ�ŵ��� ���ָ� Ǯ�� ���ϵ��� �����Ĵٿ�!!!";
            dialogue.sentences[3] = "���� ������ ���� ������ �������� ��ȭ�� �ɸ� �ູ�� �ɾ��ٲ���.\n�׷��� �� ��ȭ�� �ɾ���!!";
            dialogue.sentences[4] = "Ȥ�ó� ������ �����ٸ� ������ ����ƺ񿡰� ��ȭ�� �ɾ��\n�׷� �̸�!";

            manager.Start_Dialogue(dialogue);
        }

        if (!player_data.playerDataTable.wolf_boss_talk && world[5].is_world_enter) {
            player_data.playerDataTable.wolf_boss_talk = true;

            dialogue.image_index = 5;
            dialogue.name = "ȭ����\n�̲��� ����";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "���Ÿ�� �༮ ��� ������� �״� �������� ��.";
            dialogue.sentences[1] = "���� Ǯ�Ŀ��� ��Ű� �ο� ������ �����ϱ� ������ ������";

            manager.Start_Dialogue(dialogue);
        }
        
        if (!player_data.playerDataTable.bird_boss_talk && world[6].is_world_enter) {
            player_data.playerDataTable.bird_boss_talk = true;

            dialogue.image_index = 6;
            dialogue.name = "��ī�ο�\n������ �θ� ��";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "���� �ʸ� ��ġ�� �� ������ ����..";
            dialogue.sentences[1] = "�׳� ������ ���ľ��� �����ٰ�";

            manager.Start_Dialogue(dialogue);
        }

        if (!player_data.playerDataTable.monkey_boss_talk && world[7].is_world_enter) {
            player_data.playerDataTable.monkey_boss_talk = true;

            dialogue.image_index = 7;
            dialogue.name = "���� ����\n�ѷ��� ����";
            dialogue.sentences = new string[2];
            dialogue.sentences[0] = "��";
            dialogue.sentences[1] = "��";

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

        //Debug.Log(scene_name + "����");
    }

    public void Check_Scene_Exit()
    {
        scene_name = SceneManager.GetActiveScene().name;
        //Debug.Log(scene_name + "����");
    }

    public void Load_Scene(int number)
    {
        SceneManager.LoadScene(world[number].world_name);
    }
}
