using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class GraphNode<T> : MonoBehaviour
{
	public int id;
	public T node_data;
	private List<GraphNode<T>> adjacency_list;      // neighbours
	private List<float> costs_list;					// cost associated with neighbours
	
	// Constructor
	public GraphNode(int id, in T data)
	{
		this.id = id;
		this.node_data = data;
		
		adjacency_list = new List<GraphNode<T>>();
		costs_list = new List<float>();
	}

	// Methods
	public void AddConnection(ref GraphNode<T> node, float cost)
	{
		adjacency_list.Add(node);
		costs_list.Add(cost);
	}


}

