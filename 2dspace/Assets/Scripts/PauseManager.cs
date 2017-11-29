using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public bool onpause = false;
	public GameObject pausePanel;

	
	void Awake() {
		pausePanel.SetActive(false);
	}
	public void ResumeButton() {
		unPause();
		pausePanel.SetActive(false);
	}

	public void BackToMenu() {
		Destroy(GameManager.instance);
		Destroy(GameObject.Find("UIManager"));
		SceneManager.LoadScene("StartScene");
		Debug.Log("Back to menu !!");
	}

	public void ExitButton(){
		Application.Quit();
	}

	void FixedUpdate(){
		if(Input.GetKeyUp(KeyCode.P)) {
			if(!onpause){
				Pause();
			}
		}
	}

	public void Pause(){
		Debug.Log("OnPause !");
		Time.timeScale = 0;
		SoundManager.instance.efxSource.Pause();
		pausePanel.SetActive(true);
		//GameObject.Find("Player").
		onpause = true;
	}

	public void unPause() {
		Debug.Log("Offpause");
		Time.timeScale = 1;
		SoundManager.instance.efxSource.UnPause();
		onpause = false;
	}
}
