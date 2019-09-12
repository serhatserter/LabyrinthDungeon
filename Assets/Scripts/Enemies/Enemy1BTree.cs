using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BTree : MonoBehaviour {


	public Enemy1AI ai;

	//--------------------------
	public Selector root;

	public Selector seePlayer;
	public ActionNode followPlayer;
	public ActionNode atackPlayer;

	public Selector notSeePlayer;
	public Selector inPlace;
	public ActionNode lookAround;
	public ActionNode walk;

	public Sequence notInPlace;
	public ActionNode goPlace;


	void Start () {
		//-----------See Player Node----------------

		atackPlayer = new ActionNode(State_atackPlayer);
		followPlayer = new ActionNode(State_followPlayer);

		List<Node> seePlayerChildren = new List<Node>();
		seePlayerChildren.Add(atackPlayer);
		seePlayerChildren.Add(followPlayer);

		seePlayer = new Selector(seePlayerChildren);


		//--------------In Place Node--------------

		lookAround = new ActionNode(State_lookAround);
		walk = new ActionNode(State_walk);

		List<Node> inPlaceChildren = new List<Node>();
		inPlaceChildren.Add(lookAround);
		inPlaceChildren.Add(walk);

		inPlace = new Selector(inPlaceChildren);


		//--------------not In Place Node--------------

		goPlace = new ActionNode(State_goPlace);

		List<Node> notInPlaceChildren = new List<Node>();
		notInPlaceChildren.Add(goPlace);

		notInPlace = new Sequence(notInPlaceChildren);


		//--------------not See Player Node--------------

		List<Node> notSeePlayerChildren = new List<Node>();
		notSeePlayerChildren.Add(inPlace);
		notSeePlayerChildren.Add(notInPlace);

		notSeePlayer = new Selector(notSeePlayerChildren);


		//--------------Root Node--------------

		List<Node> rootChildren = new List<Node>();
		rootChildren.Add(seePlayer);
		rootChildren.Add(notSeePlayer);

		root = new Selector(rootChildren);
	}


	private NodeStates State_atackPlayer()
	{
		if (ai.isAtack) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}

	private NodeStates State_followPlayer()
	{
		if (ai.isFollow) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}

	private NodeStates State_lookAround()
	{
		if (ai.isLook) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}

	private NodeStates State_walk()
	{
		if (ai.isWalk) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}

	private NodeStates State_goPlace()
	{
		if (ai.isGo) { return NodeStates.SUCCESS; }
		else { return NodeStates.FAILURE; }
	}


	void Update()
	{

		root.Evaluate();

		if (followPlayer.nodeState == NodeStates.SUCCESS) { ai.Follow(); }

		else if (lookAround.nodeState == NodeStates.SUCCESS) { ai.ControlAround(); }

		if (atackPlayer.nodeState == NodeStates.SUCCESS) { ai.Attack(); }

		if (walk.nodeState == NodeStates.SUCCESS) { StartCoroutine( ai.goWatch()); }

		ai.EnemyView();


	}
}
