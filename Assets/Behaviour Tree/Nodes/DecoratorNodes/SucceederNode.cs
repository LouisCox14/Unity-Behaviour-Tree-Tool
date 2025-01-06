public class SucceederNode : DecoratorNode
{
    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        child.Query(agent);
        return BTNodeState.SUCCESS;
    }
}