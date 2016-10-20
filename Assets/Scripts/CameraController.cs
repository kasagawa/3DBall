using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	private GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		player = PlayerController.Instance.gameObject;
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		float angle = player.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, angle, 0);


		transform.position = player.transform.position + (rotation * offset);

		transform.LookAt(player.transform);

	}
}
