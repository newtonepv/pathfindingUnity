using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float rayCastDistance = 1f;

    private RaycastHit2D hit;
    private bool isRayCastHittingACollider = false;
    private bool hitMessageShowed = false;
    private LayerMask floorMask;

    void Start()
    {
        // Set up the LayerMask using layer names
        floorMask = LayerMask.GetMask("floor");
    }

    void Update()
    {
        UpdateIsRayCastHittingACollider();
        ShowHitMessageOrNot();
    }

    void UpdateIsRayCastHittingACollider()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up, rayCastDistance, floorMask);
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
        if (isRayCastHittingACollider && !hitMessageShowed)
        {
            Debug.Log("Hit an obstacle: " + hit.collider.name);
            hitMessageShowed = true;
        }
        else if (!isRayCastHittingACollider)
        {
            hitMessageShowed = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.up * rayCastDistance);
    }
}
