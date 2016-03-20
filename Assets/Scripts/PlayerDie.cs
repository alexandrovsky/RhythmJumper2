using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D collision) {

		if (collision.gameObject.tag == "Die")
		{
			Debug.Log("Die");
		}


	}
}
