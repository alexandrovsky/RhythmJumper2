using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour {
	


	LevelGenerator levelGenerator;

	public float jumpHeightMultiplier = 2.0f;
	public float computedJumpHeight;
	public float distanceToNextPlaform;
	public Transform currentPlatform;
	public Transform nextPlatform;
	public Animator playerAnimator;

	private AudioSource syncAudioSrc;
	float lastAudioPlayTime = 0;
	Vector3 p0, p1, p2;

	string animState;
	Ray ray = new Ray();



	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(ray.origin, ray.direction);
	}

	void Start(){
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		playerAnimator = GetComponentInChildren<Animator>();
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		playerAnimator.SetTrigger("Idle");
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log(other.tag);
 		currentPlatform = other.transform;
	}

	void Update() {
		AnimatorStateInfo animatorInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);



		if( syncAudioSrc.time == 0 ){
			if(!animatorInfo.IsName("Idle")){
				
			}
		}else {
			animState = "Walk";
		}



		if(ComputeJumpCurve()){
			Jump();
		}

		if(!animatorInfo.IsName(animState)){
			playerAnimator.SetTrigger(animState);
		}

		lastAudioPlayTime = syncAudioSrc.time;
	}

	bool ComputeJumpCurve(){
		int idx = 0; // levelGenerator.platforms.IndexOf(currentPlatform.gameObject);

		if(idx < 0) {
			return false;
		}

		if(idx  >= levelGenerator.platforms.Count-1) {
			return false; // return if we are on the last platform
		}

			
		nextPlatform = levelGenerator.platforms[idx+1].transform;


		Vector3 p = new Vector3(transform.position.x, 
			currentPlatform.transform.position.y, 
			currentPlatform.transform.position.z);
		

		Vector3 offset =  new Vector3(0, currentPlatform.transform.localScale.y/2, 0);

		p0 = currentPlatform.transform.position + offset;
		p2 = nextPlatform.transform.position + offset;

		distanceToNextPlaform = Vector3.Distance(p, p2);
		computedJumpHeight = distanceToNextPlaform * jumpHeightMultiplier;

		Vector3 centerBetweenPlatforms = (p0 + p2) / 2;
		Debug.DrawLine(p0, centerBetweenPlatforms, Color.green);
		Debug.DrawLine(p2, centerBetweenPlatforms, Color.red);
		Vector3 centerTop = centerBetweenPlatforms + Vector3.up * computedJumpHeight;

		p1 = centerTop;

		Debug.DrawLine(centerBetweenPlatforms, p1, Color.cyan);
		Debug.DrawLine(p0, p1, Color.green);
		Debug.DrawLine(p2, p1, Color.red);

			
		// do animation stuff:

		if(p.x > p0.x && p.x < p1.x){ // in first half of jump:
			animState = "Jump";
		}else if(p.x > p1.x && p.x < p2.x){ // in 2nd half of jump:
			animState = "Fall";
		}

		return true;
	}

	public void Jump() {
		Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		float range = nextPlatform.position.x - currentPlatform.position.x;
		float t = (transform.position.x - currentPlatform.position.x) / range;
		t = Mathf.Clamp01(t);

		newPos.y = (1-t) * (p0.y)+ 2 * (1-t)*t*(p1.y) + t*t * (p2.y);
		newPos.y += transform.localScale.y;

		transform.position = newPos;
	}

}
