using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f; //Shpejtesia e levizjes se lojtarit
	private Rigidbody2D myBody; // Per fiziken
	private Animator anim; // Per animimin e levizjes se lojtarit

	// Referenc per objektet
	void Awake() // Funksioni i pare qe behet call kur fillojme lojen
	{
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Per inicializimin
	void Start () { 

	}
	
	// Azhornohet per cdo frame (60 here ne sekond)
	void Update () {
		
	}

	// Ktu vendosim cdo kalkulim fizike 
	void FixedUpdate()
	{
		PlayerWalk();
	}

	// Funksioni per te levizur lojtarin
	void PlayerWalk()
	{
		float h = Input.GetAxisRaw("Horizontal"); // Me kte do te dim kur te shtypim majtas ose djathtas (A ose D; Shigjetat majtas ose djathtas) Raw > Nr i plote. Kalon nga 0 tek 1. Left negativ - Right pozitiv
		if(h > 0) // Dmth po shkojme nga ana e djathte (nr pozitiv)
		{
			myBody.velocity = new Vector2(speed, myBody.velocity.y); // Vector 2 permban x dhe y (Vector 3 per 3D). Parametri i pare eshte per x dhe i dyti per y. Ktu nuk duam te vendosim shpejtesi per y ndaj vejme myBody.velocity.y (Vendosim vetem x qe ta levizim majtas ose djathtas)

			ChangeDirection(1);

		} else if(h < 0) // Dmth po shkojme nga ana e majte (nr negativ)
		{
			myBody.velocity = new Vector2(-speed, myBody.velocity.y); // Velocity = Speed over Time

			ChangeDirection(-1);

		} else
		{
			myBody.velocity = new Vector2(0f, myBody.velocity.y); // Qe te ndaloj direkt sapo leshojn butonin
		}

		anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x)); // Qe animimi te kaloj direkt nga Idle ne Walk dhe anasjelltas  
	}

	void ChangeDirection(int direction) // Per te ndryshuar drejtimin e trupit kur leviz majtas-djathtas
	{
		Vector3 tempScale = transform.localScale; // localScale eshte current scale dhe tempScale eshte temporary scale, variabla e krijuar per te ruajtur kte current scale. Pra marrim current scale e fusim ne temporary scale. E mbajme ate perkohesisht
		tempScale.x = direction; // Ktu i ndryshojme X-scale ksaj temporary variable. Pra ndryshojme vleren e X perkohesisht.
		transform.localScale = tempScale; // Ktu e kthejme vleren prap mbrapsht tek temp scale. E kthejme X serish ne current scale

		// Nuk mund te perdorim dot se jep error psh:
		// transform.localScale.x = direction;
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if(target.gameObject.tag == "Ground")
		{
			print("Collided with the ground");
		}
	}

} //class
