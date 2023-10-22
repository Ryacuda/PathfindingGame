using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;
using Unity.VisualScripting;


public class GraphRenderer : MonoBehaviour
{
	private Graph<string> graph;
	[SerializeField] private GameObject node_prefab;

	// Start is called before the first frame update
	void Start()
	{
		// initialize graph
		graph = Graph<string>.CreateTestGraph2();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Transparent/Diffuse"));

		lr.startColor = color;
		lr.endColor =  color;

		lr.startWidth = 0.1f;
		lr.endWidth = 0.1f;

		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}
}
