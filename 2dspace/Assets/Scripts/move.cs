using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour {

public GameObject bullet;
public float restartLevelDelay = 1f;
public Rigidbody2D playerbody;
private int hp = 6;
private int maxHp = 6;

private int ammo = 10;
private int maxAmmo = 40;
private bool invincible = false;
private float invincibleTime = 2f;
private float invincibleDate;
private Animator animator;
private float lastShotDate;
private float delayBetweenShots = 0.5f;
// Sound 
public AudioClip walk1;
public AudioClip walk2;
public AudioClip shootSound;
public AudioClip hurt;
public AudioClip teleport;
public AudioClip silence;
public AudioClip heal;



//

	// Use this for initialization
	void Start () {
		hp = GameManager.instance.getLife();
		Debug.Log("next level hp "+ hp);
		GameObject.Find("LifeImage").GetComponent<Life>().changeHp(hp);
		ammo = GameManager.instance.getAmmo();
		playerbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	private void Restart() {
		SceneManager.LoadScene (0);
	}
	private void OnDisable(){
		// what we store in the game manager when changing levels
		// GameManager.instance.points = points;
	}
	// Update is called once per frame
	void Update () {
		
	}
	void LoseHp(){
		hp--;
		GameObject.Find("LifeImage").GetComponent<Life>().changeHp(hp);
		GameManager.instance.setLife(hp);
		if(hp <= 0) {
			GameManager.instance.GameOver();
		}
	}
	bool AddHp(){
		if (hp < maxHp){
			hp++;
			GameManager.instance.setLife(hp);
			GameObject.Find("LifeImage").GetComponent<Life>().changeHp(hp);
			GameManager.instance.addScore(10);
			SoundManager.instance.PlaySingle(heal);
			return true;
		}
		return false;
	}
	bool AddAmmo(){
		if (ammo < maxAmmo-5){
			ammo = ammo+5;
			GameManager.instance.setAmmo(ammo);
			GameManager.instance.addScore(10);
			return true;
		}
		else if (ammo < maxAmmo){
			ammo = maxAmmo;
			GameManager.instance.setAmmo(ammo);
			GameManager.instance.addScore(10);
		}
		return false;
	}
	bool SpendAmmo(){
		if(ammo > 0){
			GameManager.instance.setAmmo(ammo);
			ammo--;
			return true;
		}
		return false;
	}
	void OnTriggerEnter2D(Collider2D other) {
		//Check if the tag of the trigger collided with is Exit.
		
            if(other.tag == "Exit")
            {
				GameManager.instance.addScore(500);
				playerbody.velocity = new Vector2(0,0);
				playerbody.position = other.transform.position;
                //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
                Invoke ("Restart", restartLevelDelay);
                
                //Disable the player object since level is over.
                enabled = false;
            }
			if(other.tag == "Enemy") {
				if(invincible) {
					Debug.Log("même pas mal");
				} else {
					SoundManager.instance.RandomizeSfx(hurt);
					Debug.Log("Aieaieaie ouie ouie");
					invincible = true;
					invincibleDate = Time.time;
					animator.SetBool("invincible", true);
					Debug.Log("I'm invincible !!");
					LoseHp();
				}
			}
			if (other.tag == "Life") {
				other.GetComponent<Box>().lootBox();
				if(AddHp()){
					Destroy(other.gameObject);
				}
			}
			if (other.tag == "Loot") {
				if(!other.GetComponent<Box>().isBoxOpen()){
					other.GetComponent<Box>().openBox();
				}
				other.GetComponent<Box>().lootBox();
				if(AddAmmo()){
					Destroy(other.gameObject);
				}
			}
    }	
	public void Shot()
 	{

		if((Time.time - lastShotDate > delayBetweenShots)) {
			if(SpendAmmo()){
				
				SoundManager.instance.RandomizeSfx(shootSound);
				Vector3 posOffset = new Vector3(transform.position.x,transform.position.y-0.25f);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 targetPosition = ray.origin;
				Vector3 shotDirection = targetPosition - posOffset;
				Vector3 shotPosition = new Vector3(posOffset.x + shotDirection.normalized.x, posOffset.y + shotDirection.normalized.y, 0f);
				GameObject bulletInstance;
				float dist = Vector3.Distance(posOffset, shotPosition);
				shotPosition = new Vector3(posOffset.x + shotDirection.normalized.x*(0.2f/dist), posOffset.y + shotDirection.normalized.y*(0.2f/dist), 0f);
				Debug.Log("Distance : " + Vector3.Distance(posOffset, shotPosition));
				bulletInstance = Instantiate(bullet, shotPosition, Quaternion.identity) as GameObject;
				Rigidbody2D rbshot = bulletInstance.GetComponent<Rigidbody2D>();
				rbshot.velocity = new Vector2(  shotDirection.normalized.x*(1f/dist) * 15, shotDirection.normalized.y*(1f/dist) * 15);
				Debug.Log("Velocity " + rbshot.velocity);
				bulletInstance.transform.localRotation = Quaternion.identity;
				Debug.Log("Shoot !!!");
				lastShotDate = Time.time;
			}
		}


 	}
	void FixedUpdate() {
		if(GameManager.instance.doingSetup) return;

		if(invincible) {
			if(Time.time - invincibleDate > invincibleTime){
				invincible = false;
				animator.SetBool("invincible", false);
			}
		}

		Vector2 vel = new Vector2(0,0);
		if(Input.GetKey(KeyCode.Space)  || Input.GetKey(KeyCode.Mouse0)) {
			Shot();
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)){
			vel.y = 4;		
			animator.SetTrigger("down");
			if(!SoundManager.instance.efxSource.isPlaying) {
				SoundManager.instance.RandomizeSfx(walk1, walk2);
			}
			
		}
		else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			animator.SetTrigger("down");
			if(!SoundManager.instance.efxSource.isPlaying) {
				SoundManager.instance.RandomizeSfx(walk1, walk2);
			}
			vel.y = -4;

		}
		if(Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.Q)){
			animator.SetTrigger("left");
			if(!SoundManager.instance.efxSource.isPlaying) {
				SoundManager.instance.RandomizeSfx(walk1, walk2);
			}
			vel.x = -4;

		}
		else if(Input.GetKey(KeyCode.RightArrow)  || Input.GetKey(KeyCode.D)){
			animator.SetTrigger("right");
			if(!SoundManager.instance.efxSource.isPlaying) {
				SoundManager.instance.RandomizeSfx(walk1, walk2);
			}
			vel.x = 4;
		}
		if(vel.x == 0 && vel.y == 0){
			animator.SetTrigger("idle");
			//SoundManager.instance.RandomizeSfx(silence);
		}
		
		playerbody.velocity = vel;

	}



}
