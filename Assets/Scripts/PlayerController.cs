using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float storedSpeed;
	private Vector3 dir;

	public int pickUpCount;

	private Rigidbody rb;
	private int count;

	public GameObject plane;

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
		count = 0;
		storedSpeed = speed;
		dir = Vector3.forward;
	}

	//controls the movement of the ball
	void Update() {
//		if (plane.CompareTag ("TopPlane")) {
//			if (Input.GetKey (KeyCode.RightArrow)) {
//				dir = Vector3.right;
//			} else if (Input.GetKey (KeyCode.LeftArrow)) {
//				dir = Vector3.left;
//			} else {
//				dir = Vector3.forward;
//			}
//		} else if (plane.CompareTag ("LeftPlane")) {
//			if (Input.GetKey (KeyCode.DownArrow)) {
//				dir = Vector3.back;
//			} else if (Input.GetKey (KeyCode.UpArrow)) {
//				dir = Vector3.forward;
//			} else {
//				dir = Vector3.left;
//			}
//		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			dir = Vector3.back;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			dir = Vector3.forward;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			dir = Vector3.right;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			dir = Vector3.left;
		} else
			return;

		float amountToMove = speed * Time.deltaTime;
		transform.Translate (dir * amountToMove);
	}
		
	//****we may want to move this to another class!****
	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("LeftPlane") || other.gameObject.CompareTag("TopPlane")) {
			plane = other.gameObject;
		}
		else if (other.gameObject.CompareTag("EasterEgg")){
			CollectObject (other);
			GameManager.Instance.addPoints ();
		}
		else if (other.gameObject.CompareTag("Jackolantern")){
			CollectObject (other);
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
		//if hit skeleton -- lose points?!
	}

	void OnCollisionExit(Collision c) {
		if (c.gameObject.CompareTag("Start")) {
			Destroy (c.gameObject);
		}
	}

	void CollectObject (Collider other){
		other.gameObject.SetActive(false);
		count++;
	}
}
