using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clck_Event : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject credits;
    public AudioClip sound;
    private string object_name;
    private GameObject object_child;
    private Image_FaidOut image_faidout;
    private AudioSource audio;

    void Start()
    {
        image_faidout = GameObject.Find("System").GetComponent<Image_FaidOut>();
        audio = GameObject.Find("System").GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        object_name = transform.name;
        Scene_Event(object_name);
        Enter_Sound(object_name);
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        Transform child = this.transform.GetChild(0);
        child.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        Transform child = this.transform.GetChild(0);
        child.gameObject.SetActive(false);
    }

    void Scene_Event(string name)
    {
        if (name == "start")
        {
            StartCoroutine(Game_Start());
        }

        if (name == "credit") 
        {
            credits.SetActive(true);
        }

        if (name == "exit")
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    void Enter_Sound(string name)
    {
        if (name == "start" || name == "seting" || name == "credit" || name == "exit") {
            audio.PlayOneShot(sound);
        }
    }

    IEnumerator Game_Start()
    {
        image_faidout.Faid_Out();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("MainPlayRoom");
    }
}