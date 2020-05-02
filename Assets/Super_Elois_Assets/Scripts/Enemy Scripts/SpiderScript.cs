using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderScript : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D myBody;

	private Vector3 moveDirection = Vector3.down;

	private string corountine_Name = "ChangeMovement";

	void Awake() {
		anim = GetComponent<Animator>();
		myBody = GetComponent<Rigidbody2D>();
	}
	
	void Start () {
		StartCoroutine(corountine_Name);
	}
	

	void Update () {
		MoveSpider();
	}

	void MoveSpider() {
		transform.Translate(moveDirection * Time.smoothDeltaTime);
	}

	IEnumerator ChangeMovement() {
		yield return new WaitForSeconds(Random.Range(2f, 5f));

		if(moveDirection == Vector3.down) {
			moveDirection = Vector3.up;

		} else {
			moveDirection = Vector3.down;
		}

		StartCoroutine(corountine_Name);
	}
	IEnumerator SpiderDead() {
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target) {
		if(target.tag == MyTags.BULLET_TAG) {
			anim.Play("SpiderDead");

			myBody.bodyType = RigidbodyType2D.Dynamic;

			StartCoroutine(SpiderDead());
			StopCoroutine(corountine_Name);
		}
	}
}
