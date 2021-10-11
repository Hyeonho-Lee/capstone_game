using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boss_Bird))]
public class Boss_Bird_Button : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Boss_Bird patern = (Boss_Bird)target;
        if (GUILayout.Button("Patern_1")) {
            patern.Patern_1();
        }

        if (GUILayout.Button("Patern_2")) {
            patern.Patern_2();
        }

        if (GUILayout.Button("Patern_3")) {
            patern.Patern_3();
        }

        if (GUILayout.Button("Patern_4")) {
            patern.Patern_4();
        }
    }
}
