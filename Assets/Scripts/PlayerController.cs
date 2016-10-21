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
		if (plane.CompareTag ("TopPlane")) {
			if (Input.GetKey (KeyCode.RightArrow)) {
				dir = Vector3.right;
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				dir = Vector3.left;
			} else {
				dir = Vector3.forward;
			}
		} else if (plane.CompareTag ("LeftPlane")) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				dir = Vector3.left;
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				dir = Vector3.back;
			} else {
				dir = Vector3.forward;
			}
		} else
			return;
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
			
		float amountToMove = speed * Time.deltaTime;
		transform.Translate (dir * amountToMove);
	}
		
	//controls the movement of the ball
	void FixedUpdate () {
		if (plane.CompareTag ("WinningPlane")) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 dir = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (dir * speed);
		}
	}
		
	//****we may want to move this to another class!****
	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("LeftPlane") || other.gameObject.CompareTag("TopPlane")) {
			plane = other.gameObject;
		}
		else if (other.gameObject.CompareTag("EasterEgg") && (manager.level == 0)){
			CollectObject (other);
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
		else if (other.gameObject.CompareTag("Ornament")&& (manager.level == 2)){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("Present")&& (manager.level == 2)){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("CandyCane")&& (manager.level == 2)){
			CollectObject (other);
		}
		else if (other.gameObject.CompareTag("TeddyBear")&& (manager.level == 2)){
			CollectObject (other);
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
		GameManager.Instance.addPoints ();
	}
}
