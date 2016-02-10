using UnityEngine;
using System.Collections;

public enum PlatformType {
	Jump,
	Stage
}

public class Platform : MonoBehaviour {



	public PlatformType type;
	public float durationInBeats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
