/*
 * 时间：2018年5月3日22:52:56
 * 作者：vszed
 * 功能：键鼠控制
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MK_Controler : MonoBehaviour
{
    //必须用EventTrigger限定开关
    public bool MoveOrAttackSwitch = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseToMove();
    }

    public void MouseRightButtonDown()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            MoveOrAttackSwitch = true;
        }
    }
    public void MouseRightButtonUp()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            MoveOrAttackSwitch = false;
        }
    }

    public void MouseToMove()
    {
        if (Input.GetKey(KeyCode.Mouse1) && MoveOrAttackSwitch)  //鼠标右键限定
        {
            //Debug.Log("MouseToMove()");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer("Enemy"));
            //Debug.Log(hitInfo.point);
            if (hitInfo.collider.tag == "Terrain")
            {
                //Debug.Log("Terrain");
                Run.getInstance().RunToPos(hitInfo.point);              //坐标
                SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);  //朝向

                NormalAttack.getInstance().attackTargetObj = null;
            }
            else if (hitInfo.collider.tag == "Enemy")
            {
                //Debug.Log("Enemy");
                if (NormalAttack.getInstance().attackTargetObj != null)  //如果以前有目标需要重刷beIn的值
                    NormalAttack.getInstance().beInAttackStatus = false;
                NormalAttack.getInstance().attackTargetObj = hitInfo.collider.GetComponentInParent<Enemy>().gameObject;
            }
            else
            {
                NormalAttack.getInstance().attackTargetObj = null;  //清空普攻目标
            }

        }
    }
}
