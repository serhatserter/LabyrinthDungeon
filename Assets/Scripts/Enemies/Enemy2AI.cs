using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy2AI : MonoBehaviour {

	public Animator anim;
	public bool isWalk, isLook, isWalkPlayer, isRun;

	public NavMeshAgent agent;
	public GameObject player;
	public Image dark;

	public GameObject[] destObj;
	public Vector3 playerRunPos;

	RaycastHit hitInfo;
	Vector3 minRay;
	Vector3 maxRay;
	Vector3 tempRay;
	float viewRange, totalView;
	public float followDistance;

	float startSpeed;

	public int destNum;

	public bool collapseWall;

	Vector3 extraVec;

	void Start () {

		anim = GetComponent<Animator>();
		isWalk = true;

		destNum = 0;


		minRay = new Vector3(-3f, 0, 1f);
		maxRay = new Vector3(3f, 0, 1f);

		tempRay = minRay;
		viewRange = 0.2f;
		totalView = minRay.x;
		followDistance = 25f;

		startSpeed = agent.speed;
	}



	public void WalkDest()
	{
		anim.SetBool("isBullWalk", true);
		anim.SetBool("isBullRun", false);

		agent.SetDestination(destObj[destNum].transform.position);

		if(Vector3.Distance(transform.position, destObj[destNum].transform.position) < 1f)
		{
			if (destNum < destObj.Length - 1) destNum = destNum+1;
			else destNum = 0;
		}
		//anim.SetBool("isBullWalk", false);
	}

	public void EnemyView()
	{
		if (totalView < maxRay.x)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(tempRay), out hitInfo))
			{
				if ((hitInfo.transform.name == "Player" && hitInfo.distance < followDistance))
				{

					isWalk = false;
					isWalkPlayer = true;
					transform.DOLookAt(player.transform.position, 0.2f);
					//Debug.Log("x");

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

	public void FollowPlayer()
	{
		anim.SetBool("isBullWalk", true);
		anim.SetBool("isBullRun", false);
		if (Vector3.Distance(player.transform.position, transform.position)< followDistance) {

			isWalk = false;

			Vector3 playerPos = player.transform.position;
			playerPos.x += 3f;

			agent.SetDestination(playerPos);
			transform.DOLookAt(playerPos, 0.2f);

			if (Vector3.Distance(player.transform.position, transform.position) < followDistance/2 ){
				
				isRun = true;
			}
		}

		else
		{
			isWalkPlayer = false;
			isLook = true;
			isWalk = true;

		}

		//anim.SetBool("isBullWalk", false);
	}

	bool runPos = true;
	public IEnumerator Run()
	{

		anim.SetBool("isBullWalk", false);
		anim.SetBool("isBullRun", true);
		collapseWall = true;

		isLook = false;
		if (runPos)
		{
			playerRunPos = player.transform.position + new Vector3(1,1,1);
			runPos = false;
		}

		if (Vector3.Distance(player.transform.position, transform.position) < followDistance* 0.75f)
		{
			agent.SetDestination(playerRunPos);
			isLook = false;
			isWalkPlayer = false;
			if(agent.speed < startSpeed + 5 ) agent.speed = agent.speed + 0.3f;



		}

		else if (Vector3.Distance(player.transform.position, transform.position) > followDistance) {
			isLook = true;
			isWalk = true;
			collapseWall = false;
			anim.SetBool("isBullRun", false);
			playerRunPos = Vector3.zero;
			runPos = true;


		}

		if(Vector3.Distance(playerRunPos, transform.position) < 2f)
		{

			playerRunPos = transform.position;
			agent.speed = startSpeed;


			collapseWall = false;
			anim.SetBool("isBullIdle", true);
			anim.SetBool("isBullRun", false);
			yield return new WaitForSeconds(0.5f);
			anim.SetBool("isBullIdle", false);
			collapseWall = true;
			runPos = true;

		}


		if((Vector3.Distance(player.transform.position, transform.position)< 6f)){

			anim.SetBool("isBullRun", false);
			anim.SetBool("isBullAttack", true);
			isWalk = false;
			isWalkPlayer = false;

			player.GetComponent<PlayerController>().enabled = false;
			//anim.SetBool("isBullIdle", true);
			player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

			agent.SetDestination(transform.position);
			//anim.SetBool("isBullAttack", false);
			player.GetComponent<Animator>().SetBool("DeathKey", true);

			StartCoroutine(Wait());
		}
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
	void Update () {

		isLook = true;


		
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
