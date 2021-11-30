using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

// 플레이어 정보 테이블
[System.Serializable]
public class PlayerDataTable
{
    // 보스를 잡았는지 체크
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

    // 플레이어의 위치를 저장
    public float player_position_x;
    public float player_position_z;
}

public class PlayerData : MonoBehaviour
{
    // 다른 스크립트 파일에서 player_Data를 사용하기 위해 선언
    // 사용법 : 함수 안에서 Player_Data.player_Data.playerDataTable.bird_boss
    public static PlayerData player_Data;

    // 플레이어 정보 테이블
    public PlayerDataTable playerDataTable = new PlayerDataTable();
    private GameObject player; // 플레이어 정보

    string playerDataFileName = "PlayerDataJson.json"; // 파일 이름

    private void Start()
    {
        if (player_Data == null) {
            player_Data = this;
        }

        // 테스트를 위해 작성
        player = GameObject.Find("Player");
        //PlayerDataSave(player.transform);
        //PlayerDataLoad();
    }

    // 파일이 있는지 체크
    // 입력 값 : fileName : 파일 이름 작성, 경로는 Resources로 고정
    // 반환 값 : 입력한 파일이 있는지 체크하여 없으면 false, 있으면 true 반환
    public bool FileCheck(string fileName)
    {
        FileInfo fi = new FileInfo(Application.dataPath + "/Resources/" + fileName);

        if (fi.Exists) return true;
        else return false;
    }

    // 플레이어 저장을 위한 PlayerDataJson.json 파일 생성
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

    // 플레이어 데이터 저장을 위해 PlayerDataJson.json에 파일 저장
    [ContextMenu("PlayerDataSave")]
    public void PlayerDataSave()
    {
        if (!FileCheck(playerDataFileName)) {
            CreatPlayerData();
        }

        // 다른 스크립트에서 Player_Data.player_Data.playerDataTable.bird_boss 를 사용하여
        // 데이터를 변경할 경우 해당 내용을 그대로 저장

        playerDataTable.player_position_x = player.transform.position.x;
        playerDataTable.player_position_z = player.transform.position.z;

        JObject data = JObject.FromObject(playerDataTable);

        string text = Application.dataPath + "/Resources/" + playerDataFileName;
        File.WriteAllText(text, data.ToString());

        //Debug.Log(data.ToString());
    }

    // 플레이어 데이터 로드을 위해 PlayerDataJson.json의 정보를 가져옴
    [ContextMenu("PlayerDataLoad")]
    public void PlayerDataLoad()
    {
        if (!FileCheck(playerDataFileName)) {
            Debug.LogError("로드할 파일이 없습니다!!");
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
