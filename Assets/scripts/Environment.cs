using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Environment : MonoBehaviour
{

    public float environment_speed = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Vector2.left * Time.deltaTime * environment_speed
        );
    }
  
}
