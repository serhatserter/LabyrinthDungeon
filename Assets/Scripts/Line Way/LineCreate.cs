using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineCreate : MonoBehaviour
{

	public GameObject partEffect;
	public GameObject line, drawLine;

	public bool lineAcitve;

	public Animator anim;
	Vector3 lineVector, insVector;

	public GameObject wayCounter;

	bool isButtonDown;
	Tween fade;

	public int drawSecond, reDrawSecond;

	// Use this for initialization
	void Start()
	{
		lineAcitve = false;
		isButtonDown = false;

		drawSecond = 30;
		reDrawSecond = 15;

	}

	public bool waitCreate = false;
	IEnumerator WaitNode()
	{

		waitCreate = false;
		insVector = transform.position;
		insVector.y = 0.1f;
		insVector.x = insVector.x - 0.5f;
		insVector.z = insVector.z - 1f;
		Instantiate(drawLine, insVector, transform.rotation).transform.DOScaleZ(1, 0.5f);
		yield return new WaitForSeconds(0.2f);

		waitCreate = true;
	}

	// Update is called once per frame
	void Update()
	{

		if (lineAcitve)
		{
			lineVector = partEffect.transform.position;
			lineVector.y = 0.5f;
			line.transform.position = lineVector;

			if (waitCreate && anim.GetBool("PressKey") == true) StartCoroutine(WaitNode());
		}


		if (Input.GetKeyDown("a"))

		{
			if (isButtonDown == false)
			{
				isButtonDown = true;

				if (partEffect.activeSelf == false)
				{
					partEffect.SetActive(true);
					partEffect.transform.DOScale(1, 1);
					lineAcitve = true;

					waitCreate = true;

					fade = wayCounter.GetComponent<Renderer>().material.DOFade(0, drawSecond);
				}

			}

			else
			{
				fade.Kill();
				partEffect.transform.DOScale(0.1f, 1f).OnComplete(partEffDisable);
				isButtonDown = false;
			}

		}



		if (wayCounter.GetComponent<Renderer>().material.color.a < 0.09f)
		{
			fade.Kill();
			partEffect.transform.DOScale(0.1f, 1f).OnComplete(partEffDisable);
			isButtonDown = false;
		}


	}



	void partEffDisable()
	{
		lineAcitve = false;
		partEffect.SetActive(false);
	}
}
