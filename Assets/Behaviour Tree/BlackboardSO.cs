using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Trees/Blackboard")]
public class BlackboardSO : ScriptableObject, IBlackboard
{
    private Blackboard blackboard = new Blackboard();

    public object GetValue(string key)
    {
        return blackboard.GetValue(key);
    }

    public void SetValue(string key, object value)
    {
        blackboard.SetValue(key, value);
    }

    public void Clear()
    {
        blackboard.Clear();
    }
}
