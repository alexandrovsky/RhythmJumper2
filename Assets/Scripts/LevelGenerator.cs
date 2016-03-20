using UnityEngine;
using System;
using System.Collections.Generic;
using SynchronizerData;
using TileEditor;


public class LevelGenerator : MonoBehaviour {

	TileMap map;

	public GameObject player;
	public float stepWidth { // width of a single beat
		get{
			return platformWidth * beatStep;
		}
	}
	public float platformWidth {
		get {
			return map.TileSize.x;
		}
	}

	public float platformHeight {
		get {
			return map.TileSize.y;
		}
	}


	public float beatStep = 4; // number of tiles per beat




	BeatCounter beatCounter;

	public List<GameObject> platforms;
	List<GameObject> beatObservers;


	void Start () {
		map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
		beatCounter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		beatObservers = new List<GameObject>();



		List<TileLayer> layers = map.TileLayers;
		foreach(TileLayer layer in layers){
			if(layer.name == "ObjectsLayer"){
				foreach(Tile tile in layer.Tiles){
					if(!tile) continue;
					switch(tile.tag){
					case "Start":
						//player = GameObject.Instantiate(player);
						player.GetComponent<BeatSyncMove>().startPosition = (Vector3.up * platformHeight/2) + tile.transform.position;

						Camera.main.GetComponent<BeatSyncMove>().startPosition = player.GetComponent<BeatSyncMove>().startPosition;


//						Camera.main.GetComponent<FollowCamera>().target = player;
//						Camera.main.transform.parent = player.transform;

						beatObservers.Add(player);

						break;
					default: break;
					}
				}
			}	
		}


		foreach(GameObject go in beatCounter.observers){
			beatObservers.Add(go);	
		}
		beatCounter.observers = beatObservers.ToArray();

	}








}
