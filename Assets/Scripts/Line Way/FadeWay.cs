using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeWay : MonoBehaviour {

	Color normalColor, transparentColor;

	// Use this for initialization
	void Start () {
		normalColor = transform.GetComponent<Renderer>().material.color;
		transparentColor = normalColor;
		transparentColor.a = 0f;

		transform.GetComponent<Renderer>().material.DOColor(transparentColor, 120f).OnComplete(DeleteWay);
	}
	
	void DeleteWay()
	{
		Destroy(transform.parent.gameObject);
	}
}
