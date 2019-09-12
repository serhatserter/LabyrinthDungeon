using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeWall : MonoBehaviour
{

	public GameObject player;
	Color normalColor, transparentColor;

	bool faded;
	// Use this for initialization
	void Start()
	{
		normalColor = transform.GetComponent<Renderer>().material.color;
		transparentColor = normalColor;
		transparentColor.a = 0.5f;
		faded = false;
	}

	// Update is called once per frame
	void Update()
	{

		if ((transform.position.z + transform.localScale.y > player.transform.position.z - 2f ) 
			&& (player.transform.position.z > transform.position.z) 
			&& ((player.transform.position.x > Mathf.Abs(transform.position.x - transform.localScale.z) 
			&& player.transform.position.x < transform.position.x + transform.localScale.z ))
			&& (transform.rotation.y != 0))
		{
			
				transform.GetComponent<Renderer>().material.DOColor(transparentColor, 0.3f).OnComplete(SetFaded);
		}
		else
		{
			if(faded) transform.GetComponent<Renderer>().material.DOColor(normalColor, 0.3f).OnComplete(SetFaded);
		}
	}

	void SetFaded()
	{
		if (faded) faded = false;
		else { faded = true; }
	}
}