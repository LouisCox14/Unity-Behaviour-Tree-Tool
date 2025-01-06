using System.Collections.Generic;
using UnityEngine;

public class RootNode : BTNode
{
    [SerializeField] BTNode child;

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        return child.Query(agent);
    }

    public override void AddChild(BTNode child)
    {
        this.child = child;
    }

    public override void RemoveChild(BTNode child)
    {
        if (child != this.child)
            return;

        this.child = null;
    }

    public override List<BTNode> GetChildren()
    {
        if (this.child != null)
            return new List<BTNode>() { child };

        return new List<BTNode>();
    }
}