using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeView : GraphView
{
    public Action<NodeView> onNodeSelected;

    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

    private BehaviourTree behaviourTree;

    public BehaviourTreeView() 
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        styleSheets.Add(BehaviourTreeEditor.GetStyleSheet());
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        Vector2 placementPos = viewTransform.matrix.inverse.MultiplyPoint(evt.localMousePosition);

        List<Type> types = TypeCache.GetTypesDerivedFrom<ExecutionNode>().ToList();
        types.AddRange(TypeCache.GetTypesDerivedFrom<CompositeNode>());
        types.AddRange(TypeCache.GetTypesDerivedFrom<DecoratorNode>());

        foreach (Type type in types)
            evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", (a) => CreateNode(type, placementPos));
    }

    void CreateNode(Type type, Vector2 position)
    {
        if (!type.IsSubclassOf(typeof(BTNode)))
            return;

        BTNode node = behaviourTree.CreateNode(type);
        node.gridPos = position;
        CreateNodeView(node);
    }

    internal void PopulateViewport(BehaviourTree behaviourTree)
    {
        this.behaviourTree = behaviourTree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (!behaviourTree.rootNode)
            behaviourTree.rootNode = behaviourTree.CreateNode(typeof(RootNode), false);

        NodeView rootNode = CreateNodeView(behaviourTree.rootNode);

        foreach (BTNode node in behaviourTree.nodes)
        {
            CreateNodeView(node);
        }

        if (rootNode.node.GetChildren().Count > 0)
        {
            
            NodeView childView = GetNodeView(rootNode.node.GetChildren()[0]);

            Edge edge = rootNode.output.ConnectTo(childView.input);
            AddElement(edge);
        }

        foreach (BTNode node in behaviourTree.nodes)
        {
            NodeView parentView = GetNodeView(node);

            foreach (BTNode childNode in node.GetChildren())
            {
                NodeView childView = GetNodeView(childNode);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            }
        }
    }

    public NodeView CreateNodeView(BTNode node)
    {
        NodeView nodeView = new NodeView(node);
        AddElement(nodeView);

        nodeView.onNodeSelected = onNodeSelected;
        return nodeView;
    }

    public NodeView GetNodeView(BTNode node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (GraphElement element in graphViewChange.elementsToRemove)
            {
                if (element is NodeView)
                {
                    NodeView nodeView = element as NodeView;
                    behaviourTree.DeleteNode(nodeView.node);
                }
                else if (element is Edge)
                {
                    Edge edge = element as Edge;

                    NodeView parentNode = edge.output.node as NodeView;
                    NodeView childNode = edge.input.node as NodeView;

                    parentNode.node.RemoveChild(childNode.node);
                }
            }
        }

        if (graphViewChange.edgesToCreate != null)
        {
            foreach (Edge edge in graphViewChange.edgesToCreate)
            {
                NodeView parentNode = edge.output.node as NodeView;
                NodeView childNode = edge.input.node as NodeView;

                parentNode.node.AddChild(childNode.node);
                EditorUtility.SetDirty(parentNode.node);
                AssetDatabase.SaveAssets();
            }
        }

        return graphViewChange;
    }
}
