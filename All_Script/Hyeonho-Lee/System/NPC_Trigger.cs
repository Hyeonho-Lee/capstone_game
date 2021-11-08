using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trigger : MonoBehaviour
{
    public NPC_Dialogue dialogue;
    private NPC_Manager manager;

    public void Dialogue_Trigger()
    {
        manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        manager.Start_Dialogue(dialogue);
    }
}
