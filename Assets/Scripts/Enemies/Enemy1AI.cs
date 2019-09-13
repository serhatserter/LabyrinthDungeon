using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.AI;

public class Enemy1AI : MonoBehaviour
{

	public Animator anim;
	public NavMeshAgent agent;
	public GameObject player;
	public GameObject heart;

	RaycastHit hitInfo;
	Vector3 minRay;
	Vector3 maxRay;
	Vector3 tempRay;
	float viewRange, totalView;
	public float followDistance, nearDistance, hearDistance;
	public float turningDegree;
	Vector3 rotateEnd, rotateStart;

	public bool isAtack, isFollow, isLook, isWalk, isGo;

	Vector3 watchPlace;

	void Start()
	{

		anim = GetComponent<Animator>();

		minRay = new Vector3(-1f, 0, 1f);
		maxRay = new Vector3(1f, 0, 1f);

		tempRay = minRay;
		viewRange = 0.2f;
		totalView = minRay.x;

		followDistance = 10f;
		hearDistance = 4f;
		nearDistance = 3.5f;

		watchPlace = transform.position;
		rotateStart.y = transform.rotation.eulerAngles.y;
		rotateEnd.y = transform.rotation.eulerAngles.y + turningDegree;

		isFollow = false;
		isLook = true;

	}

	public void EnemyView()
	{
		if (totalView < maxRay.x)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(tempRay), out hitInfo))
			{
				if ((hitInfo.transform.name == "Player" && hitInfo.distance < followDistance))
				{

					isLook = false;
					isFollow = true;
					anim.SetBool("Enemy1Walk", true);
					transform.DOLookAt(player.transform.position, 0.2f);

				}
			}
		}

		else
		{
			totalView = minRay.x;
			tempRay = minRay;

		}

		totalView = totalView + viewRange;
		tempRay.x = totalView;
	}


	public void Follow()
	{

		if (Vector3.Distance(player.transform.position, transform.position) >= nearDistance
			&& Vector3.Distance(player.transform.position, transform.position) < followDistance)
		{

			isAtack = false;
			isWalk = false;

			Vector3 playerPos = player.transform.position;
			playerPos.x += 3f;

			agent.SetDestination(playerPos);
			transform.DOLookAt(playerPos, 0.2f);

			rotateEnd.y = transform.rotation.eulerAngles.y;
			rotateStart.y = transform.rotation.eulerAngles.y - turningDegree;

			anim.SetBool("Enemy1Attack", false);
			player.GetComponent<PlayerAttack>().damage = false;
		}

		else if (Vector3.Distance(player.transform.position, transform.position) < nearDistance)
		{

			isAtack = true;
		}

		else
		{
			rotateEnd.y = transform.rotation.eulerAngles.y;
			rotateStart.y = transform.rotation.eulerAngles.y - turningDegree;

			isFollow = false;
			isWalk = true;

			isAtack = false;
			anim.SetBool("Enemy1Walk", false);
			anim.SetBool("Enemy1Attack", false);
			player.GetComponent<PlayerAttack>().damage = false;
			agent.SetDestination(transform.position);

		}
	}

	public void Attack()
	{
		if (player.GetComponent<PlayerAttack>().attack == true)
		{
			player.GetComponent<PlayerAttack>().damage = false;
			isAtack = false;
			transform.DOPunchPosition(-3 * transform.forward, 1f, 1);
		}
		else
		{
			
			StartCoroutine(WaitAndAttack());
			
		}
	}

	IEnumerator WaitAndAttack()
	{
		anim.SetBool("Enemy1Attack", true);
		isAtack = true;
		player.GetComponent<PlayerAttack>().damage = true;


		yield return new WaitForSeconds(0.5f);

		heart.GetComponent<HeartManage>().damaged = true;
		isAtack = false;
		player.GetComponent<PlayerAttack>().damage = false;
		anim.SetBool("Enemy1Attack", false);
	}

	bool ControlAroundBool = true;

	public void ControlAround()
	{
		if (Vector3.Distance(player.transform.position, transform.position) < hearDistance)
		{
			isFollow = true;
			isLook = false;
		}

		if (ControlAroundBool)
		{
			ControlAroundBool = false;
			transform.DOLocalRotate(rotateEnd, 1f).OnComplete(ControlAroundState);
		}
	}

	void ControlAroundState()
	{
		if (rotateEnd.y < 0) { rotateEnd.y = Mathf.Abs(rotateEnd.y); }
		else if (rotateEnd.y > 360) { rotateEnd.y = rotateEnd.y % 360; }

		if (rotateEnd.y <= rotateStart.y) rotateEnd.y = Mathf.Abs(transform.rotation.eulerAngles.y) + turningDegree;
		else rotateEnd.y = Mathf.Abs(transform.rotation.eulerAngles.y - turningDegree);

		ControlAroundBool = true;
	}


	public IEnumerator goWatch()
	{
		if (transform.position.x != watchPlace.x && transform.position.z != watchPlace.z && !isFollow)
		{
			isLook = false;
			yield return new WaitForSeconds(2);

			if (isWalk)
			{
				watchPlace.y = transform.position.y + 0.0001f;
				transform.DOLookAt(watchPlace, 0.2f);
				agent.SetDestination(watchPlace);
				isWalk = true;

			}
		}
		else
		{
			isLook = true;
			isWalk = false;
		}

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Vector3 direction = transform.TransformDirection(minRay) * followDistance;
		Vector3 direction2 = transform.TransformDirection(maxRay) * followDistance;
		Gizmos.DrawRay(transform.position, direction);
		Gizmos.DrawRay(transform.position, direction2);
	}
}
