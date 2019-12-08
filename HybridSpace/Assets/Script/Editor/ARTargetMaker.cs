using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ARTargetMaker : EditorWindow
{
    [MenuItem("Window/UIElements/ARTargetMaker")]
    public static void Init()
    {
        ARTargetMaker window = GetWindow<ARTargetMaker>();
        window.titleContent = new GUIContent("ARTargetMaker");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Making an AR Target with this Editor Window");
        VisualElement API = new Label("Paste API link");
        VisualElement text = new TextField();
        
        VisualElement Image = new Image();
        root.Add(label);
        root.Add(API);
        root.Add(text);
        //Add Target

        //target
        root.Add(Image);
         
    }
}