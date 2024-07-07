using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 location;
    public Node(bool _walkable, Vector3 _location)
    {
        this.walkable = _walkable;
        this.location = _location;
    }
}
