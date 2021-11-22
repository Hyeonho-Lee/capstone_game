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
        manager.Start_Dialogue(dialogue);
    }
}
