using UnityEngine;

public class FloatComparisonNode : DecoratorNode
{
    [SerializeField] private string firstFloatBlackboardKey;
    [SerializeField] private Operators comparison;
    [SerializeField] private string secondFloatBlackboardKey;
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        float firstFloat = 0;
        float secondFloat = 0;

        try
        {
            firstFloat = (float)agent.GetBlackboard().GetValue(firstFloatBlackboardKey);
            secondFloat = (float)agent.GetBlackboard().GetValue(secondFloatBlackboardKey);
        }
        catch
        {
            return BTNodeState.FAILURE;
        }

        switch (comparison)
        {
            case Operators.LESS_THAN:
                return firstFloat < secondFloat ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.LESS_THAN_OR_EQUALS:
                return firstFloat <= secondFloat ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.EQUALS:
                return firstFloat == secondFloat ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.GREATER_THAN_OR_EQUALS:
                return firstFloat >= secondFloat ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.GREATER_THAN:
                return firstFloat > secondFloat ? child.Query(agent) : BTNodeState.FAILURE;
            default:
                return BTNodeState.FAILURE;
        }
    }
}
