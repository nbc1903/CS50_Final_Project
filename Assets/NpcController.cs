using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    EventsController eventsController;
    public Dialogue dialogue;
    public GameObject enterKeySprite;


    private GameObject currentEnterKey;
    private bool talking = false;
    private bool canTalk = false;
    // Start is called before the first frame update
    void Start()
    {
        eventsController = GameObject.Find("Huntress").GetComponent<EventsController>();
        eventsController.OnPlayerInteract += OnPlayerInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            canTalk = true;
            currentEnterKey = Instantiate(enterKeySprite, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            canTalk = false;
            if (currentEnterKey)
            {
                Destroy(currentEnterKey);
            }
        }
    }

    void OnPlayerInteract(object sender, EventArgs e)
    {
        if (canTalk)
        {
            if (talking)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
            else{
                TriggerDialogue();
            }
            
        }
    }

    void TriggerDialogue()
    {
        if (currentEnterKey)
        {
            Destroy(currentEnterKey);
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        talking = true;
    }

}
