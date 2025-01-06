public class InverterNode : DecoratorNode
{
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        switch (child.Query(agent))
        {
            case BTNodeState.SUCCESS: return BTNodeState.FAILURE;
            case BTNodeState.FAILURE: return BTNodeState.SUCCESS;
            default: return BTNodeState.RUNNING;
        }
    }
}