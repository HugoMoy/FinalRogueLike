using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	private Transform target;
	public Rigidbody2D rb;
	
	//Augment Chase range of 1 every 3 levels on easy (2 every 3 on diffcult)
	public float chaseRange = 5f;
	private float speed = 1f;
	private int hp = 2;
	// Sounds
	public AudioClip walk;
	public AudioClip death;
	public AudioClip hurt;
	public AudioClip hitPlayer;
	public AudioSource audio;

	private float delay = 1f;
	void Start () {
		target = GameObject.Find("Player").transform;
		chaseRange = chaseRange + Mathf.Round((GameManager.instance.level*(1+ButtonManager.UI.difficulty/3))/3);
	}
	public void LowerHp(){
		hp--;
		Debug.Log("Robot hurt ! my hp : " + hp);
		if(hp <= 0) {
			PlaySingle(death);
			GameManager.instance.enemyKilled();
			Destroy(gameObject);
		}
		else {
			PlaySingle(hurt);
		}
	}
	public void PlaySingle(AudioClip clip){
		audio.clip = clip;
		audio.Play();
	}
	void onTriggerEnter2D(Collider2D other) {
		if(other.tag == "Bullet"){
			
			Debug.Log("Ouch !");
			//playerDetected = true;
			//Destroy(other.gameObject);
		}
		Debug.Log("Enemy colide with " + other.tag);
	}
	
	// Update is called once per frame
	void Update () {
		//if(Time.time)
	}
	void moveTowardsPlayer() {
	}
	void FixedUpdate() {
			float distanceToTarget = Vector3.Distance(transform.position, target.position);
			if(distanceToTarget< chaseRange) {
				PlaySingle(walk);
				// start chasing
				Vector3 direction = target.position - this.transform.position;
				//direction.x++;
				//direction.y++;
				transform.Translate(direction *Time.deltaTime * speed);
			}
		//if (playerDetected) {
			//moveTowardsPlayer();
			//isPlayerNear();
		//}
	}
}
