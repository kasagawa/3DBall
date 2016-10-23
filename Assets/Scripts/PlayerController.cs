using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Controls the player's (ball) movement and pick up of the player
public class PlayerController : MonoBehaviour {

	// the first, second, and third speed depending on the level
	public float speed, speed1, speed2;

	// the current speed of the player
	public float storedSpeed;

	// the count of the amount of objects the player 
	// has picked up in the current level
	public int pickUpCount;

	// if the plane is on a platform
	private bool grounded;

	// the rigidbody component of the player
	private Rigidbody rb;

	// the current plane the player is on
	public GameObject plane;

	// the force of the player jump
	private static float jumpForce = 350f;

	// an instance of the game manager
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

	// the start of the game
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
		} else if (plane.CompareTag ("WinningPlane")) { // different rolling functionality for the winning scene
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 mov = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (mov * speed);
			return;
		} 
		// jumping functionality (only jump if on ground and in level 3)
		if (Input.GetKey(KeyCode.Space) && grounded && manager.level == 2) { 
			PlaneManager.Instance.SpawnPlane (); // spawns a new plane when jumping in order to normalize
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

	void OnCollisionEnter (Collision collision) {
		// determines if the player collided with the skeleton
		if (collision.gameObject.CompareTag("Skeleton") && (manager.level == 1)){
			manager.subPoints ();
		}
	}

	// speeds up the player (changes the player's speed)
	public void speedUp() {
		if (manager.level == 1) {
			speed = speed1;
		} else if (manager.level == 2) {
			speed1 = speed2;
		}
	}

	void OnCollisionExit(Collision c) {
		// determine of the player is at the start plane
		if (c.gameObject.CompareTag("Start")) {
			Destroy (c.gameObject);
		}
	}

	// collects an object (deactivates an object)
	//for the player and adds pointr to the game manager
	void CollectObject (Collider other){
		other.gameObject.SetActive(false); 
		manager.addPoints (other.gameObject.tag);
	}
}
