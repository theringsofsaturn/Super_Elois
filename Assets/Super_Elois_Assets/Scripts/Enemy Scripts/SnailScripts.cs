using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScripts : MonoBehaviour {

	public float moveSpeed = 1f;
	private Rigidbody2D myBody;
	private Animator anim;

	public LayerMask playerLayer;

	private bool moveLeft;

	private bool canMove;
	private bool stunned;

	public Transform left_Collision, right_Collision, top_Collision, down_Collision;
	private Vector3 left_Collision_Pos, right_Collision_Pos;

	void Awake() {
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		left_Collision_Pos = left_Collision.position;
		right_Collision_Pos = right_Collision.position;
	}

	
	void Start () {
		moveLeft = true;
		canMove = true;
	}
	
	
	void Update () {
		if (canMove) {
			if (moveLeft)
			{
				myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
			}
			else
			{
				myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
			}
		}

		CheckCollision();


	}

	void CheckCollision() {

		RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
		RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

		Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

		if(topHit != null) { // Nqs te gjitha kto jane true (nqs objekti qe rrokim ne top ka tagun e player dhe nqs nuk jemi stunned do bejme...
			if(topHit.gameObject.tag == MyTags.PLAYER_TAG) {
				if (!stunned) {
					topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
						new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 13f);

					canMove = false;
					myBody.velocity = new Vector2(0, 0);

					anim.Play("Stunned");
					stunned = true;

					// BEETLE CODE HERE
					if(tag == MyTags.BEETLE_TAG) {
						anim.Play("Stunned");
						StartCoroutine(Dead(0.5f));
					}
				}
			}
		}

		if(leftHit) {
			if(leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
				if (!stunned) {
					// Lendojme Playerin (Damage player)
					print("DAMAGE LEFT");
				} else {
					if(tag != MyTags.BEETLE_TAG) {
						myBody.velocity = new Vector2(30f, myBody.velocity.y); // Mbasi te jete stunned, nqs e shtyjme objektin nga majtas (nqs player eshte majtas) do ta cojme objektin ne drejtim djathtas, ta leshojme djathtas dmth.
						StartCoroutine(Dead(3f));
					}
				}
			}
		}

		if (rightHit) {
			if(rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
				if (!stunned) {
					// Lendojme Playerin (Damage player)
					print("DAMAGE RIGHT");
				}
				else
				{
					if (tag != MyTags.BEETLE_TAG) {
						myBody.velocity = new Vector2(-30f, myBody.velocity.y); // Ktu eshte numer negativ. Dmth, Mbasi te jete stunned, nqs e shtyjme objektin nga djathtas (nqs player eshte djathtas) do ta cojme objektin ne drejtim majtas, ta leshojme majtas dmth.
						StartCoroutine(Dead(3f));
					}	
				}
			}
		}


		// Nqs nuk detektojme kolizion me, atehere bej ate qe eshte ne {} Pra nqs nuk detektojme qe mund te levizim...
		if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f)) {

			changeDirection(); // Nqs nul leviz dot me, ose nqs detektojme qe nuk mund te bejme me Raycast, kthejme drejtim. 



		}
	}

	void changeDirection() {

		moveLeft = !moveLeft;

		Vector3 tempScale = transform.localScale;

		if(moveLeft) {
			tempScale.x = Mathf.Abs(tempScale.x);

			left_Collision.position = left_Collision_Pos;
			right_Collision.position = right_Collision_Pos;

		} else {
			tempScale.x = -Mathf.Abs(tempScale.x);

			left_Collision.position = right_Collision_Pos;
			right_Collision.position = right_Collision_Pos;
		}

		transform.localScale = tempScale;
	}

	void OnCollisionEnter2D(Collision2D target) {
		if(target.gameObject.tag == "Player") {
			anim.Play("Stunned");
		}
	}

	IEnumerator Dead(float timer) {
		yield return new WaitForSeconds(timer);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target) {  // Nqs tagu eshte Beetle, bullet do e vrasi 
		if(target.tag == MyTags.BULLET_TAG) {
		
			if(tag == MyTags.BEETLE_TAG) {
				anim.Play("Stunned");

				canMove = false;
				myBody.velocity = new Vector2(0, 0);

				StartCoroutine(Dead(0.4f));
			}

			if(tag == MyTags.SNAIL_TAG) { // Nqs tagu eshte Snail, bullet do vrasi Snail
				if (!stunned) {

					anim.Play("Stunned");
					stunned = true;
					canMove = false;
					myBody.velocity = new Vector2(0, 0);

				} else {
					gameObject.SetActive(false);
				}
			}
		}
	
	}
}
