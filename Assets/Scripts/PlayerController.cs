using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed, speed1, speed2;
	public float storedSpeed;

	public int pickUpCount;

	private bool grounded;

	private Rigidbody rb;

	public GameObject plane;
	private static float jumpForce = 350f;

	private GameManager manager;

	// materials for each level
	public Material[] materials;

	// the instance of the current PlaneManager
	private static PlayerController instance;

	// static property of a PlaneManager 
	public static PlayerController Instance {
		get { // an instance getter
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PlayerController> ();
			}
			return instance;
		}
	}

	void Start() {
		rb = GetComponent<Rigidbody>();
		storedSpeed = speed;
		manager = GameManager.Instance;
	}

	//controls the movement of the ball
	void FixedUpdate () {
		if (plane.CompareTag ("TopPlane")) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				rb.velocity = new Vector3(1 * speed, rb.velocity.y);
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				rb.velocity  = new Vector3(-1 * speed, rb.velocity.y);
			} else {
				rb.velocity  = new Vector3(0, rb.velocity.y, 1 * speed);
			}
		} else if (plane.CompareTag ("LeftPlane")) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				rb.velocity = new Vector3(-1 * speed, rb.velocity.y);
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				rb.velocity = new Vector3(0, rb.velocity.y, -1 * speed);
			} else {
				rb.velocity = new Vector3(0, rb.velocity.y, 1 * speed);
			}
		} else if (plane.CompareTag ("WinningPlane")) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 mov = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (mov * speed);
			return;
		} 
		if (Input.GetKey(KeyCode.Space) && grounded && manager.level == 2) {
			rb.AddForce (new Vector3(0, jumpForce));
			grounded = false;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("LeftPlane") || other.gameObject.CompareTag ("TopPlane")) {
			plane = other.gameObject;
			grounded = true;
		}
		else if (other.gameObject.CompareTag("EasterEgg") && (manager.level == 0)){
			CollectObject (other);
			manager.addPoints (other.gameObject.tag);
		}
		//you've already collected enough for this level so it doesn't add points
		else if (other.gameObject.CompareTag("EasterEgg") && (manager.level == 1)){
			other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("Jackolantern")&& (manager.level == 1)){
			CollectObject (other);
		}
		//you've already collected enough for this level so it doesn't add points
		else if (other.gameObject.CompareTag("Jackolantern")&& (manager.level == 2)){
			other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("Skeleton") && (manager.level == 1)){
			manager.subPoints ();
		}
		else if (other.gameObject.CompareTag("Ornament")){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("Present")){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("CandyCane")){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("TeddyBear")){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("Star")){
			CollectObject (other);
		}

	}

	public void speedUp() {
		if (manager.level == 1) {
			speed = speed1;
		} else if (manager.level == 2) {
			speed1 = speed2;
		}
	}

	void OnCollisionExit(Collision c) {
		if (c.gameObject.CompareTag("Start")) {
			Destroy (c.gameObject);
		}
	}

	void CollectObject (Collider other){
		other.gameObject.SetActive(false); 
		manager.addPoints (other.gameObject.tag);
	}
}
