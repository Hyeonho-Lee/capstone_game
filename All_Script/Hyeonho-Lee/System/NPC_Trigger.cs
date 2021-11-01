using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trigger : MonoBehaviour
{
    public NPC_Dialogue dialogue;

    public void Dialogue_Trigger()
    {
        FindObjectOfType<NPC_Manager>().Start_Dialogue(dialogue);
    }
}
