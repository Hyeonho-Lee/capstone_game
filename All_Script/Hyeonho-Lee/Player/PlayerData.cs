using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

// �÷��̾� ���� ���̺�
[System.Serializable]
public class PlayerDataTable
{
    // ������ ��Ҵ��� üũ
    public bool bird_boss;
    public bool wolf_boss;
    public bool monkey_boss;
    public bool last_boss;
    public bool town;
    public bool stage_2;
    public bool is_stone_1;
    public bool is_stone_2;
    public bool is_stone_3;
    public bool tutorial_talk;
    public bool town_talk;
    public bool bird_boss_talk;
    public bool wolf_boss_talk;
    public bool monkey_boss_talk;
    public bool last_boss_talk;
    public bool first_scene;

    // �÷��̾��� ��ġ�� ����
    public float player_position_x;
    public float player_position_z;
}

public class PlayerData : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ ���Ͽ��� player_Data�� ����ϱ� ���� ����
    // ���� : �Լ� �ȿ��� Player_Data.player_Data.playerDataTable.bird_boss
    public static PlayerData player_Data;

    // �÷��̾� ���� ���̺�
    public PlayerDataTable playerDataTable = new PlayerDataTable();
    private GameObject player; // �÷��̾� ����

    string playerDataFileName = "PlayerDataJson.json"; // ���� �̸�

    private void Start()
    {
        if (player_Data == null) {
            player_Data = this;
        }

        // �׽�Ʈ�� ���� �ۼ�
        player = GameObject.Find("Player");
        //PlayerDataSave(player.transform);
        //PlayerDataLoad();
    }

    // ������ �ִ��� üũ
    // �Է� �� : fileName : ���� �̸� �ۼ�, ��δ� Resources�� ����
    // ��ȯ �� : �Է��� ������ �ִ��� üũ�Ͽ� ������ false, ������ true ��ȯ
    public bool FileCheck(string fileName)
    {
        FileInfo fi = new FileInfo(Application.dataPath + "/Resources/" + fileName);

        if (fi.Exists) return true;
        else return false;
    }

    // �÷��̾� ������ ���� PlayerDataJson.json ���� ����
    public void CreatPlayerData()
    {
        playerDataTable.bird_boss = false;
        playerDataTable.wolf_boss = false;
        playerDataTable.monkey_boss = false;
        playerDataTable.last_boss = false;
        playerDataTable.town = false;
        playerDataTable.stage_2 = false;
        playerDataTable.is_stone_1 = false;
        playerDataTable.is_stone_2 = false;
        playerDataTable.is_stone_3 = false;
        playerDataTable.tutorial_talk = false;
        playerDataTable.town_talk = false;
        playerDataTable.bird_boss_talk = false;
        playerDataTable.wolf_boss_talk = false;
        playerDataTable.monkey_boss_talk = false;
        playerDataTable.last_boss_talk = false;
        playerDataTable.first_scene = false;
        playerDataTable.player_position_x = player.transform.position.x;
        playerDataTable.player_position_z = player.transform.position.z;

        JObject player_data_json = JObject.FromObject(playerDataTable);

        string text = Application.dataPath + "/Resources/" + playerDataFileName;
        File.WriteAllText(text, player_data_json.ToString());

        Debug.Log(player_data_json.ToString());
    }

    // �÷��̾� ������ ������ ���� PlayerDataJson.json�� ���� ����
    [ContextMenu("PlayerDataSave")]
    public void PlayerDataSave()
    {
        if (!FileCheck(playerDataFileName)) {
            CreatPlayerData();
        }

        // �ٸ� ��ũ��Ʈ���� Player_Data.player_Data.playerDataTable.bird_boss �� ����Ͽ�
        // �����͸� ������ ��� �ش� ������ �״�� ����

        playerDataTable.player_position_x = player.transform.position.x;
        playerDataTable.player_position_z = player.transform.position.z;

        JObject data = JObject.FromObject(playerDataTable);

        string text = Application.dataPath + "/Resources/" + playerDataFileName;
        File.WriteAllText(text, data.ToString());

        //Debug.Log(data.ToString());
    }

    // �÷��̾� ������ �ε��� ���� PlayerDataJson.json�� ������ ������
    [ContextMenu("PlayerDataLoad")]
    public void PlayerDataLoad()
    {
        if (!FileCheck(playerDataFileName)) {
            Debug.LogError("�ε��� ������ �����ϴ�!!");
            return;
        }

        string text = Application.dataPath + "/Resources/" + playerDataFileName;
        string text_data = File.ReadAllText(text);

        JObject data = JObject.Parse(text_data);

        playerDataTable.bird_boss = bool.Parse(data["bird_boss"].ToString());
        playerDataTable.wolf_boss = bool.Parse(data["wolf_boss"].ToString());
        playerDataTable.monkey_boss = bool.Parse(data["monkey_boss"].ToString());
        playerDataTable.last_boss = bool.Parse(data["last_boss"].ToString());
        playerDataTable.town = bool.Parse(data["town"].ToString());
        playerDataTable.stage_2 = bool.Parse(data["stage_2"].ToString());
        playerDataTable.is_stone_1 = bool.Parse(data["is_stone_1"].ToString());
        playerDataTable.is_stone_2 = bool.Parse(data["is_stone_2"].ToString());
        playerDataTable.is_stone_3 = bool.Parse(data["is_stone_3"].ToString());
        playerDataTable.tutorial_talk = bool.Parse(data["tutorial_talk"].ToString());
        playerDataTable.town_talk = bool.Parse(data["town_talk"].ToString());
        playerDataTable.bird_boss_talk = bool.Parse(data["bird_boss_talk"].ToString());
        playerDataTable.wolf_boss_talk = bool.Parse(data["wolf_boss_talk"].ToString());
        playerDataTable.monkey_boss_talk = bool.Parse(data["monkey_boss_talk"].ToString());
        playerDataTable.last_boss_talk = bool.Parse(data["last_boss_talk"].ToString());
        playerDataTable.first_scene = bool.Parse(data["first_scene"].ToString());
        playerDataTable.player_position_x = float.Parse(data["player_position_x"].ToString());
        playerDataTable.player_position_z = float.Parse(data["player_position_z"].ToString());

        //Debug.Log(text_data.ToString());
    }
}
