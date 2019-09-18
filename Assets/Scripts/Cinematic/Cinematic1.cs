using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Cinematic1 : MonoBehaviour {


	public GameObject player;
	public Image dark;
	Color transparent;
	public Image cine1, cine2;

	void Start () {
		player.GetComponent<Animator>().SetBool("Cinematic", true);
		transparent = new Color(0, 0, 0, 0);
		transform.DOMoveZ(player.transform.position.z-1.5f, 20f).SetEase(Ease.InOutSine).OnComplete(StopAnim);
		dark.DOColor(transparent, 10f);
	}
	
	void StopAnim()
	{

		StartCoroutine(WaitSecond());

	}

	IEnumerator WaitSecond()
	{
		yield return new WaitForSeconds(2);
		gameObject.GetComponent<Camera>().enabled = false;
		player.GetComponent<Animator>().SetBool("Cinematic", false);
		cine1.DOColor(transparent, 2f);
		cine2.DOColor(transparent, 2f);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController>().enabled = true;
		player.GetComponent<PlayerAttack>().enabled = true;
		player.GetComponent<LineCreate>().enabled = true;
		gameObject.SetActive(false);
	}

}
