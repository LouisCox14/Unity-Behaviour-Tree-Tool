using System;
using System.Collections.Generic;
using UnityEngine;

public enum BTNodeState
{
    SUCCESS,
    FAILURE,
    RUNNING
}
public abstract class BTNode : ScriptableObject
{
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 gridPos;

    [HideInInspector] public bool isActive;

    public Action<BTNode> onNodeMoved;

    public BTNodeState Query(BehaviourTreeAgent agent)
    {
        if (!isActive)
        {
            isActive = true;
            OnActivate(agent);
        }

        BTNodeState state = QueryEx(agent);

        if (state != BTNodeState.RUNNING)
        {
            DeactivateAllChildren(agent);
        }

        return state;
    }

    public void DeactivateAllChildren(BehaviourTreeAgent agent)
    {
        if (!isActive)
            return;

        foreach (BTNode child in GetChildren())
        {
            child.DeactivateAllChildren(agent);
        }

        isActive = false;
        OnDeactivate(agent);
    }

    protected abstract BTNodeState QueryEx(BehaviourTreeAgent agent);
    protected virtual void OnActivate(BehaviourTreeAgent agent) { }
    protected virtual void OnDeactivate(BehaviourTreeAgent agent) { }

    public virtual void AddChild(BTNode child) { }
    public virtual void RemoveChild(BTNode child) { }
    public virtual List<BTNode> GetChildren() { return new List<BTNode>(); }
}