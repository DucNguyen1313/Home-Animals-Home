using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveableObject : MonoBehaviour
{
    public float pushForce = 10f; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction)
    {
        rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
    }
}


