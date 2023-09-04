using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CharactersRunning : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Transform alla;
    [SerializeField] Transform andy;
    [SerializeField] Transform kat;
    [SerializeField] Transform sin;
    [SerializeField] Transform follow;


    void Update()
    {
        alla.position += (Vector3)(Vector2.right * Time.deltaTime * movementSpeed);
        andy.position += (Vector3)(Vector2.right * Time.deltaTime * movementSpeed);
        kat.position += (Vector3)(Vector2.right * Time.deltaTime * movementSpeed);
        sin.position += (Vector3)(Vector2.right * Time.deltaTime * movementSpeed);
        follow.position += (Vector3)(Vector2.right * Time.deltaTime * movementSpeed);
    }
}
