using UnityEngine;
using System.Collections;

public class SnowManSpinScript : MonoBehaviour {
	
	//This makes the snowman look like it's spinning
	void Update () {
		transform.Rotate (new Vector3 (0, 180, 0) * Time.deltaTime);
	}
}
