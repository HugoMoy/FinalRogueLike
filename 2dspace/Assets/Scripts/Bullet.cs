using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

private Animator animator;
	private Rigidbody2D bulletbdy; 
	// Use this for initialization
	void Awake () {
		bulletbdy = gameObject.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		Debug.Log("I'm alive !");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log("bullet velocity " + bulletbdy.velocity);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag != "Player"){
			Debug.Log("collide with " + collision.gameObject.tag);
			Destroy(gameObject);
		}
	}

	IEnumerator WaitTillEnd(){
		yield return new WaitForSeconds(2);
	}
	// void OnBecameInvisible () {
   	// 	Destroy(gameObject);
 	// }
	void OnTriggerEnter2D(Collider2D other)  {
		Debug.Log("I collided");
		if(other.tag == "Loot" || other.tag == "Life"){
			if(!other.GetComponent<Box>().isBoxOpen()){
				other.GetComponent<Box>().openBox();
				animator.SetTrigger("Explode");
				this.bulletbdy.velocity = new Vector2(0,0);
				Destroy(gameObject,1);
			}
			
		}
		else if(other.tag == "Enemy") {
			Debug.Log("Hello i'ma bullet and i just touched an enemy!");
			other.GetComponent<Enemy>().LowerHp();
			animator.SetTrigger("Explode");
			// StartCoroutine(WaitTillEnd());
			this.bulletbdy.velocity = new Vector2(0,0);
			Destroy(gameObject,1);
		}
		else {
			// animator.SetTrigger("Explode");
			// StartCoroutine(WaitTillEnd());
			animator.SetTrigger("Explode");
			this.bulletbdy.velocity = new Vector2(0,0);
			Destroy(gameObject,1);
		}
	}
}
