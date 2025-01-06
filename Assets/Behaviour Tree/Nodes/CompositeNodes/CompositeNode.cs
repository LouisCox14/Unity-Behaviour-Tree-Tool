using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CompositeNode : BTNode
{
    [SerializeField] protected List<BTNode> children = new List<BTNode>();

    protected abstract override BTNodeState QueryEx(BehaviourTreeAgent agent);

    public override void AddChild(BTNode child)
    {
        children.Add(child);
        child.onNodeMoved = HandleChildMoved;

        OrderChildren();
    }

    public override void RemoveChild(BTNode child)
    {
        children.Remove(child);
        child.onNodeMoved -= HandleChildMoved;
    }

    private void OnEnable()
    {
        foreach (BTNode child in children)
        {
            child.onNodeMoved = HandleChildMoved;
        }
    }

    public void HandleChildMoved(BTNode node)
    {
        OrderChildren();
    }

    void OrderChildren()
    {
        children = children.OrderBy(x => -x.gridPos.y).ToList();
    }

    public override List<BTNode> GetChildren()
    {
        return children;
    }
}