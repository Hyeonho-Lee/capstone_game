using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wolf_Effect : MonoBehaviour
{
    public GameObject skill_0_effect;
    public GameObject skill_1_1_effect;
    public GameObject skill_1_2_effect;
    public GameObject skill_2_1_effect;
    public GameObject skill_2_2_effect;
    public GameObject skill_2_3_effect;
    public GameObject skill_3_effect;

    public IEnumerator Effect_Delay_0(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawn = Instantiate(skill_0_effect, transform.position + new Vector3(0f, 2f, 0f), Quaternion.LookRotation(transform.forward) * Quaternion.Euler(90f, 0f, 0f));
        Destroy(spawn, 2.0f);
    }
}
