using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BehaviourTreeAgent : MonoBehaviour
{
    public abstract IBlackboard GetBlackboard();
}

[CreateAssetMenu(menuName = "Behaviour Trees/Behaviour Tree")]
public class BehaviourTree : ScriptableObject
{
    [HideInInspector] public List<BTNode> nodes = new List<BTNode>();
    [HideInInspector] public BTNode rootNode;

    public void Reset()
    {
        rootNode.isActive = false;

        foreach (BTNode node in nodes)
        {
            node.isActive = false;
        }
    }

    public void Query(BehaviourTreeAgent behaviourTreeAgent)
    {
        rootNode.Query(behaviourTreeAgent);
    }

    public BTNode CreateNode(System.Type type, bool atToNodesList = true)
    {
        BTNode node = (BTNode)CreateInstance(type);

        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        if (atToNodesList)
            nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();

        return node;
    }

    public void DeleteNode(BTNode node)
    {
        nodes.Remove(node);

        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
}
