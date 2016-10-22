using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float storedSpeed;
	private Vector3 dir;

	public int pickUpCount;

	private Rigidbody rb;

	public GameObject plane;
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
		dir = Vector3.zero;
		manager = GameManager.Instance;
	}

	//controls the movement of the ball
	void Update() {
//		if (Input.GetKey (KeyCode.LeftArrow)) {
//			dir = Vector3.left;
//		} else if (Input.GetKey (KeyCode.RightArrow)) {
//			dir = Vector3.right;
//		} else {
//			dir = Vector3.forward;
//		}
//		if (plane.CompareTag ("TopPlane")) {
//			if (Input.GetKey (KeyCode.RightArrow)) {
//				dir = Vector3.right;
//			} else if (Input.GetKey (KeyCode.LeftArrow)) {
//				dir = Vector3.left;
//			} else {
//				dir = Vector3.forward;
//			}
//		} else if (plane.CompareTag ("LeftPlane")) {
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				dir = Vector3.left;
//			} else if (Input.GetKey (KeyCode.DownArrow)) {
//				dir = Vector3.back;
//			} else {
//				dir = Vector3.forward;
//			}
//		} else
//			return;
//
//		if (plane.CompareTag ("WinningPlane")) {
//			if (Input.GetKey (KeyCode.DownArrow)) {
//				dir = Vector3.back;
//			} else if (Input.GetKey (KeyCode.UpArrow)) {
//				dir = Vector3.forward;
//			} else if (Input.GetKey (KeyCode.RightArrow)) {
//				dir = Vector3.right;
//			} else if (Input.GetKey (KeyCode.LeftArrow)) {
//				dir = Vector3.left;
//			} 
//		} 
			
//		float amountToMove = speed * Time.deltaTime;
//		transform.Translate (dir * amountToMove);
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
	}
		
	//****we may want to move this to another class!****
	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("LeftPlane") || other.gameObject.CompareTag("TopPlane")) {
			plane = other.gameObject;
		}
		else if (other.gameObject.CompareTag("EasterEgg") && (manager.level == 0)){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}
		//you've already collected enough for this level so it doesn't add points
		else if (other.gameObject.CompareTag("EasterEgg") && (manager.level == 1)){
			other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("Jackolantern")&& (manager.level == 1)){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}
		//you've already collected enough for this level so it doesn't add points
		else if (other.gameObject.CompareTag("Jackolantern")&& (manager.level == 2)){
			other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("Skeleton")){

		}
		else if (other.gameObject.CompareTag("Ornament")){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}
		else if (other.gameObject.CompareTag("Present")){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}
		else if (other.gameObject.CompareTag("CandyCane")){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}
		else if (other.gameObject.CompareTag("TeddyBear")){
			CollectObject (other);
			GameManager.Instance.addPoints (other.gameObject.tag);
		}

		//if hit skeleton -- lose points?!
	}
//

	void OnCollisionExit(Collision c) {
		if (c.gameObject.CompareTag("Start")) {
			Destroy (c.gameObject);
		}
	}

	void CollectObject (Collider other){
		other.gameObject.SetActive(false);
	}
}
