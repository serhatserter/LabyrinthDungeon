using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using DG.Tweening;

public class Swich : MonoBehaviour
{

	public Material switchOn, switchOff;
	public GameObject door, player;

	public GameObject[] objects;


	// Use this for initialization
	void Start()
	{

		objects = GameObject.FindGameObjectsWithTag("Monster");

	}

	// Update is called once per frame
	void Update()
	{

		if (Vector3.Distance(transform.position, player.transform.position) >= 3f)
		{
			GetComponent<Renderer>().material = switchOff;

			if (door.transform.position.y >= 5f) door.transform.DOMoveY(0, 0.5f);
			//door.SetActive(true);

			MonsterDetect();


		}
		else
		{
			GetComponent<Renderer>().material = switchOn;
			door.transform.DOMoveY(5, 0.5f);
			//door.SetActive(false);
		}

	}


	void MonsterDetect()
	{
		GetComponent<Renderer>().material = switchOff;

		if(door.transform.position.y>= 5f) door.transform.DOMoveY(0, 0.5f);

		foreach (GameObject obj in objects)
		{

			if (Vector3.Distance(transform.position, obj.transform.position) <= 1f)
			{
				GetComponent<Renderer>().material = switchOn;
				door.transform.DOMoveY(5, 0.5f);
				//door.SetActive(false);

				obj.transform.parent.gameObject.GetComponent<Enemy1AI>().enabled = false;
				obj.transform.parent.gameObject.GetComponent<Enemy1BTree>().enabled = false;
				obj.transform.parent.GetComponent<NavMeshAgent>().enabled = false;
				this.enabled = false;
			}
		}
	}

}
