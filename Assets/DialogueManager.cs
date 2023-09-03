using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject blackscreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // continue button to continue dialogue
    public void Continue()
    {

    }

    // skip button to skip entire dialogue and load level scene
    public void Skip()
    {
        blackscreen.SetActive(true);
        StartCoroutine(WaitForLevelSceneLoad());
    }

    private IEnumerator WaitForLevelSceneLoad()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level 1");

    }
}
