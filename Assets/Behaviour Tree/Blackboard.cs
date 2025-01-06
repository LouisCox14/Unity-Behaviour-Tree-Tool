using System.Collections.Generic;

public interface IBlackboard
{
    public object GetValue(string key);
    public void SetValue(string key, object value);
    public void Clear();
}

public class Blackboard : IBlackboard
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public void AddValue(string key, object value)
    {
        data.Add(key, value);
    }

    public object GetValue(string key)
    {
        if (data.ContainsKey(key))
            return data[key];

        return null;
    }

    public void SetValue(string key, object value)
    {
        if (data.ContainsKey(key))
            data[key] = value;
        else
            data.Add(key, value);
    }

    public void Clear()
    {
        data.Clear();
    }
}