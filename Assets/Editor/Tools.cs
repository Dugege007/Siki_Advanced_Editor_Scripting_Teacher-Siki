using UnityEditor;
using UnityEngine;

public class Tools
{
    // 在菜单栏中添加菜单按钮
    // 方法不管是 public 还是 private，Unity 都有权限访问到，必须是 static 的
    [MenuItem("MenuTools/Test/test1", false, 1)]
    static void Test1()
    {
        Debug.Log("Test1");
    }

    // 每一个菜单栏的 priority 优先级默认为1000
    // priority 越小，菜单项越靠前
    // 相邻的两个按钮的 priority 相差>11时，菜单中间会加分割线
    [MenuItem("MenuTools/Test/test2", false, 13)]
    static void Test2()
    {
        Debug.Log("Test2");
    }

    // 在Window菜单中添加选项
    [MenuItem("Window/MyTest/test3")]
    static void Test3()
    {
        Debug.Log("Test3");
    }

    // 在GameObject菜单中添加选项
    // 如果想在Hierarchy面板的右键菜单中添加选项，priority需要尽量靠前
    [MenuItem("GameObject/MyTool")]
    static void Test4()
    {
        Debug.Log("Test4");
    }

    // 选择物体
    [MenuItem("GameObject/MySelection")]
    static void SelectionGameObject()
    {
        //Debug.Log("Select GameObject");
        if (Selection.activeGameObject != null)
        {
            // Selection 选中的物体，可以是 Hierarchy 面板中的，也可以是 Project 面板中的
            Debug.Log(Selection.activeGameObject.name); // 假如多选，只输出选择的第一个
        }

        if (Selection.objects != null)
        {
            Debug.Log(Selection.objects.Length); // 输出选中物体的数量
        }
    }

    // 删除物体
    // _D 表示快捷键
    // %=Ctrl, #=Shift, &=Alt，例如想要使用 Ctrl+Q 作为快捷键，可以输入 %Q
    // 快捷键可以在 Edit -> Shortcuts 中设置
    [MenuItem("GameObject/MyDelete _D", false, 1)]
    static void MyDelete()
    {
        foreach (var obj in Selection.objects)
        {
            // 需要把删除操作注册到操作记录里面，才可以撤回操作
            //Object.DestroyImmediate(obj);

            // 把删除操作注册到操作记录里面
            Undo.DestroyObjectImmediate(obj);   // 用 Undo 进行的操作是可以撤销的
        }
    }

    // 当 isValidateFunction 为 true 时，表示为同选项（如上方选项）做验证
    // 需要有 bool 类型的返回值，返回 true 使上方选项可以点击，返回 false 使其不能点击
    [MenuItem("GameObject/MyDelete _D", true, 1)]
    static bool MyDeleteValidate()
    {
        if (Selection.objects.Length > 0)
            return true;
        else
        {
            Debug.Log("未选择物体");
            return false;
        }
    }

}
