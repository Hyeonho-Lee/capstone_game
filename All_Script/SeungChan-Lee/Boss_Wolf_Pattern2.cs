using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_Pattern2 : MonoBehaviour
{
    public GameObject pattern2_1;
    public GameObject pattern2_2;
    public GameObject pattern2_3;

    public float spawn_time = 5;
    
    void Start()
    {
        StartCoroutine(Change_Pattern(spawn_time));
    }

    IEnumerator Change_Pattern(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(pattern2_1);
        yield return new WaitForSeconds(delay);
        Instantiate(pattern2_2);
        yield return new WaitForSeconds(delay);
        Instantiate(pattern2_3);
    }
}
