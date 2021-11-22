using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_Manager : MonoBehaviour
{
    public GameObject dialogue_ui;
    public GameObject name;
    public GameObject text;
    public List<GameObject> images = new List<GameObject>();
    private GameObject image_input;

    private Queue<string> sentences;

    private PlayerData player_data;
    private PlayerMovement movement;
    private TextMeshProUGUI text_mesh;

    public void Start_Dialogue(NPC_Dialogue dialogue)
    {
        sentences = new Queue<string>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //Debug.Log("시작: " + dialogue.name);
        dialogue_ui.SetActive(true);
        image_input = images[dialogue.image_index];
        image_input.SetActive(true);
        movement.is_talk = true;

        text_mesh = name.GetComponent<TextMeshProUGUI>();
        text_mesh.text = dialogue.name.ToString();

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        Next_Sentence();
        Talk_Trigger(dialogue.name);
    }

    public void Next_Sentence()
    {
        if (sentences.Count <= 0) {
            End_Dialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        text_mesh = text.GetComponent<TextMeshProUGUI>();
        StopAllCoroutines();
        StartCoroutine(delay(sentence));

        //text_mesh.text = sentence.ToString();
        //Debug.Log(sentence);
    }

    void End_Dialogue()
    {
        //Debug.Log("대화 끝");
        dialogue_ui.SetActive(false);
        image_input.SetActive(false);
        movement.is_talk = false;
    }

    IEnumerator delay(string sentence)
    {
        text_mesh.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            text_mesh.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Talk_Trigger(string npc_name)
    {
        if (npc_name == "복숭아 나무") {
            player_data = GameObject.Find("System").GetComponent<PlayerData>();
            player_data.PlayerDataSave();
            player_data.PlayerDataLoad();
        }
    }
}
