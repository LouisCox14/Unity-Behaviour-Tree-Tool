using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

public class NodeView : Node
{
    public BTNode node;

    public Port input;
    public Port output;

    public Action<NodeView> onNodeSelected;

    public NodeView(BTNode node)
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;

        style.left = node.gridPos.x;
        style.top = node.gridPos.y;

        if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "";
            outputContainer.Add(output);

            return;
        }

        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        input.portName = "";
        inputContainer.Add(input);

        if (node is DecoratorNode)
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        else if (node is CompositeNode)
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        if (output != null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        node.gridPos = new Vector2(newPos.x, newPos.y);
        node.onNodeMoved?.Invoke(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        onNodeSelected?.Invoke(this);
    }
}
