using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	
	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;

	public bool doingSetup = true;
	private bool disable = true;
	// UI
	private Text levelText;
	private Text levelTextInfo;
	private Text ammoTextInfo;
	private Text scoreText;
	private int score = 0;
	private int enemiesKilled = 0;
	private int ammo = 10;
	private int life = 6;
	private GameObject levelImage;
	//private List<Enemy> enemies;
	public int level = 1;
	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Scene Loaded");
		if(!disable){
			Debug.Log("Enable to add level");
			//Add one to our level number.
			level++;
			//Call InitGame to initialize our level.
			InitGame();
		}
	}
	void OnEnable()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}	
	void OnDisable()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
		//Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	void Awake() {
		Debug.Log("Awake");
		if(instance == null){
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	//Hides black image used between levels
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);
		
		//Set doingSetup to false allowing player to move again.
		doingSetup = false;
	}

	public void addScore(int pscore){
		score += pscore;
		scoreText.text = ""+score;
	}
	void InitGame(){
		Debug.Log("Start Init");
		//While doingSetup is true the player can't move, prevent player from moving while title card is up.
		doingSetup = true;
            
		//Get a reference to our image LevelImage by finding it by name.
		levelImage = GameObject.Find("LevelImage");
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		ammoTextInfo = GameObject.Find("AmmoText").GetComponent<Text>();
		ammoTextInfo.text =  ammo + "/40";
        scoreText.text = ""+score;
		//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		
		//Set the text of levelText to the string "Level" and append the current level number.
		levelText.text = "Level " + level;
		levelTextInfo = GameObject.Find("LevelTextInfo").GetComponent<Text>();
		levelTextInfo.text = "Level " + level;
		//Set levelImage to active blocking player's view of the game board during setup.
		levelImage.SetActive(true);
		
		//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Invoke("HideLevelImage", levelStartDelay);
		
		//Clear any Enemy objects in our List to prepare for next level.
		//enemies.Clear();
		Debug.Log("images loaded");
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.setupScene(level);
		Debug.Log("endInit");
	}
	void Start () {
		disable = false;
	}

	public void enemyKilled(){
		enemiesKilled++;
		score += 100;
		scoreText.text = ""+score;
	}
	public void GameOver() {
		//Set levelText to display number of levels passed and game over message
		levelText.text = "You died on floor " + level + ".";
		
		//Enable black background image gameObject.
		levelImage.SetActive(true);
		
		//Disable this GameManager.
		enabled = false;
	}

	public void setAmmo(int pammo) {
		ammo = pammo;
		ammoTextInfo.text = ammo + "/40";
	}
	public int getAmmo(){
		return ammo;
	}
	public int getLife(){
		return life;
	}
	public void setLife(int hp){
		life = hp;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		
	}
}
