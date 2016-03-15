using UnityEngine;
using System.Collections;
using SynchronizerData;

public class BeatSyncMove : MonoBehaviour {

//	private BeatObserver beatObserver;
//	private BeatCounter counter;
	private BeatSynchronizer syncronizer;
	private BeatSyncMoveBehaviour moveBehaviour;
	private LevelGenerator levelGenerator;
	private AudioSource syncAudioSrc;
	public Vector3 startPosition;


	void Avake(){
		startPosition = transform.position;
	}

	void Start () {
//		beatObserver = GetComponent<BeatObserver>();
		syncronizer = GameObject.Find("AudioManager").GetComponent<BeatSynchronizer>();
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
//		counter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();


	}
	
	// Update is called once per frame
	void Update () {
		float t =  syncAudioSrc.time * syncronizer.bpm/60;
		transform.position =  new Vector3(startPosition.x + t * levelGenerator.stepWidth, 
			transform.position.y, 
			transform.position.z);
//		if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat) {
//			animator.SetTrigger("move");
//			float step = levelGenerator.stepWidth;
//			animator.SetFloat("stepWidth", step);
//
////			StartCoroutine(MoveSync());
//		}
	}

	IEnumerator MoveSync() {
//		transform.Translate(new Vector3(1, 0, 0) );
		yield return new WaitForSeconds( syncronizer.bpm / 1000f);
	}
}
