using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject player_instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // distance to player
        Vector3 dv = player_instance.transform.position - transform.position;
		float dist = Vector3.Dot(dv, dv);

		if (dist < 1)
        {
            // collect the treasure and spawn another
            player_instance.GetComponent<PlayerManager>().SpawnTreasure();

            // increment score
            player_instance.GetComponent<PlayerManager>().score++;

            // destroy itself
			Destroy(gameObject); return;
        }
    }
}
