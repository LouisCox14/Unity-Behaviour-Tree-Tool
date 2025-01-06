using UnityEngine;

public class IntCheckNode : DecoratorNode
{
    [SerializeField] private string firstIntBlackboardKey;
    [SerializeField] private Operators comparison;
    [SerializeField] private int secondInt;
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        int firstInt = 0;

        try
        {
            firstInt = (int)agent.GetBlackboard().GetValue(firstIntBlackboardKey);
        }
        catch
        {
            return BTNodeState.FAILURE;
        }

        switch (comparison)
        {
            case Operators.LESS_THAN:
                return firstInt < secondInt ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.LESS_THAN_OR_EQUALS:
                return firstInt <= secondInt ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.EQUALS:
                return firstInt == secondInt ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.GREATER_THAN_OR_EQUALS:
                return firstInt >= secondInt ? child.Query(agent) : BTNodeState.FAILURE;
            case Operators.GREATER_THAN:
                return firstInt > secondInt ? child.Query(agent) : BTNodeState.FAILURE;
            default:
                return BTNodeState.FAILURE;
        }
    }
}
