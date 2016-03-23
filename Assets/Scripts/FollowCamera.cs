using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public float interpVelocity;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		targetPos = transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (target)
		{

			Vector3 newPos = new Vector3();
			newPos.x = target.transform.position.x + offset.x;
			newPos.z = target.transform.position.z + offset.z;

			float yDistance = Mathf.Abs(target.transform.position.y - transform.position.y);

			newPos.y = Mathf.Lerp(transform.position.y, target.transform.position.y, interpVelocity * Time.deltaTime);

			transform.position = newPos;


//			Vector3 posNoZ = transform.position;
//			posNoZ.z = target.transform.position.z;
//
//			Vector3 targetDirection = (target.transform.position - posNoZ);
//
//			interpVelocity = targetDirection.magnitude * 5f;
//
//			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
//			transform.position = targetPos + offset; // Vector3.Lerp( transform.position, targetPos + offset, 1);

		}
	}
}

// Original post with image here  >  http://unity3diy.blogspot.com/2015/02/unity-2d-camera-follow-script.html