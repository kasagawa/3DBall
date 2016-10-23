using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// represents a plane object and its functionality
public class Plane : MonoBehaviour {
	// the amount of level
	private static int levels = 3;

	// the level of the plane
	public int level = 0;

	// the delay for the deactivation of the current plane
	private float delay = 1.5f;

	// the objects on the plane depending on the level
	public Stack<GameObject>[] objects = createObjectStackArray();

	void OnTriggerEnter(Collider other) {
		// everytime a planes exits a trigger with a Player it will spawn another plane
		if (other.tag == "Player") {
			PlaneManager.Instance.SpawnPlane ();
			// starts a coroutine for deactivating the current plane
			StartCoroutine (DeactivatePlane(delay)); 
		}
	}

	// deactivates a plane and all the objects on it
	IEnumerator DeactivatePlane(float destroyDelay) {
		yield return new WaitForSeconds (destroyDelay);
		this.gameObject.SetActive (false);
		foreach (GameObject o in objects[level]) {
			o.SetActive(false);
			o.transform.parent = transform;
		}
	}

	// create an array of stacks of game objects
	private static Stack<GameObject>[] createObjectStackArray() {
		Stack<GameObject>[] o = new Stack<GameObject>[levels];
		for (int i = 0; i < levels; i++) {
			o [i] = new Stack<GameObject> ();
		}
		return o;
	}
}