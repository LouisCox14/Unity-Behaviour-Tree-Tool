using UnityEngine;

public class BoolSetNode : ExecutionNode
{
    [SerializeField] private string blackboardLookupKey;
    [SerializeField] private bool targetValue = true;

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        agent.GetBlackboard().SetValue(blackboardLookupKey, targetValue);
        return BTNodeState.SUCCESS;
    }
}
