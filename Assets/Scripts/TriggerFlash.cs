using UnityEngine;
using System.Collections;
using SynchronizerData;

public class TriggerFlash : MonoBehaviour {

	[Range(0f, 500f)]
	public float blinkDuration = 30f;	// milliseconds
	Renderer ren;


	void Start () {
		ren = GetComponent<Renderer>();
		ren.material.color = Color.green;
	}


	void OnTriggerEnter(Collider other) {
		
		ren.material.color = Color.red;
		StartCoroutine(FlashMaterial());
	}

	IEnumerator FlashMaterial() {
		yield return new WaitForSeconds(blinkDuration / 1000f);
		ren.material.color = Color.green;
	}
}
