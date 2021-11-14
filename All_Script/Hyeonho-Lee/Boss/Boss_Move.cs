using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    public void Move(GameObject player, float speed)
    {
        Vector3 move_dir = (player.transform.position - transform.position).normalized;
        transform.position += move_dir * speed * Time.deltaTime;
        transform.LookAt(player.transform);
    }

    public void Rotate(GameObject player)
    {
        transform.LookAt(player.transform);
    }
}
