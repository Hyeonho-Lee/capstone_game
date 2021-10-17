using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Monkey : MonoBehaviour
{
    private Boss_Monkey_1 patern_1;
    private Boss_Monkey_2 patern_2;

    void Start()
    {
        patern_1 = GameObject.Find("Monkey_Patern_1").GetComponent<Boss_Monkey_1>();
        patern_2 = GameObject.Find("Monkey_Patern_2").GetComponent<Boss_Monkey_2>();
    }

    public void Patern_1()
    {
        patern_1.Attack();
    }

    public void Patern_2()
    {
        patern_2.Attack();
    }
}
