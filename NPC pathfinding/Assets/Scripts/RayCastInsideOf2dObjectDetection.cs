using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInsideOf2dObjectDetection : MonoBehaviour
{
    public Vector2 rayCastOrigin;
    public Vector2 rayCastDirection;
    public float rayCastDistance;
    private LayerMask floor;

    // Start is called before the first frame update
    void Start()
    {
        floor = LayerMask.GetMask("floor");
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(CountRaycastCollisions(rayCastOrigin, rayCastDirection, rayCastDistance, floor));
        Debug.DrawRay(rayCastOrigin, rayCastDirection * rayCastDistance, Color.yellow);

    }
    void OnDrawGizmos()
    {
    }
    int CountRaycastCollisions(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, layerMask);
        return hits.Length;
    }
}
