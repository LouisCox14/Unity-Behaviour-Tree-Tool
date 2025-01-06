public class SequenceNode : CompositeNode
{
    private int index;
    protected override void OnActivate(BehaviourTreeAgent agent)
    {
        index = 0;
    }

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        BTNodeState nodeState = children[index].Query(agent);

        if (nodeState == BTNodeState.FAILURE)
            return BTNodeState.FAILURE;

        if (nodeState == BTNodeState.SUCCESS)
            index++;

        if (index == children.Count)
            return BTNodeState.SUCCESS;

        return BTNodeState.RUNNING;
    }
}