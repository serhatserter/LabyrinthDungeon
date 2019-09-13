using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnim : MonoBehaviour
{

	public GameObject player;
	public GameObject cam;

	void Start()
	{
		player = GameObject.Find("Player");
		cam = GameObject.Find("Main Camera");
	}

	void Update()
	{

		transform.RotateAround(player.transform.position, Vector3.up, 1f);
		transform.LookAt(cam.transform.position - (Vector3.back * 30));
	}
}