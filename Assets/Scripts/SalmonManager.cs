using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonManager : MonoBehaviour
{
	public GameObject animal_instance;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// distance to animal
		Vector3 dv = animal_instance.transform.position - transform.position;
		float dist = Vector3.Dot(dv, dv);

		if (dist < 1)
		{
			// collect the fish and spawn another
			animal_instance.GetComponent<AnimalController>().SpawnSalmon();

			// destroy itself
			Destroy(gameObject); return;
		}
	}
}
