using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField] string currentScene;

    public int index;
    [SerializeField] GameObject blackscreen;
    [SerializeField] GameObject portrait;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Button continueButton;

    //characters
    public string sin = "SIN";
    [SerializeField] Sprite sinPortrait;

    public string alla = "ALLA";
    [SerializeField] Sprite allaPortrait;

    public string kat = "KAT";
    [SerializeField] Sprite katPortrait;

    public string andy = "ANDY";
    [SerializeField] Sprite andyPortrait;

    void Start()
    {
        StartCoroutine(WaitForDialogueStart());
    }
    void StartDialogue()
    {
        //index = 0;
        StartCoroutine(TypeLine());
    }
    // continue button to continue dialogue
    public void Continue()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if(currentScene == "introduction")
            {
                Skip();
            }
            else if(currentScene == "ending")
            {
                SceneManager.LoadScene("End Screen");
            }
      
        }
    }

    // skip button to skip entire dialogue and load level scene
    public void Skip()
    {
        blackscreen.SetActive(true);
        StartCoroutine(WaitForLevelSceneLoad());
    }

    private IEnumerator TypeLine()
    {
        continueButton.interactable = false;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        continueButton.interactable = true;
    }

    private IEnumerator WaitForDialogueStart()
    {
        yield return new WaitForSeconds(1.5f);
        dialogueBox.SetActive(true);
        textComponent.text = string.Empty;
        index = 0;
        yield return new WaitForSeconds(1.0f);
        StartDialogue();
    }
    private IEnumerator WaitForLevelSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level 1");

    }
}
