using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	private static float destroyDelay = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerExit(Collider other) {
		// everytime a planes exits a trigger with a Player it will spawn another plane
		if (other.tag == "Player") {
			PlaneManager.Instance.SpawnPlane ();
			StartCoroutine (DestroyPlane());
		}
	}

	IEnumerator DestroyPlane() {
		yield return new WaitForSeconds (destroyDelay);
		gameObject.SetActive (false);
	}
}
