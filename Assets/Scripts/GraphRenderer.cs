using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;

public class GraphRenderer : MonoBehaviour
{
    private Graph graph;
    [SerializeField] private GameObject node_prefab;

    // Start is called before the first frame update
    void Start()
    {
        graph = Graph.CreateTestGraph();

        graph.AddNode(new List<float> { 10, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity }
                      new List<float>);

        int n = graph.nodes.Count;
        float rayon = 5 * n / (2 * Mathf.PI);
        float angle = 2.0f * Mathf.PI / (float) n;

        for(int i = 0; i < n; i++)
        {
            Vector2 p = rayon * new Vector2(Mathf.Cos(i * angle), Mathf.Sin(i * angle));
            GameObject go = Instantiate(node_prefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
            go.GetComponent<Node>().n = i;
        }

        for(int i = 0; i < graph.connections.Count; i++)
        {
            Vector2 pi = rayon * new Vector2(Mathf.Cos(i * angle), Mathf.Sin(i * angle));
            for (int j = 0; j < graph.connections.Count; j++)
            {
                Vector2 pj = rayon * new Vector2(Mathf.Cos(j * angle), Mathf.Sin(j * angle));

                if (!float.IsPositiveInfinity(graph.connections[i][j]))
                {
                    DrawLine(pi, pj, Color.red);
                }
            }
        }
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
