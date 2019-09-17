using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartManage : MonoBehaviour {


	public GameObject player;
	public Image dark;

	public bool damaged;

	public int heartCount;

	void Start () {
		damaged = true;
		heartCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		

		if(player.GetComponent<PlayerAttack>().damage == true && damaged)
		{
			if (transform.childCount > 0)
			{
				transform.GetChild(0).transform.DOScale(0.1f, 1).OnComplete(DisabedHeart);
				
			}

			else
			{
				player.GetComponent<PlayerController>().enabled = false;
				player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				player.GetComponent<Animator>().SetBool("DeathKey", true);

				StartCoroutine(Wait());
			}

			damaged = false;
		}


		if (player.GetComponent<PlayerController>().enabled == false)
		{
			foreach (Transform child in transform)
			{
				child.transform.DOScale(0.001f, 1).OnComplete(DisabedAllHeart);
			}
		}


	}

	void DisabedAllHeart()
	{
		Destroy(this);
	}

	void DisabedHeart()
	{
		heartCount--;
		Destroy(transform.GetChild(0).gameObject); 
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);
		dark.DOColor(Color.black, 2f).OnComplete(ReturnMenu);
	}

	void ReturnMenu()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
