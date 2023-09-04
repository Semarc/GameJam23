using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class endScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioScript.Instance.PlayThrowSound();
        AudioScript.Instance.PlayMusic(2);
    }

	// Update is called once per frame
	void Update()
	{

	}
}
