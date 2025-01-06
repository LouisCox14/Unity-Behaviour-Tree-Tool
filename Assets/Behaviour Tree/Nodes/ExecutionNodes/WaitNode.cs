using UnityEngine;

public class WaitNode : ExecutionNode
{
    [SerializeField] private float waitTime;
    private float timer;
    protected override void OnActivate(BehaviourTreeAgent agent)
    {
        timer = 0f;
    }

    protected override BTNodeState QueryEx(BehaviourTreeAgent agent)
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
            return BTNodeState.SUCCESS;

        return BTNodeState.RUNNING;
    }
}