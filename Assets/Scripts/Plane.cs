using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plane : MonoBehaviour {

	private float delay = 1.5f;

	public bool leftAttach = false;

	// the objects on the plane
	public Stack<GameObject> objects = new Stack<GameObject> ();

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.level == 1) {
			delay /= 0.25f;
		}
	}
		
	void OnTriggerEnter(Collider other) {
		// everytime a planes exits a trigger with a Player it will spawn another plane
		if (other.tag == "Player") {
			PlaneManager.Instance.SpawnPlane ();
			StartCoroutine (DestroyPlane(delay));
		}
	}

	IEnumerator DestroyPlane(float destroyDelay) {
		yield return new WaitForSeconds (destroyDelay);
		if (PlayerController.Instance.plane.Equals (this)) {
			Destroy (this);
		}
		gameObject.SetActive (false);
		foreach (GameObject o in objects) {
			o.SetActive(false);
			o.transform.parent = transform;
		}
	}
}
