using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewScene : MonoBehaviour {

	public GameObject player;
	public Image dark;
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position ,transform.position) < 5f)
		{
			dark.DOColor(Color.black, 2f).OnComplete(GoNextScene);
		}
	}

	void GoNextScene()
	{

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
	}
}
