using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {


	public float jumpHeight = 1;
	LevelGenerator levelGenerator;
	Rigidbody2D rb;
	JumpVelocityCalculator jumpCalculator;

	bool isJumping, doubleJump;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		jumpCalculator = GetComponentInChildren<JumpVelocityCalculator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isJumping && !doubleJump){
			
		}
	}

	void OnCollisionStay2D(Collision2D coll ) {
		
		if (coll.gameObject.layer == LayerMask.NameToLayer("Default") ) {
			if(GetJumpInput()){
				ApplyJump();
			}

		}
	}

	bool GetJumpInput(){
		return Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow);
	}

	void ApplyJump(){
		
		//rb.velocity = Vector2.up * jumpHeight * levelGenerator.platformHeight;

		float jumpForce = Mathf.Sqrt(2f * Physics2D.gravity.magnitude * rb.gravityScale * jumpHeight) * rb.mass;
		rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}

}
