using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IntroductoryScene : MonoBehaviour
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
        if (index == 0 | index == 1 | index == 4 || index == 6 || index == 19 || index == 24)
        {
            name.text = kat;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = katPortrait;
        }

        // change character portrait and name to Alla
        if (index == 2 | index == 3 | index == 18)
        {
            name.text = alla;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = allaPortrait;
        }

        // change character portrait and name to Andy
        if (index == 5 | index == 12 | index == 20 | index == 21 | index == 22)
        {
            name.text = andy;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = andyPortrait;
        }

        // change character portrait and name to Sin
        if (index == 7 | index == 8 | index == 9 | index == 10 | index == 11 | index == 13 | index == 14 | index == 15 | index == 16 | index == 17 | index == 23)
        {
            name.text = sin;
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = sinPortrait;
        }
        
        if (index == 8)
        {

        }
    }

}