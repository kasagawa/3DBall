using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plane : MonoBehaviour {

	private float delay = 1.5f;

	public bool leftAttach = false;

	// the objects on the plane
	public Stack<GameObject> objects = new Stack<GameObject> ();

	void OnTriggerEnter(Collider other) {
		// everytime a planes exits a trigger with a Player it will spawn another plane
		if (other.tag == "Player") {
			Debug.Log ("entered the plane");
			PlaneManager.Instance.SpawnPlane ();
			Debug.Log ("start coroutine");
			StartCoroutine (DestroyPlane(delay));
		}
	}

	IEnumerator DestroyPlane(float destroyDelay) {
		Debug.Log ("started delay");
		yield return new WaitForSeconds (destroyDelay);
		Debug.Log ("destroying object");
		this.gameObject.SetActive (false);
		foreach (GameObject o in objects) {
			o.SetActive(false);
			o.transform.parent = transform;
		}
	}
}
