using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillChunk : MonoBehaviour {

	// Use this for initialization
	public GameObject[] enemies;
	public GameObject[] boxes;
	public Transform[] positions;
	void Start () {
		foreach(Transform pos in positions) {
			GameObject toInstantiate = enemies[Random.Range(0,enemies.Length)];
			if(Random.Range(0,2) < 0.5+(ButtonManager.UI.difficulty/4)){
				Instantiate(toInstantiate, pos.position, Quaternion.identity);
			}
			else if (Random.Range(0,2) < 1){
				toInstantiate = boxes[Random.Range(0,boxes.Length)];
				Instantiate(toInstantiate, pos.position, Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
