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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MouseToMove()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))  //鼠标右键限定
        {
            //Debug.Log("MouseToMove()");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            //Debug.Log(hitInfo.point);
            Run.getInstance().RunToPos(hitInfo.point);              //坐标
            SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);  //朝向
        }
    }
}
