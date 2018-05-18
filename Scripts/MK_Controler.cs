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
        //摄像机偏移量
        offset = trans_character.position - trans_mainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        MouseToMove();
        SpaceToLocateSelf();
        cameraCroll();
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
            if (hitInfo.collider.tag == "Terrain"
                && Mathf.Abs(Vector3.Distance(GameObject.FindWithTag("Player").transform.position, hitInfo.point)) > 1)  //加限制
            {
                //Debug.Log("Terrain");
                Run.getInstance().RunToPos(hitInfo.point);              //坐标
                SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);  //朝向

                NormalAttack.getInstance().attackTargetObj = null;
            }
            else if (hitInfo.collider.tag == "Enemy")
            {
                NormalAttack.getInstance().attackTargetObj = hitInfo.collider.GetComponentInParent<Enemy>().gameObject;
            }
            else
            {
                NormalAttack.getInstance().attackTargetObj = null;  //清空普攻目标
            }

        }
    }

    public Transform trans_mainCamera;
    public Transform trans_character;
    private Vector3 offset;
    public bool lockView;
    //按下空格 => Camera定位到角色自身
    public void SpaceToLocateSelf()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            trans_mainCamera.position = Vector3.Lerp(trans_mainCamera.position, trans_character.position - offset, Time.deltaTime * 100);
            lockView = true;
        }
        else
        {
            lockView = false;
        }
    }

    //镜头滚动
    public void cameraCroll()
    {
        if (lockView == false) //视角未锁定前提
        {
            if (Input.mousePosition.x >= Screen.width - 25)
                trans_mainCamera.position = new Vector3(trans_mainCamera.position.x + 20 * Time.deltaTime, trans_mainCamera.position.y, trans_mainCamera.position.z);
            else if (Input.mousePosition.x <= 25)
                trans_mainCamera.position = new Vector3(trans_mainCamera.position.x - 20 * Time.deltaTime, trans_mainCamera.position.y, trans_mainCamera.position.z);
            if (Input.mousePosition.y >= Screen.height - 25)
                trans_mainCamera.position = new Vector3(trans_mainCamera.position.x, trans_mainCamera.position.y, trans_mainCamera.position.z + 20 * Time.deltaTime);
            else if (Input.mousePosition.y <= 25)
                trans_mainCamera.position = new Vector3(trans_mainCamera.position.x, trans_mainCamera.position.y, trans_mainCamera.position.z - 20 * Time.deltaTime);
        }
    }
}
