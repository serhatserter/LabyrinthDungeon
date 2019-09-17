using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Node {

    public delegate NodeStates NodeReturn();

    protected NodeStates m_nodeState;

    public NodeStates nodeState {
        get { return m_nodeState; }
    }

    public Node() {}

    public abstract NodeStates Evaluate();

}



