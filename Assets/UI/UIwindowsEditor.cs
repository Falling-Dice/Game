using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class UIwindowsEditor : EditorWindow
{
    [MenuItem("Window/UI Toolkit/UIwindowsEditor")]
    public static void ShowExample()
    {
        UIwindowsEditor wnd = GetWindow<UIwindowsEditor>();
        wnd.titleContent = new GUIContent("UIwindowsEditor");
    }

    public void CreateGUI()
    {
        VisualElement container = new VisualElement();
        rootVisualElement.Add(container);
        Label title = new Label("Start");
        Label biggerTitle = new Label("Exit");

        container.Add(title);
        container.Add(biggerTitle);

        Debug.Log(container.panel);
        Debug.Log(rootVisualElement.panel);
    }
}