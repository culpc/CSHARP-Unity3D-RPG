using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    
    public GameObject DialogConfirm;
    public GameObject ContButton;

    public Text nameText;
    public Text DialougeText;

    public Animator DialogueAnimator;

    // Start is called before the first frame update
    void Start()
    {
        DialogConfirm.SetActive(false);

        sentences = new Queue<string>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogConfirm.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            DialogConfirm.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueAnimator.SetBool("DialogueIsOpen", true);

        nameText.text = dialogue.name;
        // Fix to allow text messh pro 

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialougeText.text = "";
        // fix to allow text mesh pro

        foreach(char letter in sentence.ToCharArray())
        {
            DialougeText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        DialogueAnimator.SetBool("DialogueIsOpen", false);
    }
}
