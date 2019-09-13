using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableHeart : MonoBehaviour
{

	public GameObject player, hearts, insHeart;

	Vector3 lastHeartLoc, turnAxis;
	// Use this for initialization
	void Start()
	{
		lastHeartLoc = Vector3.zero;
		turnAxis = Vector3.up;
	}

	// Update is called once per frame
	void Update()
	{

		transform.RotateAround(transform.position, turnAxis, 1.5f);

		if (Vector3.Distance(transform.position, player.transform.position) < 3f && hearts.GetComponent<HeartManage>().heartCount <= 2)
		{
			transform.DOMove(player.transform.position, 0.2f).OnComplete(DeleteHeart);
			transform.DOScale(0.1f, 1);
		}

	}

	void DeleteHeart()
	{
		GameObject x = Instantiate(insHeart);

		x.transform.localScale = Vector3.zero;
		x.transform.DOScale(0.2f, 1);

		x.transform.parent = hearts.transform;
		x.transform.position = player.transform.position + (Vector3.left * 2f) + (Vector3.up / 2);

		if (Vector3.Distance(x.transform.position, lastHeartLoc) < 1f)
		{
			x.transform.position += lastHeartLoc * 2;
		}

		lastHeartLoc = x.transform.position;

		hearts.GetComponent<HeartManage>().heartCount = hearts.GetComponent<HeartManage>().heartCount + 1;
		Destroy(gameObject);
	}

}
