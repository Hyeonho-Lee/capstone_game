using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bird : MonoBehaviour
{
    private Boss_Bird_1 patern_1;
    private Boss_Bird_2 patern_2;
    private Boss_Bird_3 patern_3;
    private Boss_Bird_4 patern_4;

    void Start()
    {
        patern_1 = GameObject.Find("Bird_Patern_1").GetComponent<Boss_Bird_1>();
        patern_2 = GameObject.Find("Bird_Patern_2").GetComponent<Boss_Bird_2>();
        patern_3 = GameObject.Find("Bird_Patern_3").GetComponent<Boss_Bird_3>();
        patern_4 = GameObject.Find("Bird_Patern_4").GetComponent<Boss_Bird_4>();
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

    public void Patern_4()
    {
        patern_4.Attack();
    }
}
