using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {


	public float jumpHeight = 1;
	LevelGenerator levelGenerator;
	BeatSynchronizer syncronizer;
	Rigidbody2D rb;
	JumpVelocityCalculator jumpCalculator;

	bool isJumping, doubleJump;

	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		syncronizer = GameObject.Find("AudioManager").GetComponent<BeatSynchronizer>();
		jumpCalculator = GetComponentInChildren<JumpVelocityCalculator>();


		jumpHeight *= Mathf.Pow(syncronizer.bpm/60, 2);
		rb.gravityScale *= Mathf.Pow(syncronizer.bpm/60, 2);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isJumping && !doubleJump){
			if(GetDoubleJumpInput()){
				doubleJump = true;
				ApplyJump();
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll ) {
		
		if (coll.gameObject.layer == LayerMask.NameToLayer("Default") ) {
			isJumping = false;
			doubleJump = false;
			if(GetJumpInput()){
				ApplyJump();
			}
		}
	}

	bool GetJumpInput(){
		return Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow);
	}

	bool GetDoubleJumpInput(){
		return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow);
	}


	void ApplyJump(){
		
		//rb.velocity = Vector2.up * jumpHeight * levelGenerator.platformHeight;


		float jumpForce = Mathf.Sqrt(2f * Physics2D.gravity.magnitude * rb.gravityScale * jumpHeight) * rb.mass * 60.0f/syncronizer.bpm;
		rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		isJumping = true;
	}

}
