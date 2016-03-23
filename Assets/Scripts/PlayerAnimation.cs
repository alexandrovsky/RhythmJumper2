using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	Vector3 lastPos;
	Animator anim;

	AudioSource syncAudioSrc;

	void Start () {

		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();


		anim = GetComponent<Animator>();

		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	

		anim.SetBool("isRunning", syncAudioSrc.time > 0);


		float speed = (lastPos - transform.position).magnitude;

		anim.SetFloat("Speed", speed);
//		Debug.Log("Speed" + speed);
		lastPos = transform.position;

	}
}
