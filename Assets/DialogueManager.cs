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

    private int index;
    [SerializeField] GameObject blackscreen;
    [SerializeField] GameObject portrait;
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
        textComponent.text = string.Empty;
        StartDialogue();
    }
   void Update()
    {
        // change character portrait and name to Kat
        if (index == 0 | index == 1 | index == 4 || index == 6 || index == 18 || index == 22)
        {
            name.text = kat;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = katPortrait;
        }

        // change character portrait and name to Alla
        if(index == 2 | index == 3 | index == 17)
        {
            name.text = alla;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = allaPortrait;
        }

        // change character portrait and name to Andy
        if (index == 5 | index == 12 | index == 19 | index == 20)
        {
            name.text = andy;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = andyPortrait;
        }

        // change character portrait and name to Sin
        if (index == 7 | index == 8 | index == 9 | index == 10 | index == 11 | index == 13 | index == 14 | index == 15 | index == 16 | index == 21)
        {
            name.text = sin;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = sinPortrait;
        }

    }
    void StartDialogue()
    {
        index = 0;
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
            Skip();
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

    private IEnumerator WaitForLevelSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level 1");

    }
}
