using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_FaidOut : MonoBehaviour
{
    public GameObject image_gameobject;
    private Image image;
    private Color color;

    [Range(0, 1)]
    public float value;
    private float fadein_delay;
    private float fadeout_delay;
    private float time;

    public bool is_fadein;
    public bool is_fadeout;

    void Start()
    {
        fadein_delay = 4f;
        fadeout_delay = 4f;
        image = image_gameobject.GetComponent<Image>();
        color = image.color;
        Faid_In();
    }

    void Update()
    {
        Is_Fade();
    }

    void Is_Fade()
    {
        if (is_fadein)
        {
            time += Time.deltaTime / fadein_delay;
            value = Mathf.Lerp(1f, 0f, time);
            color.a = value;
            image.color = color;
            if (value <= 0f) 
            {
                image_gameobject.gameObject.SetActive(false);
            }
        }

        if (is_fadeout) 
        {
            time += Time.deltaTime / fadeout_delay;
            value = Mathf.Lerp(0f, 1f, time);
            color.a = value;
            image.color = color;
        }
    }

    [ContextMenu("Faid_In")]
    public void Faid_In()
    {
        if(!is_fadein) 
        {
            StartCoroutine(Faid_In(fadein_delay));
        }
    }

    [ContextMenu("Faid_Out")]
    public void Faid_Out()
    {
        if (!is_fadeout) {
            StartCoroutine(Faid_Out(fadeout_delay));
        }
    }

    IEnumerator Faid_In(float delay)
    {
        image_gameobject.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        is_fadein = true;
        value = 1f;
        time = 0f;
        yield return new WaitForSeconds(delay);
        is_fadein = false;
    }

    IEnumerator Faid_Out(float delay)
    {
        image_gameobject.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        is_fadeout = true;
        value = 0f;
        time = 0f;
        yield return new WaitForSeconds(delay);
        is_fadeout = false;
    }
}
