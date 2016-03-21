using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	Vector3 lastPos;
	Animator anim;
	void Start () {
		anim = GetComponent<Animator>();

		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		float speed = (lastPos - transform.position).magnitude;

		anim.SetFloat("Speed", speed);
		Debug.Log("Speed" + speed);
		lastPos = transform.position;

	}
}
