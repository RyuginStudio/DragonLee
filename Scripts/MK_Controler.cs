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
    //鼠标右键按住
    public bool holdPressed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseDragMove();
    }

    public void MouseToMove()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))  //鼠标右键限定
        {
            holdPressed = true;
            //Debug.Log("MouseToMove()");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"));
            //Debug.Log(hitInfo.point);
            if (hitInfo.collider.tag == "Terrain")
            {
                Run.getInstance().RunToPos(hitInfo.point);              //坐标
                SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);  //朝向
            }
        }
    }

    public void MouseMoveFinish()
    {
        holdPressed = false;
    }

    public void MouseDragMove()
    {
        if (holdPressed && Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("MouseDragMove()");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"));
            //Debug.Log(hitInfo.point);
            if (hitInfo.collider.tag == "Terrain")
            {
                Run.getInstance().RunToPos(hitInfo.point);              //坐标
                SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);  //朝向
            }
        }
    }

    public void MoveAndAttack()
    {
        Debug.Log("Moveatatack");
    }
}
