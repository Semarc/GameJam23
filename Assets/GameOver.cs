using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AudioManager").PlayMusic(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
