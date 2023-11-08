using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
	// Attributes
	public List<GraphNode> nodes;      // Connections
	public Vector2 position;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawSphere(position, 0.3f);
	}
}
