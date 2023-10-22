using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node<T>
{
    public int id;
    public T node_data;
    
    // Constructor
    public Node(int id, in T data)
    {
        this.id = id;
        this.node_data = data;
    }
}

