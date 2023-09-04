using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackscreenTrigger : MonoBehaviour
{
    [SerializeField] Image blackscreen;
    [SerializeField] float fadeSpeed;

    void OnTriggerEnter2D(Collider2D coll)
    {
        StartCoroutine(WaitForBlackScreen());
    }

    private IEnumerator WaitForBlackScreen()
    {
        while (blackscreen.color.a <= 1)
        {
            blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b, blackscreen.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }
}