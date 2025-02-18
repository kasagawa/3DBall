﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		player = PlayerController.Instance.gameObject;
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
		transform.LookAt(player.transform);

	}
}
