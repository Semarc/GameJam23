using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    [SerializeField] GameObject restartButton;

    void Start()
    {
        StartCoroutine(WaitForCredits());
    }
    void StartCredits()
    {
        index = 0;
        StartCoroutine(Type());
    }

    public void ContinueCredits()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(Type());
        }
        else
        {
            restartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Introductory Scene");
    }

    private IEnumerator Type()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        ContinueCredits();
    }

    private IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(1.5f);
        textComponent.text = string.Empty;
        StartCredits();
    }
}


