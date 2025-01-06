public class SelectorNode : CompositeNode
{
    private int index;
    protected override void OnActivate(BehaviourTreeAgent agent)
    {
        index = 0;
    }

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        BTNodeState nodeState = children[index].Query(agent);

        while (nodeState == BTNodeState.FAILURE)
        {
            index++;

            if (index == children.Count)
                return BTNodeState.FAILURE;

            nodeState = children[index].Query(agent);
        }

        if (nodeState == BTNodeState.SUCCESS)
            return BTNodeState.SUCCESS;

        return BTNodeState.RUNNING;
    }
}