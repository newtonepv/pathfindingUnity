using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust this value to control the jump force
    private BoxCollider2D boxCollider;
    public LayerMask floor;
    private Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        floor = LayerMask.GetMask("floor");
    }
    void Update(){
        if (boxCollider.IsTouchingLayers(floor))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
