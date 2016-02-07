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
			Debug.Log(idx);
			if(idx+1  > levelGenerator.platforms.Count) {
				return; // return if we are on the last platform
			}
			nextPlatform = levelGenerator.platforms[idx+1].transform;

			Debug.DrawLine(transform.position, nextPlatform.position, Color.red);

			Vector3 p = new Vector3(transform.position.x, 
				currentPlatform.transform.position.y, 
				currentPlatform.transform.position.z);

			distanceToNextPlaform = Vector3.Distance(p, nextPlatform.position);
			computedJumpHeight = distanceToNextPlaform;

			Vector3 centerBetweenPlatforms = (currentPlatform.transform.position + nextPlatform.transform.position) / 2;
			Vector3 centerTop = centerBetweenPlatforms + Vector3.up * computedJumpHeight;
			Debug.DrawLine(centerBetweenPlatforms, centerTop, Color.cyan);
			Debug.DrawLine(currentPlatform.transform.position, centerTop, Color.green);
			Debug.DrawLine(nextPlatform.transform.position, centerTop, Color.red);

			Vector3 newPos = transform.forward;

			if(transform.position.x < centerBetweenPlatforms.x) {

				float range = centerTop.x - currentPlatform.position.x;
				float t = (transform.position.x - currentPlatform.position.x) / range;
				Debug.Log("T:" + t);
				newPos.y = Mathf.Lerp(currentPlatform.position.y, centerTop.y, t);
//				transform.position = Vector3.MoveTowards(p, centerTop, Vector3.Distance(p, centerTop) * Time.deltaTime);
			}else {
				float range = nextPlatform.position.x - centerTop.x;
				float t = (transform.position.x - centerTop.x) / range;

				newPos.y = Mathf.Lerp(centerTop.y, nextPlatform.position.y, t);
			}
			newPos.y += startPosY; 

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
