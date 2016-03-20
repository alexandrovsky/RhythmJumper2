using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {


	public float jumpHeight = 1;
	LevelGenerator levelGenerator;
	Rigidbody2D rb;
	JumpVelocityCalculator jumpCalculator;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		jumpCalculator = GetComponentInChildren<JumpVelocityCalculator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void OnCollisionStay2D(Collision2D coll ) // C#, type first, name in second
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Default") && (Input.GetButtonDown("Jump")))
		{

//			jumpCalculator.distanceToNextPlatform

			//Jump Script
//			rb.velocity = Vector2.up * jumpSpeed * levelGenerator.platformHeight;

			float jumpForce = Mathf.Sqrt(2f * Physics2D.gravity.magnitude * rb.gravityScale * jumpHeight) * rb.mass;
			rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}
