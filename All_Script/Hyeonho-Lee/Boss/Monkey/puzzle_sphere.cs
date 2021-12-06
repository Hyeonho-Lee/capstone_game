using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_sphere : MonoBehaviour
{
    public int index;

    private GameObject puzzle;
    private puzzle puzzles;
    private SphereCollider colliders;

    void Start()
    {
        puzzle = GameObject.Find("puzzle(Clone)");
        puzzles = puzzle.GetComponent<puzzle>();
        colliders = GetComponent<SphereCollider>();
        if (!colliders.enabled) {
            colliders.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Attack") {
            colliders.enabled = false;
            puzzles.player_value.Add(index-1);
            if (index == 1) {
                puzzles.renderer_1.material = puzzles.attack_color;
            }else if (index == 2) {
                puzzles.renderer_2.material = puzzles.attack_color;
            } else if (index == 3) {
                puzzles.renderer_3.material = puzzles.attack_color;
            } else if (index == 4) {
                puzzles.renderer_4.material = puzzles.attack_color;
            }
        }
    }
}
