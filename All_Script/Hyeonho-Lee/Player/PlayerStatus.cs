using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class PlayerStat
{
    public float health;
    public float move_speed;
    public float attack_speed;
    public float attack_damage;
    public int life_count;
    public int potion_count;
    public float potion_recovery;
}

public class PlayerStatus : MonoBehaviour
{
    public PlayerStat player_stat = new PlayerStat();

    void Start()
    {
        //Stat_Load();
    }

    [ContextMenu("Stat_Create")]
    public void Stat_Create()
    {
        JObject status = new JObject(
            new JProperty("health", 0),
            new JProperty("move_speed", 0),
            new JProperty("attack_speed", 0),
            new JProperty("attack_damage", 0),
            new JProperty("life_count", 0),
            new JProperty("potion_count", 0),
            new JProperty("potion_recovery", 0)
        );

        string text = Application.dataPath + "/Script/All_Script/Hyeonho-Lee/System/PlayerJson.json";
        File.WriteAllText(text, status.ToString());
    }

    [ContextMenu("Stat_Load")]
    public void Stat_Load()
    {
        string text = Application.dataPath + "/Script/All_Script/Hyeonho-Lee/System/PlayerJson.json";
        string text_data = File.ReadAllText(text);

        JObject data = JObject.Parse(text_data);

        player_stat.health = float.Parse(data["health"].ToString());
        player_stat.move_speed = float.Parse(data["move_speed"].ToString());
        player_stat.attack_speed = float.Parse(data["attack_speed"].ToString());
        player_stat.attack_damage = float.Parse(data["attack_damage"].ToString());
        player_stat.life_count = int.Parse(data["life_count"].ToString());
        player_stat.potion_count = int.Parse(data["potion_count"].ToString());
        player_stat.potion_recovery = float.Parse(data["potion_recovery"].ToString());
    }

    [ContextMenu("Stat_Save")]
    public void Stat_Save()
    {
        JObject status = new JObject(
            new JProperty("health", player_stat.health),
            new JProperty("move_speed", player_stat.move_speed),
            new JProperty("attack_speed", player_stat.attack_speed),
            new JProperty("attack_damage", player_stat.attack_damage),
            new JProperty("life_count", player_stat.life_count),
            new JProperty("potion_count", player_stat.potion_count),
            new JProperty("potion_recovery", player_stat.potion_recovery)
        );

        string text = Application.dataPath + "/Script/All_Script/Hyeonho-Lee/System/PlayerJson.json";
        File.WriteAllText(text, status.ToString());
    }


    public void Player_Damage()
    {
        player_stat.health -= 1f;
    }
}
