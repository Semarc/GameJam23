using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class endScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioScript.Instance.PlayMusic(2);
        AudioScript.Instance.PlayThrowSound();
        
    }

	// Update is called once per frame
	void Update()
	{

	}
}
