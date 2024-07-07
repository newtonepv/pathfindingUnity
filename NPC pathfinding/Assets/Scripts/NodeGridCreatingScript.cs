using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NodeGridCreatingScript : MonoBehaviour
{
    private Node[,] nodeGrid;
    public Vector2 worldSize;
    public float nodeRadius;
    private LayerMask floorMask;
    float nodeDiameter;
    int numNodesX, numNodesY;

    void Awake()
    {
    }

    void Start()
    {
        floorMask = LayerMask.GetMask("floor");
        nodeDiameter = nodeRadius * 2;
        numNodesX = Mathf.RoundToInt(worldSize.x * 2 / nodeDiameter);
        numNodesY = Mathf.RoundToInt(worldSize.y * 2 / nodeDiameter);
        FillNodeGrid();
    }

    private void FillNodeGrid()
    {
        nodeGrid = new Node[numNodesX, numNodesY];
        Vector3 worldBottomLeft = transform.position - new Vector3(worldSize.x, worldSize.y, 0);
        for (int x = 0; x < numNodesX; x++)
        {
            for (int y = 0; y < numNodesY; y++)
            {
                Vector3 location = new Vector3(worldBottomLeft.x + ((nodeDiameter * x) + nodeRadius), worldBottomLeft.y + ((nodeDiameter * y) + nodeRadius), transform.position.z);
                
                bool walkable;
                if (CountRaycastCollisions(new Vector2(location.x, location.y), Vector2.left, worldSize.x * 100, floorMask) % 2 == 0)
                {
                    walkable = true;
                }
                else
                {
                    walkable= false;
                }
                

                nodeGrid[x, y] = new Node(walkable, location);
            }
        }
    }

    void Update()
    {
        FillNodeGrid();
    }

    void OnDrawGizmos()
    {
        DrawCube();
        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                Gizmos.color = n.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(n.location, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

    void DrawCube()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x - worldSize.x, transform.position.y - worldSize.y, transform.position.z), new Vector3(transform.position.x + worldSize.x, transform.position.y - worldSize.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - worldSize.x, transform.position.y - worldSize.y, transform.position.z), new Vector3(transform.position.x - worldSize.x, transform.position.y + worldSize.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - worldSize.x, transform.position.y + worldSize.y, transform.position.z), new Vector3(transform.position.x + worldSize.x, transform.position.y + worldSize.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + worldSize.x, transform.position.y - worldSize.y, transform.position.z), new Vector3(transform.position.x + worldSize.x, transform.position.y + worldSize.y, transform.position.z));
    }
    int CountRaycastCollisions(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, layerMask);
        return hits.Length;
    }
}
