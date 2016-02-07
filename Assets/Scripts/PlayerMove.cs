using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour {

	float startPosY;

	CharacterController controller;
	Vector3 moveDirection = Vector3.zero;

	LevelGenerator levelGenerator;
	float bounceHeight;
	public float jumpHeight;
	public float computedJumpHeight;
	public float distanceToNextPlaform;
	public Transform currentPlatform;
	public Transform nextPlatform;
	Ray ray = new Ray();

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(ray.origin, ray.direction);
	}

	void Start(){
		startPosY = transform.position.y;
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		controller = GetComponent<CharacterController>();
		bounceHeight = 0; // levelGenerator.stepWidth/2; // bounce height 1/16th;
		jumpHeight = levelGenerator.stepWidth;
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log(other.tag);
 		currentPlatform = other.transform;

	}

	void Update() {

		Debug.DrawLine(transform.position, currentPlatform.position, Color.green);
		int idx = levelGenerator.platforms.IndexOf(currentPlatform.gameObject);
		if(idx > -1){
			if(idx  >= levelGenerator.platforms.Count-1) {
				return; // return if we are on the last platform
			}
			nextPlatform = levelGenerator.platforms[idx+1].transform;


			Debug.DrawLine(transform.position, nextPlatform.position, Color.red);

			Vector3 p = new Vector3(transform.position.x, 
				currentPlatform.transform.position.y, 
				currentPlatform.transform.position.z);

			distanceToNextPlaform = Vector3.Distance(p, nextPlatform.position);
			computedJumpHeight = distanceToNextPlaform;



			Vector3 offset =  new Vector3(0, levelGenerator.platformHeight + transform.localScale.y, 0);

			Vector3 p0 = currentPlatform.transform.position + offset;
			Vector3 p2 = nextPlatform.transform.position + offset;

			Vector3 centerBetweenPlatforms = (p0 + p2) / 2;
			Vector3 centerTop = centerBetweenPlatforms + Vector3.up * computedJumpHeight;

			Vector3 p1 = centerTop + offset;


			Debug.DrawLine(centerBetweenPlatforms, p1, Color.cyan);
			Debug.DrawLine(p0, p1, Color.green);
			Debug.DrawLine(p2, p1, Color.red);


			Vector3 newPos = transform.forward;

//			if(transform.position.x < centerBetweenPlatforms.x) {
//
//				float range = centerTop.x - currentPlatform.position.x;
//				float t = (transform.position.x - currentPlatform.position.x) / range;
//				Debug.Log("T:" + t);
//				newPos.y = Mathf.Lerp(currentPlatform.position.y, centerTop.y, t);
//			}else {
//				float range = nextPlatform.position.x - centerTop.x;
//				float t = (transform.position.x - centerTop.x) / range;
//
//				newPos.y = Mathf.Lerp(centerTop.y, nextPlatform.position.y, t);
//			}
//			newPos.y += startPosY; 


			float range = nextPlatform.position.x - currentPlatform.position.x;
			float t = (transform.position.x - currentPlatform.position.x) / range;

			newPos.y = (1-t) * (p0.y)+ 2 * (1-t)*t*(p1.y) + t*t * (p2.y);



			transform.position = newPos;
		}






//		if(controller.isGrounded){
//			moveDirection.y = bounceHeight;
//			if (Input.GetButton("Jump")){
////				jumpHeight = computedJumpHeight;
//				moveDirection.y = jumpHeight;
//			}
//		}else{
////			moveDirection.y -= jumpHeight * 4 * Time.deltaTime; // fall speed	
//		}
//		controller.Move(moveDirection * Time.deltaTime);
	}

}
