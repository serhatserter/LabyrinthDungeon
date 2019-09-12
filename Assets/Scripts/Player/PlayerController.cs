using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	public Animator anim;

	Quaternion qForward, qBack, qLeft, qRight;

	public float moveSpeed = 10f;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		qForward = Quaternion.Euler(0, 0, 0);
		qBack =  Quaternion.Euler(0, 180, 0);
		qLeft =  Quaternion.Euler(0, -90, 0);
		qRight = Quaternion.Euler(0, 90, 0);


	}
	
	// Update is called once per frame
	void Update () {
		if (!anim.GetBool("AttackKey"))
		{
		if (Input.GetKey("up"))
		{
			if (Input.GetKey("left"))
			{
				anim.SetBool("PressKey", true);
				transform.position += (Vector3.forward + Vector3.left) * Time.deltaTime * (2 * moveSpeed) / 3;
				transform.DORotateQuaternion(Quaternion.Euler(0, -45, 0), 0.2f);
			}
			else if (Input.GetKey("right"))
			{
				anim.SetBool("PressKey", true);
				transform.position += (Vector3.forward + Vector3.right) * Time.deltaTime * (2 * moveSpeed) / 3;
				transform.DORotateQuaternion(Quaternion.Euler(0, 45, 0), 0.2f);

				}
			else {

				anim.SetBool("PressKey", true);
				transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
				transform.DORotateQuaternion(qForward, 0.2f);
			}
		}

		else if (Input.GetKey("down"))
		{
			if (Input.GetKey("left"))
			{
				anim.SetBool("PressKey", true);
				transform.position += (Vector3.back + Vector3.left) * Time.deltaTime * (2 * moveSpeed) / 3;
				transform.DORotateQuaternion(Quaternion.Euler(0, -135, 0), 0.2f);

				}
			else if (Input.GetKey("right"))
			{
			anim.SetBool("PressKey", true);
				transform.position += (Vector3.back + Vector3.right) * Time.deltaTime * (2 * moveSpeed) / 3;
				transform.DORotateQuaternion(Quaternion.Euler(0, 135, 0), 0.2f);

			}

			else { 
				anim.SetBool("PressKey", true);
				transform.position += Vector3.back * Time.deltaTime * moveSpeed;
				transform.DORotateQuaternion(qBack, 0.2f);

			}
		}

		else if (Input.GetKey("left"))
		{
				anim.SetBool("PressKey", true);
				transform.position += Vector3.left * Time.deltaTime * moveSpeed;
				//transform.rotation = qLeft;
				transform.DORotateQuaternion(qLeft, 0.2f);


		}

		else if (Input.GetKey("right"))
		{
				anim.SetBool("PressKey", true);
				transform.position += Vector3.right * Time.deltaTime * moveSpeed;
				//transform.rotation = qRight;
				transform.DORotateQuaternion(qRight, 0.2f);

			}

		else
		{
			anim.SetBool("PressKey", false);

		}

		}
	}
}
