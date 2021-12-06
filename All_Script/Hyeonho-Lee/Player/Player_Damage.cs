using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);
        if (other.tag == "Attack") {
            if (!PlayerMovement.P.is_defence) {
                PlayerMovement.P.Player_Hit();
            } else {
                PlayerMovement.P.Player_Defence();
            }
        }
    }
}
