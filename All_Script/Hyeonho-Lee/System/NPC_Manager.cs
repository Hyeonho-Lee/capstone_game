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

    private PlayerMovement movement;

    void Start()
    {
        sentences = new Queue<string>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void Start_Dialogue(NPC_Dialogue dialogue)
    {
        //Debug.Log("시작: " + dialogue.name);
        dialogue_ui.SetActive(true);
        image_input = images[dialogue.image_index];
        image_input.SetActive(true);
        movement.is_talk = true;

        TextMeshProUGUI textmesh = name.GetComponent<TextMeshProUGUI>();
        textmesh.text = dialogue.name.ToString();

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        Next_Sentence();
    }

    public void Next_Sentence()
    {
        if (sentences.Count == 0) 
        {
            End_Dialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        TextMeshProUGUI textmesh = text.GetComponent<TextMeshProUGUI>();
        textmesh.text = sentence.ToString();
        //Debug.Log(sentence);
    }

    void End_Dialogue()
    {
        //Debug.Log("대화 끝");
        dialogue_ui.SetActive(false);
        image_input.SetActive(false);
        movement.is_talk = false;
    }
}
