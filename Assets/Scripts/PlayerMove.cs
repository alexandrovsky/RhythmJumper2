using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

//	public float speed = 6.0F;
//	public float jumpSpeed = 8.0F;
//	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	LevelGenerator lg;
	void Start(){
		lg = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
	}

	void Update() {
		
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection.y = 0;
//			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//			moveDirection = transform.TransformDirection(moveDirection);
//			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = lg.stepWidth*4; // jumpSpeed;
		} else{
			moveDirection.y -= lg.stepWidth*8 * Time.deltaTime;
		}

		controller.Move(moveDirection * Time.deltaTime);
	}
}
