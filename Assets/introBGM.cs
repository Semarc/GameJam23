using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioScript.Instance.AudioManagerEnabled = false;   
        AudioScript.Instance.AudioManagerEnabled = true;   
        AudioScript.Instance.PlayMusic(3);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
