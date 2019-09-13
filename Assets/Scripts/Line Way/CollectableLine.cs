using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableLine : MonoBehaviour {

	public GameObject player, wayCounter;
	Tween addColor;
	bool isCollect;

	// Use this for initialization
	void Start () {
		isCollect = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.up, 1.5f);

		if (Vector3.Distance(transform.position, player.transform.position)<3f)
		{
			if (wayCounter.GetComponent<Renderer>().material.color.a < 1 && isCollect==false 
				&& player.GetComponent<LineCreate>().lineAcitve == false )
			{
				isCollect = true;
				addColor = transform.DOMove(wayCounter.transform.position, 0.2f).OnComplete(WayCounterUp);
				transform.DOScale(0.1f, 0.2f);

			}
		}
	}

	void WayCounterUp()
	{
		wayCounter.GetComponent<Renderer>().material.DOFade(wayCounter.GetComponent<Renderer>().material.color.a + 0.3f, 0.5f);
		addColor.Kill();
		Destroy(gameObject);

	}
}
