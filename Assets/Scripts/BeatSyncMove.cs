using UnityEngine;
using System.Collections;
using SynchronizerData;

public class BeatSyncMove : MonoBehaviour {

	private BeatObserver beatObserver;
	private BeatSynchronizer syncronizer;
	private Animator animator;
	private BeatSyncMoveBehaviour moveBehaviour;
	private LevelGenerator levelGenerator;
	private AudioSource syncAudioSrc;
	void Start () {
		beatObserver = GetComponent<BeatObserver>();
		syncronizer = GameObject.Find("AudioManager").GetComponent<BeatSynchronizer>();
		syncAudioSrc = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
		animator = GetComponent<Animator>();
		moveBehaviour = animator.GetBehaviour<BeatSyncMoveBehaviour>();
		moveBehaviour.go = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3( syncAudioSrc.time * levelGenerator.stepWidth, 
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
