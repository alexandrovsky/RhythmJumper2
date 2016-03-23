using UnityEngine;
using System;
using System.Collections.Generic;
using SynchronizerData;
using TileEditor;


public struct MapInfo {
	public TileMap map;
	public Vector2 size;
	public Vector2 tileSize {
		get {
			return map.TileSize;
		}
	}
	public Vector2 startPosition;

}

public class LevelGenerator : MonoBehaviour {

	public List<MapInfo> mapInfos;

	public GameObject[] maps;

	public TileMap map;

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




//	BeatCounter beatCounter;

	public List<GameObject> platforms;
	List<GameObject> beatObservers;


	void Start () {
//		map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
//		beatCounter = GameObject.Find("AudioManager").GetComponent<BeatCounter>();
		LoadMaps();

		SpawnPlayer( mapInfos[0].startPosition );


//		List<TileLayer> layers = map.TileLayers;
//		foreach(TileLayer layer in layers){
//			if(layer.name == "ObjectsLayer"){
//				foreach(Tile tile in layer.Tiles){
//					if(!tile) continue;
//					switch(tile.tag){
//					case "Start":
//						BeatSyncMove bsm = player.GetComponent<BeatSyncMove>();
//						bsm.startPosition = (Vector3.up * platformHeight/2) + tile.transform.position;
//						player.transform.position.Set(tile.transform.position.x,
//							tile.transform.position.y,
//							player.transform.position.z);
//						Camera.main.GetComponent<BeatSyncMove>().startPosition.Set(bsm.startPosition.x, 
//							bsm.startPosition.y, 
//							Camera.main.transform.position.z);
//						
//						break;
//					default: break;
//					}
//				}
//			}	
//		}


	}

	public void RespawnPlayer(){
		
	}
	void SpawnPlayer( Vector2 startPosition ) {
		BeatSyncMove bsm = player.GetComponent<BeatSyncMove>();
		bsm.startPosition = (Vector3.up * platformHeight * 2) + new Vector3(startPosition.x, startPosition.y);
		Debug.DrawLine(bsm.startPosition, bsm.startPosition + (Vector3.up * platformHeight * 1), Color.cyan, 10);
		player.transform.position.Set(bsm.startPosition.x,
			bsm.startPosition.y,
			player.transform.position.z);

		Camera.main.GetComponent<FollowCamera>().target = player;

//		Camera.main.GetComponent<BeatSyncMove>().startPosition.Set(bsm.startPosition.x, 
//			bsm.startPosition.y, 
//			Camera.main.transform.position.z);
	}

	void LoadMaps(){
		mapInfos = new List<MapInfo>();
		Vector3 levelSize = Vector3.zero;

		foreach(GameObject go in maps){
			GameObject mapObject = GameObject.Instantiate(go);
			TileMap m = mapObject.GetComponent<TileMap>();
			MapInfo info = GetMapInfo(m);
			mapInfos.Add(info);

			mapObject.transform.position = transform.position + levelSize;

			levelSize.x += info.size.x;
//			levelSize.y += info.size.y;

		}
	}

	MapInfo GetMapInfo(TileMap map){
		MapInfo info = new MapInfo();
		info.map = map;
		info.size = map.MapSize;



		List<TileLayer> layers = map.TileLayers;
		foreach(TileLayer layer in layers){
			if(layer.name == "ObjectsLayer"){
				foreach(Tile tile in layer.Tiles){
					if(!tile) continue;
					switch(tile.tag){
					case "Start":
						info.startPosition = tile.transform.position;
						break;
					default: break;
					}
				}
			}	
		}

		return info;

	} 






}
