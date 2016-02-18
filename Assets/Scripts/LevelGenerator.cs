using UnityEngine;
using System;
using System.Collections.Generic;
using SynchronizerData;



public class LevelGenerator : MonoBehaviour {

	public RhythmSequence[] sequences;

	public float stepWidth { // width of a single beat
		get{
			return platformWidth; // * beatStep;
		}
	}
	public float platformWidth {
		get {
			return platform4Template.transform.localScale.x;
		}
	}

	public float platformHeight {
		get {
			return platform4Template.transform.localScale.y;
		}
	}


	public float beatStep = 4;
	public float [] platformBeats;

	public GameObject coinTemplate;
	public float coinOffsetY = 4;
	public int numOfCallRepeats = 1;
	public int numOfRespnseRepeats = 1;

	public GameObject platform1Template; // whole notes
	public GameObject platform2Template; // half notes
	public GameObject platform4Template; // quater notes
	public GameObject platform8Template; // eight notes
	public GameObject platform16Template;// sixteenth notes


	public GameObject stageTemplate;
	public GameObject triggerTemplate;

	BeatCounter beatCounter;

	public List<GameObject> platforms;
	List<GameObject> beatObservers;


	void Start () {
		beatCounter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		beatObservers = new List<GameObject>();
//		GenerateLevel();
		GenerateSequences();

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
		float beatValue = BeatDecimalValues.values[(int)sequence.beatValue];
		float width = 0.0f;
		switch (sequence.beatValue) {
		case BeatValue.WholeBeat:
			width = platform1Template.transform.localScale.x;
			break;
		case BeatValue.HalfBeat:
			width = platform2Template.transform.localScale.x;
			break;
		case BeatValue.QuarterBeat:
			width = platform4Template.transform.localScale.x;
			break;
		case BeatValue.EighthBeat:
			width = platform8Template.transform.localScale.x;
			break;
		case BeatValue.SixteenthBeat:
			width = platform16Template.transform.localScale.x;
			break;
		default:
			throw new System.Exception("Sequence Beat type currently not supported"); 
			break;
		}


		float sequenceLength = width * sequence.beats.Length; // / BeatDecimalValues.values[(int)sequence.beatValue] * sequence.beats.Length;

		// generate call...
		for (int k = 0; k < numOfCallRepeats; k++) {
			for(int i = 0; i < sequence.beats.Length; i++){
				float beat = sequence.beats[i];
				
				GameObject platform = null;
				
				
				platform = GameObject.Instantiate(platformForBeatValue(sequence.beatValue));
				
				platform.transform.parent = transform;
				Vector3 pos = new Vector3(sequenceLength * k + width * i, 
				                          transform.position.y, 0);
				platform.transform.position = pos;
				beatObservers.Add(platform);
				if(beat > 0.0f){
					GameObject coin = GameObject.Instantiate(coinTemplate);
					coin.transform.parent = platform.transform;
					coin.transform.position = pos + Vector3.up * coinOffsetY;
				}
			}
		}



		// generate response....
		for (int k = 0; k < numOfRespnseRepeats; k++) {
			for (int i = 0; i < sequence.beats.Length; i++) {
				float beat = sequence.beats[i];
				
				if(beat > 0.0f){
					GameObject platform = null;
					platform = GameObject.Instantiate(platformForBeatValue(sequence.beatValue));
					
					platform.transform.parent = transform;
					Vector3 pos = new Vector3(sequenceLength * numOfCallRepeats + sequenceLength * k  + width * i, 
					                          transform.position.y, 0);
					platform.transform.position = pos;
					beatObservers.Add(platform);
					platforms.Add(platform);
				}
			}
		}

	}


	GameObject platformForBeatValue(BeatValue value){
		GameObject platform = null;
		switch (value) {
		case BeatValue.WholeBeat:
			platform = platform1Template;
			break;
		case BeatValue.HalfBeat:
			platform = platform2Template;
			break;
		case BeatValue.QuarterBeat:
			platform = platform4Template;
			break;
		case BeatValue.EighthBeat:
			platform = platform8Template;
			break;
		case BeatValue.SixteenthBeat:
			platform = platform16Template;
			break;
		default:
			throw new System.Exception("Sequence Beat type currently not supported"); 
			break;
		}
		return platform;
	}

	// Update is called once per frame
	void GenerateLevel () {
	
		platforms = new List<GameObject>();
		for(int i = 0; i < platformBeats.Length; i++){
			GameObject platform = GameObject.Instantiate(platform1Template);

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
