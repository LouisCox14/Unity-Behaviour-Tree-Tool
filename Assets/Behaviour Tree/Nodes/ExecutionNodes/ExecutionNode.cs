public abstract class ExecutionNode : BTNode
{
    protected abstract override BTNodeState QueryEx(BehaviourTreeAgent agent);
}