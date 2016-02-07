using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	CharacterController controller;
	Vector3 moveDirection = Vector3.zero;

	LevelGenerator levelGenerator;
	float bounceHeight;
	float jumpHeight;

	void Start(){
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		controller = GetComponent<CharacterController>();
		bounceHeight = 0; // levelGenerator.stepWidth/2; // bounce height 1/16th;
		jumpHeight = levelGenerator.stepWidth;
	}

	void Update() {


		if(controller.isGrounded){
			moveDirection.y = bounceHeight;
			if (Input.GetButton("Jump")){
				moveDirection.y = jumpHeight * 2;
			}
		}else{
			moveDirection.y -= jumpHeight*4 * Time.deltaTime; // fall speed	
		}
		controller.Move(moveDirection * Time.deltaTime);
	}

}
