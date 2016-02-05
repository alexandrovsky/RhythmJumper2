using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	CharacterController controller;
	Vector3 moveDirection = Vector3.zero;

	LevelGenerator levelGenerator;
	AudioSource syncAudioSrc;

	void Start(){
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		controller = GetComponent<CharacterController>();
	}

	void Update() {


		if (controller.isGrounded) {
			moveDirection.y = 0;
			if (Input.GetButton("Jump"))
				moveDirection.y = levelGenerator.stepWidth*3; // jumpSpeed;
		} else{
			
			moveDirection.y -= levelGenerator.stepWidth*6 * Time.deltaTime; // fall speed
		}
		controller.Move(moveDirection * Time.deltaTime);
	}

}
