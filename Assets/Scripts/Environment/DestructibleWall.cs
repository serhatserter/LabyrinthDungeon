using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

public class DestructibleWall : MonoBehaviour {


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Bull" && col.gameObject.GetComponent<Enemy2AI>().collapseWall)
		{

		
			foreach (Transform child in transform.parent)
			{
				child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				
				child.GetComponent<Rigidbody>().AddForce((Vector3.up)* 100f);



				col.gameObject.GetComponent<Enemy2AI>().isWalk = false;
				col.gameObject.GetComponent<Enemy2AI>().isRun = false;
				col.gameObject.GetComponent<Enemy2AI>().isWalkPlayer = false;
				col.gameObject.GetComponent<Enemy2AI>().isLook = false;

				col.gameObject.GetComponent<Animator>().SetBool("isBullRun", false);
				col.gameObject.GetComponent<Animator>().SetBool("isBullDeath", true);

				col.gameObject.GetComponent<Enemy2BTree>().enabled = false;
				col.gameObject.GetComponent<Enemy2AI>().enabled = false;

				col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

				StartCoroutine(WaitWall());
			}
				
			

		}
	}

	IEnumerator WaitWall()
	{
		yield return new WaitForSeconds(1);

		foreach (Transform child in transform.parent)
		{
			child.GetComponent<BoxCollider>().isTrigger = true;
		}

		yield return new WaitForSeconds(1);

		foreach (Transform child in transform.parent)
		{
			Destroy(child.gameObject);
		}

	}
}
