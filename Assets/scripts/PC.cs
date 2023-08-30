using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PC : MonoBehaviour
{
    public static PC Instance;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool onDialogue;
    public Image dialogueImage;
    public Sprite[] characterSprites;
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && onDialogue == false)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                onDialogue = true;
            }
        }
        if (onDialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StopAllCoroutines();
                NextLine();
            }

        }
    }


    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        onDialogue = false;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        string nome = dialogue[index].Split(new char[] { ' ' })[0];
        switch (nome)
        {
            case "PC":
                dialogueImage.sprite = characterSprites[0];
                break;
            case "Voce":
                dialogueImage.sprite = characterSprites[1];
                break;
        }
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();
        }
    }


}
