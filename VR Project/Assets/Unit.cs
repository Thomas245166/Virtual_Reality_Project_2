using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Vector3 destination;

    float speed = 2;

	// Use this for initialization
	void Start () {
        destination = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = destination-transform.position;
        Vector3 velocity = dir.normalized * speed * Time.deltaTime;

        //dont overshoot the target due to speed
        velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);

        transform.Translate(velocity);

	}
    void NextTurn()
    {
        // Set "destination" to be the position of the next tile
        // in our pathfinding queue.
    }
}
