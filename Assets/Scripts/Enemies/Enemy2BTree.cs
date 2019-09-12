using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2BTree: MonoBehaviour {

	public Enemy2AI ai2; 

	//--------------------------
	public Selector rootEn2;

	public Selector travelEvr;
	public ActionNode walkDes;
	public ActionNode lookEvr;

	public Selector isSeenPlayer;
	public ActionNode walkPlayer;
	public ActionNode runPlayer;

	void Start () {

		///------------checkEvr-------------------
		walkDes = new ActionNode(State_walkDes);
		lookEvr = new ActionNode(State_lookEvr);


		List<Node> travelEvrChildren = new List<Node>();
		travelEvrChildren.Add(walkDes);
		travelEvrChildren.Add(lookEvr);

		travelEvr = new Selector(travelEvrChildren);

		///------------checkEvr-------------------
		walkPlayer = new ActionNode(State_walkPlayer);
		runPlayer = new ActionNode(State_runPlayer);


		List<Node> isSeenPlayerChildren = new List<Node>();
		isSeenPlayerChildren.Add(walkPlayer);
		isSeenPlayerChildren.Add(runPlayer);

		isSeenPlayer = new Selector(isSeenPlayerChildren);

		//--------------Root Node--------------

		List<Node> rootEn2Children = new List<Node>();
		rootEn2Children.Add(travelEvr);
		rootEn2Children.Add(isSeenPlayer);


		rootEn2 = new Selector(rootEn2Children);
	}

	private NodeStates State_walkDes()
	{
		if (ai2.isWalk) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}
	private NodeStates State_lookEvr()
	{
		if (ai2.isLook) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}

	private NodeStates State_walkPlayer()
	{
		if (ai2.isWalkPlayer) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}
	private NodeStates State_runPlayer()
	{
		if (ai2.isRun) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}



	// Update is called once per frame
	void Update () {

		rootEn2.Evaluate();

		if (walkDes.nodeState == NodeStates.SUCCESS) { ai2.WalkDest(); }
		else if (runPlayer.nodeState == NodeStates.SUCCESS) { ai2.StartCoroutine(ai2.Run()); }
		else if (walkPlayer.nodeState == NodeStates.SUCCESS) { ai2.FollowPlayer(); }


		if (lookEvr.nodeState == NodeStates.SUCCESS) { ai2.EnemyView(); }


	}
}
