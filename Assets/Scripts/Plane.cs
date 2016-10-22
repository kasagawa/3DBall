using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plane : MonoBehaviour {

	private float delay = 1.5f;

	// the objects on the plane
	public Stack<GameObject> objects = new Stack<GameObject> ();

	void OnTriggerEnter(Collider other) {
		// everytime a planes exits a trigger with a Player it will spawn another plane
		if (other.tag == "Player") {
			PlaneManager.Instance.SpawnPlane ();
			StartCoroutine (DestroyPlane(delay));
		}
	}

	IEnumerator DestroyPlane(float destroyDelay) {
		yield return new WaitForSeconds (destroyDelay);
		this.gameObject.SetActive (false);
		foreach (GameObject o in objects) {
			o.SetActive(false);
			o.transform.parent = transform;
		}
	}
}
