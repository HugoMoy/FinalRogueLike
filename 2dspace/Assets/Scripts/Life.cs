using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

	// Use this for initialization
	public Sprite hp0;
	public Sprite hp1;
	public Sprite hp2;
	public Sprite hp3;
	public Sprite hp4;
	public Sprite hp5;
	public Sprite hp6;


	void Start () {
		
	}

	public void changeHp(int hp) {
		switch(hp) {
			case 0 :
				gameObject.GetComponent<Image>().sprite = hp0;
				//this.GetComponent<SpriteRenderer>().sprite = hp0;
			break;
			case 1 : 
				gameObject.GetComponent<Image>().sprite = hp1;
			break;
			case 2 :
				gameObject.GetComponent<Image>().sprite = hp2;
			break;
			case 3 :
				gameObject.GetComponent<Image>().sprite = hp3;
			break;
			case 4 : 
				gameObject.GetComponent<Image>().sprite = hp4;
			break;
			case 5 :
				gameObject.GetComponent<Image>().sprite = hp5;
			break;
			case 6 : 
				gameObject.GetComponent<Image>().sprite = hp6;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
