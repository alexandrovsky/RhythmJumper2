using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerDie : MonoBehaviour {

	// Use this for initialization
	public int lives = 3;


	AudioSource syncAudioSrc;
	BeatCounter beat;
	Rigidbody2D rb;
	BeatSyncMove syncMove;
	bool isDying = false;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		beat = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		syncMove = GetComponentInChildren<BeatSyncMove>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isDying){
			float sample = beat.CurrentSample;
			float period = beat.SamplePeriod;

			float t = sample - period * 16;


			isDying = false;

		}
	}

	void OnCollisionEnter2D (Collision2D collision) {

		if (!isDying && collision.gameObject.tag == "Die")
		{
			Debug.Log("Die");
			isDying = true;

			lives -= 1;

			if(lives > 0){
				syncAudioSrc.time = 0;
				transform.position = syncMove.startPosition;
			} else {
				
			}
		}


	}
}
