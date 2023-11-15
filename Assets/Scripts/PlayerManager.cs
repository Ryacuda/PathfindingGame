using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	// Attributes
	private bool is_moving;
	[SerializeField] private float move_time;
	[SerializeField] private MapManager mapmanager_instance;

	[SerializeField] private float base_movespeed;

	// Start is called before the first frame update
	void Start()
	{
		is_moving = false;
		mapmanager_instance = GameObject.Find("MapManager").GetComponent<MapManager>();
	}

	// Update is called once per frame
	void Update()
	{
		float x, y;

		x = Input.GetAxisRaw("Horizontal");

		// only sideways movement or up/down, not both at the same time
		if(x == 0f)
		{
			y = Input.GetAxisRaw("Vertical");
		}
        else
        {
			y = 0;
        }
		
		if(mapmanager_instance.GetWalkingSpeed(transform.position + new Vector3(x, y, 0)) != 0
           && !is_moving && (x != 0 || y != 0))
		{
            StartCoroutine(MovePlayer(new Vector3(x, y, 0)));
        }
	}

	IEnumerator MovePlayer(Vector3 movement)
	{
		is_moving = true;

		float next_move = 0;
		Vector3 start_position = transform.position;
		Vector3 end_position = start_position + movement;

		while(next_move < move_time)
		{
			transform.position = Vector3.Lerp(start_position, end_position, next_move / move_time);
			next_move += Time.deltaTime;
			yield return null;
		}

		transform.position = end_position;

		is_moving = false;

		float time_spent_moving = mapmanager_instance.GetWalkingSpeed(start_position) + mapmanager_instance.GetWalkingSpeed(end_position);
		time_spent_moving *= base_movespeed / 2;
		mapmanager_instance.IncrementAnimalTime(time_spent_moving);		// gives the animals in the scene the same amount of time to move
	}
}
