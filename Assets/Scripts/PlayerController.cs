using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	private float storedSpeed;
	private Vector3 dir;

	public int pickUpCount;
	public Text countText;
	public Text onYourMarkText;

	private Rigidbody rb;
	private int count;

	void Start() {
		dir = Vector3.zero;
		rb = GetComponent<Rigidbody>();
		count = 0;
		storedSpeed = speed;
		onYourMarkText.text = "";
//		SetCountText ();
		StartCoroutine(OnYourMark());
	}

	//wait for 5 seconds to start the game & display starting text
	IEnumerator OnYourMark() {
		this.speed = 0;
		onYourMarkText.text = "On Your Mark";
		yield return new WaitForSeconds (2);
		onYourMarkText.text = "Get Set";
		yield return new WaitForSeconds (2);
		onYourMarkText.text = "Go!!";
		yield return new WaitForSeconds (1);
		onYourMarkText.text = "";
		this.speed = storedSpeed;
	}

	//controls the movement of the ball
	void Update() {
		if (Input.GetKey (KeyCode.RightArrow)) {
			dir = Vector3.right;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			dir = Vector3.left;
		} else {
			dir = Vector3.forward;
		}

		float amountToMove = speed * Time.deltaTime;
		//		transform.Rotate (dir * amountToMove);
		transform.Translate (dir * amountToMove);
	}
//		
//	//****we may want to move this to another class!****
//	void OnTriggerEnter (Collider other){
//		if (other.gameObject.CompareTag("EasterEgg")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		if (other.gameObject.CompareTag("Jackolantern")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		if (other.gameObject.CompareTag("Ornament")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		if (other.gameObject.CompareTag("Present")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		if (other.gameObject.CompareTag("CandyCane")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		if (other.gameObject.CompareTag("TeddyBear")){
//			CollectObject (other);
//			SetCountText ();
//		}
//		//if hit skeleton -- lose points?!
//	}
//
//	void SetCountText (){
//		countText.text = "Count: " + count.ToString ();
//	}
//
//	void CollectObject (Collider other){
//		Destroy (other.gameObject);
//		count++;
//	}
}
