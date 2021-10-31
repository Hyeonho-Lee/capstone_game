using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby_Button : MonoBehaviour
{
    private Image_FaidOut image_faidout;


    void Start()
    {
        image_faidout = GameObject.Find("System").GetComponent<Image_FaidOut>();
    }

    public void UI_Start()
    {
        StartCoroutine(Game_Start());
    }

    IEnumerator Game_Start()
    {
        image_faidout.Faid_Out();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("MainPlayRoom");
    }

    public void UI_Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
