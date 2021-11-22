using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clck_Event : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string object_name;
    private GameObject object_child;
    private Image_FaidOut image_faidout;

    void Start()
    {
        image_faidout = GameObject.Find("System").GetComponent<Image_FaidOut>();
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        object_name = transform.name;
        Scene_Event(object_name);
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

        if (name == "exit")
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    IEnumerator Game_Start()
    {
        image_faidout.Faid_Out();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Load");
    }
}