using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 继承自可编程窗口类
public class EnemyChange : ScriptableWizard
{
    [MenuItem("Tools/CreateWizard")]
    static void CreateWizard()
    {
        // 创建此窗口
        DisplayWizard<EnemyChange>("统一修改敌人", "Change And Close", "Change");
    }

    public int changeHealthValue = 10;
    public int changeSinkValue = 1;

    private const string changeHealthValueKey = "EnemyChange.changeHealthValue";
    private const string changeSinkValueKey = "EnemyChange.changeSinkValue";

    // 当窗口弹出时调用
    private void OnEnable()
    {
        // 加载数据
        changeHealthValue = EditorPrefs.GetInt(changeHealthValueKey, changeHealthValue);
        changeSinkValue = EditorPrefs.GetInt(changeSinkValueKey, changeSinkValue);
    }

    // 在点击 Create 按钮后，窗口关闭前会执行
    private void OnWizardCreate()
    {
        //Debug.Log("统一修改敌人");
        GameObject[] enemyPrefabs = Selection.gameObjects;

        // 显示进度条
        EditorUtility.DisplayProgressBar("进度", "0/" + enemyPrefabs.Length + " 完成修改值", 0);
        int count = 0;

        foreach (var go in enemyPrefabs)
        {
            // 使用正确的命名空间（正常情况下用 EnemyHealth 即可）
            CompleteProject.EnemyHealth hp = go.GetComponent<CompleteProject.EnemyHealth>();

            // 使用 Undo.RecordObject() 记录以下修改的物体，使操作可以撤回
            Undo.RecordObject(hp, "Change Health and Speed");
            hp.startingHealth += changeHealthValue;
            hp.sinkSpeed += changeSinkValue;

            count++;
            // 更新进度条
            EditorUtility.DisplayProgressBar("进度", count + "/" + enemyPrefabs.Length + " 完成修改值", count / (float)enemyPrefabs.Length);
        }

        // 关闭进度条
        EditorUtility.ClearProgressBar();
        // 弹出内置提示框
        ShowNotification(new GUIContent(Selection.gameObjects.Length + "游戏物体的值被修改了"));
    }

    private void OnWizardOtherButton()
    {
        OnWizardCreate();
    }

    // 当前窗口弹出时，和窗口中的字段值被修改时（刷新）调用。
    private void OnWizardUpdate()
    {
        //Debug.Log("OnWizardUpdate");

        // 每次刷新时先清空提示信息
        helpString = null;
        errorString = null;

        if (Selection.gameObjects.Length > 0)
            helpString = "当前选择了" + Selection.gameObjects.Length + "个敌人";
        else
            errorString = "请选择至少一个敌人";

        // 保存当前输入值
        EditorPrefs.SetInt(changeHealthValueKey, changeHealthValue);
        EditorPrefs.SetInt(changeSinkValueKey, changeSinkValue);
    }

    private void OnSelectionChange()
    {
        OnWizardUpdate();
    }
}
