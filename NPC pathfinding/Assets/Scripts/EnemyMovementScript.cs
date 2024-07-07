using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust this value to control the jump force
    public float rayCastDistance = 1f;

    private BoxCollider2D boxCollider;
    private LayerMask floor;
    private Rigidbody2D rb;
    private RaycastHit2D hit;

    private bool isGrounded = false;
    private bool isRayCastHittingACollider;
    private bool hitMessageShowed = false;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        floor = LayerMask.GetMask("floor");
    }
    void Update(){
        if (isGrounded)
        {
            Jump();
        }

        updateIsRayCastHittingACollider();
        ShowHitMessageOrNot();

    }

    void updateIsRayCastHittingACollider()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up, rayCastDistance, floor);
        if (hit.collider != null)
        {
            isRayCastHittingACollider = true;
        }
        else
        {
            isRayCastHittingACollider = false;
        }
    }

    void ShowHitMessageOrNot()
    {
        if( isRayCastHittingACollider && !hitMessageShowed)
        {
            Debug.Log("Hit an obstacle: ");
            hitMessageShowed = true;
        }
        else if(!isRayCastHittingACollider)
        {
            hitMessageShowed=false;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            isGrounded = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; 
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.up * rayCastDistance);
        }
    }

}
