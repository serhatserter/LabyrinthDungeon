﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject player;
	Vector3 camCoord;
	// Use this for initialization
	void Start () {
		//camCoord = transform.position;
		camCoord.y = 15;
		camCoord.z = -15;

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + camCoord;
	}
}
