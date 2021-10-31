using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Interaction : MonoBehaviour
{
    public List<GameObject> eventList = new List<GameObject>(); // �������� �� ����Ʈ

    public GameObject item_ui;

    private GameObject player; // �÷��̾� ����
    public GameObject item_distance;

    PlayerMovement movement;
    TextMeshProUGUI textmesh;
    Animator ui_animator;

    void Start()
    {
        player = GameObject.Find("Player");
        movement = player.GetComponent<PlayerMovement>();
        transform.position = player.transform.position;
        transform.SetParent(player.transform);
    }

    void Update()
    {
        // F��ư�� ����� �ֺ��� �������� �ֳĿ� ���� ����
        if (Input.GetKeyDown(KeyCode.F) && eventList.Count > 0) {
            if (item_distance != null && movement.is_pick != true) {
                if(item_distance.tag == "Item") {
                    StartCoroutine(PickItem(item_distance, 1.2f));
                }

                if (item_distance.tag == "Door") {
                    Debug.Log("���� ���Ǵ�");
                }

                if (item_distance.tag == "NPC") {
                    Debug.Log("NPC ��ȭ��");
                }
            }
        }

        if (eventList.Count > 0) {
            item_distance = FindPickableItemClosestToPlayer();
            item_ui.SetActive(true);
            Check_Tag();
        } else {
            item_ui.SetActive(false);
        }
    }

    void Check_Tag()
    {
        if(item_distance.tag == "Item") {
            Change_Message("F �ݱ�");
        }

        if (item_distance.tag == "Door") {
            Change_Message("F ����");
        }

        if (item_distance.tag == "NPC") {
            Change_Message("F ��ȭ");
        }
    }

    void Change_Message(string message)
    {
        textmesh = GameObject.Find("interaction_text").GetComponent<TextMeshProUGUI>();
        textmesh.text = message.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������� ������ ������ ����Ʈ�� �߰�
        if (other.tag == "Item" || other.tag == "NPC" || other.tag == "Door") {
            eventList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �������� ������ ������ ����Ʈ���� ����
        if (other.tag == "Item" || other.tag == "NPC" || other.tag == "Door") {
            eventList.Remove(other.gameObject);
        }
    }

    IEnumerator PickItem(GameObject item, float delay)
    {
        eventList.Remove(item);
        Destroy(item.gameObject);
        movement.is_pick = true;
        movement.lock_attack = true;
        movement.lock_dash = true;
        ui_animator = GameObject.Find("drop_items").GetComponent<Animator>();
        ui_animator.Play("Drop");
        yield return new WaitForSeconds(delay);
        movement.is_pick = false;
        movement.lock_attack = false;
        movement.lock_dash = false;
    }

    private GameObject FindPickableItemClosestToView()
    {
        // �÷��̾ �ٶ󺸴� ������ �������� �Դ� �ڵ�

        Debug.Log("Start Find");


        GameObject itemToPick = null;
        float bestDotProduct = -1;

        /*
        foreach (PickableItem item in eventList)
        {
            float dot = Vector3.Dot(transform.forward, item.transform.position - transform.position);

            if(dot < bestDotProduct && dot > 0.866f)
            {
                Debug.Log(dot);

                itemToPick = item;
                bestDotProduct = dot;
            }
        }
        */

        return itemToPick;
    }

    private GameObject FindPickableItemClosestToPlayer()
    {
        // �÷��̾ ���� ����� �������� �Դ� �ڵ�

        GameObject itemToPick = null;
        float minDir = -1;

        foreach (GameObject item in eventList) {
            // �������� ����ִ� ����Ʈ���� �����۰� �÷��̾��� �Ÿ��� ���
            float dir = Vector3.Distance(transform.position, item.transform.position);

            // ù ��� �Ǵ� �ּ� �Ÿ����� ������ �ش� ���������� ����
            if (minDir > dir || minDir == -1) {
                //Debug.Log(dir);

                minDir = dir;
                itemToPick = item;
            }
        }

        return itemToPick;
    }
}
