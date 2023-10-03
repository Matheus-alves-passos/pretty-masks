using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public GameObject pressPanel;
    public TMP_Text dialogueText;
    public string[] dialogue1, dialogue2, dialogue3,dialogue4,dialogue5, actualDialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool onDialogue;
    public bool cont;
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
                if(Player.Instance.dialogo1 == true)
                {
                    actualDialogue = dialogue1;
                }

                if (Player.Instance.dialogo2 == true)
                {
                    actualDialogue = dialogue2;
                }

                if (Player.Instance.dialogo3 == true)
                {
                    actualDialogue = dialogue3;
                }

                if (Player.Instance.dialogo4 == true)
                {
                    actualDialogue = dialogue4;
                }

                if (Player.Instance.dialogo5 == true)
                {
                    actualDialogue = dialogue5;
                }
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
        string nome = actualDialogue[index].Split(new char[] { ' ' })[0];
        switch (nome)
        {
            case "Thalita":
                dialogueImage.sprite = characterSprites[0];
                break;

            case "Thomas":
                dialogueImage.sprite = characterSprites[1];
                break;

            case "André":
                dialogueImage.sprite = characterSprites[2];
                break;

            case "PC":
                dialogueImage.sprite = characterSprites[3];
                break;

            case "M":
                dialogueImage.sprite = characterSprites[4];
                break;
        }
        foreach (char letter in actualDialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        if (index < actualDialogue.Length - 1)
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

}
