using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    public void Move(GameObject player, float speed)
    {
        Vector3 move_dir = (player.transform.position - transform.position).normalized;
        transform.position += move_dir * speed * Time.deltaTime;
        Rotate(player);
    }

    public void Rotate(GameObject player)
    {
        Vector3 result = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(result);
    }
}
