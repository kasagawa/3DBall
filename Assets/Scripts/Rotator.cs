using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	//This simple class makes the Easter Eggs spin
	void Update () {
		transform.Rotate (new Vector3 (45, 45, 45) * Time.deltaTime);
	}
}

//Old rotation: 15, 30, 45