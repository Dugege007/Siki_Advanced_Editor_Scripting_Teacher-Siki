using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    // 设置窗口菜单
    [MenuItem("Window/MyWindow")]
    static void ShowMyWindow()
    {
        // 获取一个 MyWindow 对象
        MyWindow myWindow = GetWindow<MyWindow>();
        // 显示窗口
        myWindow.Show();
    }

    private string GOname = "";

    private void OnGUI()
    {
        // 内容标题
        GUILayout.Label("这是我的窗口");
        // 输入框
        GOname = GUILayout.TextField(GOname);

        // 如果点击按钮
        if (GUILayout.Button("Button"))
        {
            // 创建一个名为 GOname 的游戏物体
            GameObject go = new GameObject(GOname);
            // 注册一个操作记录，使创建物体的操作可以撤销
            Undo.RegisterCreatedObjectUndo(go, "Create GameObject");
        }
    }
}
