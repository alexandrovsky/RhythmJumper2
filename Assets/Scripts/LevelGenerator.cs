using UnityEngine;
using System.Collections.Generic;
using SynchronizerData;



public class LevelGenerator : MonoBehaviour {

	public RhythmSequence[] sequences;

	public float stepWidth { // width of a single beat
		get{
			return platformWidth * beatStep;
		}
	}
	public float platformWidth {
		get {
			return platformTemplate.transform.localScale.x;
		}
	}

	public float platformHeight {
		get {
			return platformTemplate.transform.localScale.y;
		}
	}


	public float beatStep = 4;
	public float [] platformBeats;
	public GameObject platformTemplate;
	public GameObject stageTemplate;
	public GameObject triggerTemplate;

	BeatCounter beatCounter;

	public List<GameObject> platforms;
	List<GameObject> beatObservers;


	void Start () {
		beatCounter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		beatObservers = new List<GameObject>();
		GenerateLevel();
//		GenerateSequences();
		foreach(GameObject go in beatCounter.observers){
			beatObservers.Add(go);	
		}
		beatCounter.observers = beatObservers.ToArray();
	}


	void GenerateSequences(){
		foreach(RhythmSequence seq in sequences){
			GenerateSequence(seq);
		}
	}


	void GenerateSequence(RhythmSequence sequence){
		Debug.Log(sequence.beatValue);

		float length = stepWidth; // / BeatDecimalValues.values[(int)sequence.beatValue] * sequence.beats.Length;

		// generate call...
		float width = platformWidth; // / BeatDecimalValues.values[(int)sequence.beatValue];
		for(int i = 0; i < sequence.beats.Length; i++){
			float beat = sequence.beats[i];

			GameObject platform = GameObject.Instantiate(platformTemplate);
			platform.transform.parent = transform;
			Vector3 pos = new Vector3(width * i, 
				transform.position.y, 0);
			platform.transform.position = pos;
			beatObservers.Add(platform);
			if(beat > 0.0f){

			}
		}

		// generate response....
	}

	// Update is called once per frame
	void GenerateLevel () {
	
		platforms = new List<GameObject>();
		for(int i = 0; i < platformBeats.Length; i++){
			GameObject platform = GameObject.Instantiate(platformTemplate);

			platform.transform.parent = transform;
			Vector3 pos = new Vector3(stepWidth * platformBeats[i], transform.position.y, 0);
//			Vector3 pos = new Vector3(stepWidth * i, transform.position.y, 0);
			platform.transform.position = pos;
			beatObservers.Add(platform);
			platforms.Add(platform);

//			// trigger
//			GameObject trigger = GameObject.Instantiate(triggerTemplate);
//			trigger.transform.position = platform.transform.position
//				+ new Vector3(platform.transform.localScale.x * 0.5f,
//				platform.transform.localScale.y, 0);
		}
		//

	}




}
