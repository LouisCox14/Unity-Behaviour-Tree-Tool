using UnityEngine;

public class DebugNode : ExecutionNode
{
    [SerializeField] private string message;
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        Debug.Log(message);
        return BTNodeState.SUCCESS;
    }
}