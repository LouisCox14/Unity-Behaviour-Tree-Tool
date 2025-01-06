public class RepeaterNode : DecoratorNode
{
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        if (child.Query(agent) == BTNodeState.SUCCESS)
            return BTNodeState.SUCCESS;

        return BTNodeState.RUNNING;
    }
}