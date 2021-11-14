using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour
{
    public Sprite[] change_image = new Sprite[3];
    public float[] test = new float[5];
    private float value_0;
    private float value_1;
    public Image[] health_image = new Image[5];

    public void Check_Health(float health)
    {
        if (health >= 0) {
            value_0 = Mathf.Floor(health / 2);
            value_1 = health % 2;

            if (value_0 == test.Length && value_1 == 0) {
                for (int i = 0; i < test.Length; i++) {
                    test[i] = 2;
                    health_image[i].sprite = change_image[2];
                }
            }else if (value_0 == 0 && value_1 == 0) {
                for (int i = 0; i < test.Length; i++) {
                    test[i] = 0;
                    health_image[i].sprite = change_image[0];
                }
            }else {
                for (int i = 0; i < value_0; i++) {
                    test[i] = 2;
                    health_image[i].sprite = change_image[2];
                }
                
                if (value_1 == 1) {

                    if ((int)value_0 == 0) {
                        test[0] = 1;
                        health_image[0].sprite = change_image[1];
                    } else {
                        test[(int)value_0] = 1;
                        health_image[(int)value_0].sprite = change_image[1];
                    }

                    for (int i = (int)value_0 + 1; i < test.Length; i++) {
                        test[i] = 0;
                        health_image[i].sprite = change_image[0];
                    }
                }
                
                if (value_1 == 0) {
                    for (int i = (int)value_0; i < test.Length; i++) {
                        test[i] = 0;
                        health_image[i].sprite = change_image[0];
                    }
                }
            }
        }
    }
}
