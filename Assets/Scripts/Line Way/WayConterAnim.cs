using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WayConterAnim : MonoBehaviour {


	void Start () {
		
	}

	void Update () {
		transform.RotateAround(transform.position, Vector3.up, 0.5f);
	}
}
