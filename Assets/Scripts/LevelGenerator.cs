using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {


	public float stepWidth { // width of a single beat
		get{
			return beatStep * platformTemplate.transform.localScale.x * beatStep;
		}
	}


	public float beatStep = 4;
	public float [] platformBeats;
	public GameObject platformTemplate;
	public GameObject triggerTemplate;

	BeatCounter beatCounter;

	List<GameObject> platforms;
	List<GameObject> beatObservers;


	void Start () {
		beatCounter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		beatObservers = new List<GameObject>();
		GenerateLevel();

		foreach(GameObject go in beatCounter.observers){
			beatObservers.Add(go);	
		}
		beatCounter.observers = beatObservers.ToArray();
	}
	
	// Update is called once per frame
	void GenerateLevel () {
	
		platforms = new List<GameObject>();
		for(int i = 0; i < platformBeats.Length; i++){
			GameObject platform = GameObject.Instantiate(platformTemplate);

			platform.transform.parent = transform;
			Vector3 pos = new Vector3(stepWidth * platformBeats[i], 0, 0);
			platform.transform.position = pos;
			beatObservers.Add(platform);

//			GameObject trigger = GameObject.Instantiate(triggerTemplate);
//			trigger.transform.position = platform.transform.position
//				+ new Vector3(platform.transform.localScale.x * 0.5f,
//				platform.transform.localScale.y, 0);
		}
		//

	}


}
