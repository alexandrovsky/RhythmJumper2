using UnityEngine;
using System.Collections;

public class JumpVelocityCalculator : MonoBehaviour {

	public float distanceToNextPlatform = 0;

	LevelGenerator levelGenerator;
	float maxRayDistance = 16;


	void Start () {
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
				
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
			Vector2.right,
			maxRayDistance * levelGenerator.platformWidth);
		if(hit.collider != null){
			distanceToNextPlatform = hit.distance;
			Debug.DrawLine(transform.position, hit.collider.transform.position, Color.red);
		}
	
	}
}
