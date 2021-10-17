using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf : MonoBehaviour
{
    private Boss_Wolf_1 patern_1;
    private Boss_Wolf_2 patern_2;
    private Boss_Wolf_3 patern_3;

    void Start()
    {
        patern_1 = GameObject.Find("Wolf_Patern_1").GetComponent<Boss_Wolf_1>();
        patern_2 = GameObject.Find("Wolf_Patern_2").GetComponent<Boss_Wolf_2>();
        patern_3 = GameObject.Find("Wolf_Patern_3").GetComponent<Boss_Wolf_3>();
    }

    public void Patern_1()
    {
        patern_1.Attack();
    }

    public void Patern_2()
    {
        patern_2.Attack();
    }

    public void Patern_3()
    {
        patern_3.Attack();
    }
}
