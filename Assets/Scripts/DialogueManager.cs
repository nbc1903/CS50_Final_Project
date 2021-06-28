using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;


        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return false;
        
    }

    void EndDialogue()
    {
        Debug.Log("end of conversation");
    }
}
