using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    public GameObject result_1;
    public GameObject result_2;
    public GameObject result_3;
    public GameObject result_4;

    public Material color_1;
    public Material color_2;
    public Material color_3;
    public Material color_4;
    public Material attack_color;

    public Renderer renderer_1;
    public Renderer renderer_2;
    public Renderer renderer_3;
    public Renderer renderer_4;
    private Boss_Monkey_3 pattern_3;

    public List<int> rand_color = new List<int>();
    public List<int> rands_color = new List<int>();
    public List<int> answer_color = new List<int>();
    public List<int> player_value = new List<int>();

    void Start()
    {
        pattern_3 = GameObject.Find("Monkey_Patern_3").GetComponent<Boss_Monkey_3>();

        renderer_1 = result_1.GetComponent<Renderer>();
        renderer_2 = result_2.GetComponent<Renderer>();
        renderer_3 = result_3.GetComponent<Renderer>();
        renderer_4 = result_4.GetComponent<Renderer>();

        rand_colors(1, 4);

        if (rands_color[0] == 1) {
            renderer_1.material = color_1;
        } else if (rands_color[0] == 2) {
            renderer_1.material = color_2;
        } else if (rands_color[0] == 3) {
            renderer_1.material = color_3;
        } else if (rands_color[0] == 4) {
            renderer_1.material = color_4;
        }

        if (rands_color[1] == 1) {
            renderer_2.material = color_1;
        } else if (rands_color[1] == 2) {
            renderer_2.material = color_2;
        } else if (rands_color[1] == 3) {
            renderer_2.material = color_3;
        } else if (rands_color[1] == 4) {
            renderer_2.material = color_4;
        }

        if (rands_color[2] == 1) {
            renderer_3.material = color_1;
        } else if (rands_color[2] == 2) {
            renderer_3.material = color_2;
        } else if (rands_color[2] == 3) {
            renderer_3.material = color_3;
        } else if (rands_color[2] == 4) {
            renderer_3.material = color_4;
        }

        if (rands_color[3] == 1) {
            renderer_4.material = color_1;
        } else if (rands_color[3] == 2) {
            renderer_4.material = color_2;
        } else if (rands_color[3] == 3) {
            renderer_4.material = color_3;
        } else if (rands_color[3] == 4) {
            renderer_4.material = color_4;
        }

        for (int i = 0; i < 4; i++) {
            if (rands_color[i] == 1) {
                Debug.Log("맨 처음의 위치는 " + i);
                answer_color.Add(i);
            }
        }

        for (int j = 0; j < 4; j++) {
            if (rands_color[j] == 2) {
                Debug.Log("두번째의 위치는 " + j);
                answer_color.Add(j);
            }
        }

        for (int l = 0; l < 4; l++) {
            if (rands_color[l] == 3) {
                Debug.Log("세번째의 위치는 " + l);
                answer_color.Add(l);
            }
        }

        for (int m = 0; m < 4; m++) {
            if (rands_color[m] == 4) {
                Debug.Log("마지막 위치는 " + m);
                answer_color.Add(m);
            }
        }
    }

    void Update()
    {
        Check_answer();
    }

    void rand_colors(int min, int max)
    {
        for (int i = 1; i < 5; i++) {
            rand_color.Add(i);
        }

        for (int i = 1; i < 5; i++) {
            int currentNumber = Random.Range(1, rand_color.Count + 1);
            rands_color.Add(rand_color[currentNumber - 1]);
            rand_color.RemoveAt(currentNumber - 1);
        }
    }

    void Check_answer()
    {
        if (player_value.Count == 4) {
            if (player_value[0] == answer_color[0] &&
                player_value[1] == answer_color[1] &&
                player_value[2] == answer_color[2] &&
                player_value[3] == answer_color[3]) {
                pattern_3.is_puzzle_clear = true;
                Destroy(this.gameObject);
            } else {
                Debug.Log("뒤짐");
                Destroy(this.gameObject);
            }
        }
    }
}
