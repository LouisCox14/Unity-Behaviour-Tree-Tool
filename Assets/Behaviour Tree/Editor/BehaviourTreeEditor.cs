using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BehaviourTreeView m_BehaviourTreeView;
    private InspectorView m_InspectorView;

    [MenuItem("Window/AI/Behaviour Tree Editor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("Behaviour Tree Editor");
    }

    [OnOpenAsset]
    public static bool OpenGraphAsset(int instanceID, int line)
    {
        // This gets called whenever ANY asset is double clicked
        // So we gotta check if the asset is of the proper type
        BehaviourTree asset = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;
        if (!asset) 
            return false;

        bool windowIsOpen = HasOpenInstances<BehaviourTreeEditor>();

        if (!windowIsOpen) 
            CreateWindow<BehaviourTreeEditor>();
        else 
            FocusWindowIfItsOpen<BehaviourTreeEditor>();

        BehaviourTreeEditor window = GetWindow<BehaviourTreeEditor>();

        window.m_BehaviourTreeView.PopulateViewport(asset);

        return true;
    }


    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        m_VisualTreeAsset.CloneTree(root);

        // Sets the stylesheet
        root.parent.styleSheets.Add(GetStyleSheet());

        m_BehaviourTreeView = root.Q<BehaviourTreeView>();
        m_InspectorView = root.Q<InspectorView>();

        m_BehaviourTreeView.onNodeSelected = OnNodeSelected;

        OnSelectionChange();
    }

    public void OnSelectionChange()
    {
        BehaviourTree newTree = Selection.activeObject as BehaviourTree;

        if (!newTree)
            return;

        m_BehaviourTreeView.PopulateViewport(newTree);
    }

    public static StyleSheet GetStyleSheet()
    {
        string filePath = Directory.GetFiles(Application.dataPath, "BehaviourTreeEditor.uss", SearchOption.AllDirectories)[0];
        return AssetDatabase.LoadAssetAtPath<StyleSheet>(filePath.Split('/').Last());
    }

    void OnNodeSelected(NodeView nodeView)
    {
        m_InspectorView.UpdateSelection(nodeView);
    }
}
