using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Interaction : MonoBehaviour
{
    public List<GameObject> eventList = new List<GameObject>(); // 아이템이 들어갈 리스트

    public GameObject item_ui;
    public GameObject talk_effect;
    private GameObject talk;

    private GameObject player; // 플레이어 정보
    public GameObject item_distance;

    private bool is_effect;
    private bool is_area;

    PlayerMovement movement;
    NPC_Manager npc_manager;
    TextMeshProUGUI textmesh;
    Animator ui_animator;
    SphereCollider collider;

    void Start()
    {
        npc_manager = GameObject.Find("System").GetComponent<NPC_Manager>();
        player = GameObject.Find("Player");
        movement = player.GetComponent<PlayerMovement>();
        collider = GetComponent<SphereCollider>();
        transform.position = player.transform.position + new Vector3(0f, 1.2f, 0f);
        transform.SetParent(player.transform);
    }

    void Update()
    {
        if (is_area) {
            collider.enabled = true;
        }else {
            collider.enabled = false;
        }

        // F버튼을 누루면 주변에 아이템이 있냐에 따라 실행
        if (Input.GetKeyDown(KeyCode.F) && !movement.is_inventory && !movement.is_talk) {
            StartCoroutine(Check_Area(1.0f));

            if (item_distance != null && !movement.is_pick && eventList.Count > 0) {
                
                if(item_distance.tag == "Item") {
                    item_distance = FindPickableItemClosestToPlayer();
                    StartCoroutine(PickItem(item_distance, 1.2f));
                }

                if (item_distance.tag == "Door") {
                    Debug.Log("문이 열렷다");
                }
                
                if (item_distance.tag == "NPC" || item_distance.tag == "Shop") {
                    item_distance = FindPickableItemClosestToPlayer();
                    NPC_Trigger trigger = item_distance.GetComponent<NPC_Trigger>();
                    trigger.Dialogue_Trigger(); trigger.Dialogue_Trigger();
                    //Debug.Log("NPC 대화중");
                }
            }
        }

        if (item_distance != null && !is_effect) {
            is_effect = true;
            if (talk == null) {
                talk = Instantiate(talk_effect, item_distance.transform);
            }
            talk.transform.position = item_distance.transform.position + new Vector3(0f, -1.2f, 0f);
        }

        if (eventList.Count > 0) {
            item_distance = FindPickableItemClosestToPlayer();

            if (talk != null) {
                if (item_distance.tag == "Item") {
                    talk.transform.position = item_distance.transform.position + new Vector3(0f, -0.5f, 0f);
                    talk.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                if (item_distance.tag == "NPC") {
                    talk.transform.position = item_distance.transform.position + new Vector3(0f, -1.2f, 0f);
                    talk.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }
                if (item_distance.tag == "Shop") {
                    talk.transform.position = item_distance.transform.position + new Vector3(0f, -0.1f, 0f);
                    talk.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }

                if (GameObject.FindGameObjectsWithTag("Talk_Effect").Length != 1) {
                    Destroy(GameObject.FindGameObjectsWithTag("Talk_Effect")[1]);
                }
            }

            if (item_ui != null) {
                item_ui.SetActive(true);
            }
            Check_Tag();
        } else {
            if(item_ui != null) {
                item_ui.SetActive(false);
            }
        }

        if (eventList.Count <= 0) {
            item_distance = null;
            is_effect = false;
            Destroy(talk);
        }
    }

    void Check_Tag()
    {
        if(item_distance.tag == "Item") {
            Change_Message("F 줍기");
        }

        if (item_distance.tag == "Door") {
            Change_Message("F 열기");
        }

        if (item_distance.tag == "NPC" || item_distance.tag == "Shop") {
            Change_Message("F 대화");
        }
    }

    void Change_Message(string message)
    {
        textmesh = GameObject.Find("interaction_text").GetComponent<TextMeshProUGUI>();
        textmesh.text = message.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 아이템이 들어오면 아이템 리스트에 추가
        if (other.tag == "Item" || other.tag == "NPC" || other.tag == "Door" || other.tag == "Shop") {
            eventList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 아이템이 들어오면 아이템 리스트에서 제거
        if (other.tag == "Item" || other.tag == "NPC" || other.tag == "Door" || other.tag == "Shop") {
            eventList.Remove(other.gameObject);
        }
    }

    void Remove_Trigger()
    {
        eventList = new List<GameObject>();
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
        // 플레이어가 바라보는 방향의 아이템을 먹는 코드

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
        // 플레이어에 제일 가까운 아이템을 먹는 코드

        GameObject itemToPick = null;
        float minDir = -1;

        foreach (GameObject item in eventList) {
            // 아이템이 들어있는 리스트에서 아이템과 플레이어의 거리를 계산
            float dir = Vector3.Distance(transform.position, item.transform.position);

            // 첫 계산 또는 최소 거리보다 가까우면 해당 아이템으로 변경
            if (minDir > dir || minDir == -1) {
                //Debug.Log(dir);

                minDir = dir;
                itemToPick = item;
            }
        }

        return itemToPick;
    }

    IEnumerator Check_Area(float delay)
    {
        if (!is_area) {
            is_area = true;
            yield return new WaitForSeconds(delay);
            is_area = false;
            Remove_Trigger();
        }
    }
}
