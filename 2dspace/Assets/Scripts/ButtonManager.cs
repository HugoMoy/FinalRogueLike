using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public float difficulty = 0;
	public float sizeMap = 0;


	public static ButtonManager UI = null;
	public GameObject options;
	public GameObject menu;
	
	public Text difText;
	public Text sizText;
	private string[] difficulties = {"Easy","Normal","Hardcore"};
	private string[] sizes = {"Small", "Medium", "Large"};

public void Awake(){
	if(UI == null){
			UI = this;
		}
		else if (UI != this){
			Destroy(gameObject);
		}
	options.SetActive(false);
	menu.SetActive(true);
	DontDestroyOnLoad(gameObject);
}

public void AdjustDifficulty(float pdif){
	if(pdif == 0) {
		difficulty = 1;
	} else if(pdif == 0.5f){
		difficulty = 2;
	} else {
		difficulty = 3;
	}
	difText.text = "Difficulty : " + difficulties[(int) (difficulty-1)];
	//Debug.Log("diff :" + difficulty);
	
	//Debug.Log((int) pdif+ "   "+pdif);
}

public void AdjustSize(float psiz) {
	if(psiz == 0) {
		sizeMap = 1;
	} else if(psiz == 0.5f){
		sizeMap = 2;
	} else {
		sizeMap = 3;
	}
	//Debug.Log(sizeMap);
	sizText.text = "Size : " + sizes[ (int) (sizeMap-1)];
	//Debug.Log("size :" + sizeMap);
}
public void OptionsButton() {
	options.SetActive(true);
	menu.SetActive(false);
}

public void BackToMenu() {
	options.SetActive(false);
	menu.SetActive(true);
	Debug.Log("Back to menu !!");
}

public void NewGameButton() {
	Debug.Log("New game !!!");
	SceneManager.LoadScene("scene1");
}
public void ExitButton(){
	Application.Quit();
}
}
