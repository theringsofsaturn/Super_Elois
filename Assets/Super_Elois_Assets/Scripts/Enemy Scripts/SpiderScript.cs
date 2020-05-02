using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderScript : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D myBody;

	private Vector3 moveDirection = Vector3.down;

	void Awake() {
		anim = GetComponent<Animator>();
		myBody = GetComponent<Rigidbody2D>();
	}
	
	void Start () {
		
	}
	

	void Update () {
		MoveSpider();
	}

	void MoveSpider() {
		transform.Translate(MoveDirection * Time.smoothDeltaTime);
	}
}
