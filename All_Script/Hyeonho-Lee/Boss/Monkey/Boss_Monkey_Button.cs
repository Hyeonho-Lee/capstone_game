using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boss_Monkey))]
public class Boss_Monkey_Button : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Boss_Monkey patern = (Boss_Monkey)target;
        if (GUILayout.Button("Patern_1")) {
            patern.Patern_1();
        }

        if (GUILayout.Button("Patern_2")) {
            patern.Patern_2();
        }
    }
}
