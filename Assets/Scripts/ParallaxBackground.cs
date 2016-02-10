using UnityEngine;
using System.Collections;

public class ParallaxBackground : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;

	AudioSource syncAudioSrc;
	void Start () {
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
	}

	void Update () {
		
		float x = Mathf.Repeat (syncAudioSrc.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
