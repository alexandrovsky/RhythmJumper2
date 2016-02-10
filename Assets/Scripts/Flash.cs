using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Flash : MonoBehaviour {


	[Range(0f, 500f)]
	public float blinkDuration = 30f;	// milliseconds


	private Color defaultColor = new Color(1 ,1, 1, 1);
	private Color activeColor = new Color(1 ,0, 0, 1);


	private BeatObserver beatObserver;


	void Start () {
		beatObserver = GetComponent<BeatObserver>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat) {
			StartCoroutine(FlashMaterial());
		}
	}

	IEnumerator FlashMaterial() {
		GetComponent<Renderer>().material.color = activeColor;
		yield return new WaitForSeconds(blinkDuration / 1000f);
		GetComponent<Renderer>().material.color = defaultColor;
	}
}
