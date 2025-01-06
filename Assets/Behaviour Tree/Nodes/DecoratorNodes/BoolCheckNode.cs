using UnityEngine;

public class BoolCheckNode : DecoratorNode
{
    [SerializeField] private string blackboardLookupKey;
    [SerializeField] private bool targetValue = true;

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        try
        {
            if ((bool)agent.GetBlackboard().GetValue(blackboardLookupKey) != targetValue)
                return BTNodeState.FAILURE;

            return child.Query(agent);
        }
        catch
        {
            return BTNodeState.FAILURE;
        }
    }
}
