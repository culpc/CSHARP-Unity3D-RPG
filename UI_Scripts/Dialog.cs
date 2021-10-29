using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject DialogUI;
    public GameObject DialogConfirm;

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    void Start()
    {
        DialogConfirm.SetActive(false);
        DialogUI.SetActive(false);
    }

    private void Update()
    {
        if (DialogUI.activeSelf)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogConfirm.SetActive(true);

            if (Input.GetButtonDown("Use"))
            {
                DialogUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                StartCoroutine(Type());
            }
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            DialogConfirm.SetActive(false);
            DialogUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
         {
            textDisplay.text = "";
            continueButton.SetActive(false);
         }
        
    }
}
