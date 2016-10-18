using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public int pickUpCount;

	public Text countText;
	private Rigidbody rb;
	private int count;

	void Start() {
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	//****we may want to move this to another class!****
	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag("EasterEgg")){
			Destroy (other.gameObject);
			count++;
			SetCountText ();
		}
		if (other.gameObject.CompareTag("Jackolantern")){
			Destroy (other.gameObject);
			count++;
			SetCountText ();
		}
		//if hit skeleton -- lose points?!
	}

	void SetCountText (){
		countText.text = "Count: " + count.ToString ();
	}
}
