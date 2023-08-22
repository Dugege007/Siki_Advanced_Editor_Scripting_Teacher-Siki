using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerEditor
{
    // 在Inspector面板中某组件的右键菜单中添加选项
    // CONTEXT/组件名/选项名
    // 在 PlayerHealth （脚本）组件上添加右键菜单选项
    [MenuItem("CONTEXT/PlayerHealth/InitHealthAndSpeed")]
    static void InitHealthAndSpeed(MenuCommand menuCommand) // MenuCommand是当前正在操作的组件对象
    {
        Debug.Log(menuCommand.context.name);
        //Debug.Log("InitHealthAndSpeed");

        // 获取当前脚本
        CompleteProject.PlayerHealth playerHealth = menuCommand.context as CompleteProject.PlayerHealth;
        // 修改脚本中的属性
        playerHealth.startingHealth = 20;
    }

    // 在 Rigidbody 组件上添加右键菜单选项
    [MenuItem("CONTEXT/Rigidbody/ClearMassAndGravity")]
    static void ClearMassAndGravity(MenuCommand menuCommand)
    {
        Rigidbody rigidbody = menuCommand.context as Rigidbody;
        rigidbody.mass = 0;
        rigidbody.useGravity = false;
    }
}
