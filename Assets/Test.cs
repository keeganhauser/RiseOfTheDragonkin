using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D rb;
    float velocity = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * velocity * Time.deltaTime;
    }
}
