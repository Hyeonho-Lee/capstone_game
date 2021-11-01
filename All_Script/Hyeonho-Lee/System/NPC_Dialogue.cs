using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC_Dialogue
{
    public string name;
    public int image_index;

    [TextArea(5, 10)]
    public string[] sentences;
}
