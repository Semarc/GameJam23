using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingDialogue : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject portrait;
    [SerializeField] TextMeshProUGUI name;

    //characters
    public string sin = "SIN";
    [SerializeField] Sprite sinPortrait;

    public string alla = "ALLA";
    [SerializeField] Sprite allaPortrait;

    public string kat = "KAT";
    [SerializeField] Sprite katPortrait;

    public string andy = "ANDY";
    [SerializeField] Sprite andyPortrait;
    public int index;

    private void Update()
    {
        index = canvas.GetComponent<DialogueManager>().index;
        // change character portrait and name to Kat
        if (index == 2)
        {
            name.text = kat;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = katPortrait;
        }

        // change character portrait and name to Alla
        if (index == 0)
        {
            name.text = alla;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = allaPortrait;
        }

        // change character portrait and name to Andy
        if (index == 3)
        {
            name.text = andy;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = andyPortrait;
        }

        // change character portrait and name to Sin
        if (index == 1)
        {
            name.text = sin;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = sinPortrait;
        }
    }

}
