using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	private Animator animator;
	private bool open = false;
	// Use this for initialization
	void Start(){
		animator = GetComponent<Animator>();
	}

public bool isBoxOpen() {
	return open;
}
	public void openBox() {
				Debug.Log("Box opening!");
				open = true;
				animator.SetTrigger("Open");
			
			//playerDetected = true;
			//Destroy(other.gameObject);
		}
	public void lootBox() {
			if(open) {
				Debug.Log("Take my loot !");
			}
		}
	
}
