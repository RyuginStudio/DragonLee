/*
 * 时间：2018年5月4日03:29:55
 * 修改：vszed
 * 功能：平滑LookAt
 * 来源：https://blog.csdn.net/chenggong2dm/article/details/71455670
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    private static SmoothLookAt instance;

    public static SmoothLookAt getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        Init_Rotate(new Vector3(116, 0, 92));  //角色初始化朝向
    }

    // 记录转身前的角度  
    private Quaternion raw_rotation;

    // 准备面向的角度  
    private Quaternion lookat_rotation;

    // 转身速度(每秒能转多少度)    
    private float per_second_rotate = 1080.0f;

    // 旋转角度越大, lerp变化速度就应该越慢   
    float lerp_speed = 0.0f;

    // lerp的动态参数  
    float lerp_tm = 0.0f;


    // 旋转之前的初始化  
    public void Init_Rotate(Vector3 targetPos)
    {
        // 记录转身前的角度
        raw_rotation = transform.rotation;

        // 记录目标角度
        transform.LookAt(targetPos);
        lookat_rotation = transform.rotation;

        // 还原当前角度
        transform.rotation = raw_rotation;

        // 计算旋转角度
        float rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);

        // 获得lerp速度
        lerp_speed = per_second_rotate / rotate_angle;
        //Debug.Log("Angle:" + rotate_angle.ToString() + " speed:" + lerp_speed.ToString());
        lerp_tm = 0.0f;
    }

    // 旋转逻辑, 在update中调用
    void Rotate_Func()
    {
        lerp_tm += Time.deltaTime * lerp_speed;
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, lerp_tm);
        if (lerp_tm >= 1)
        {
            transform.rotation = lookat_rotation;
            // 此时, 转身完毕, 已经对着目标物体
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Character.getInstance().status != Character.CharacterStatus.Die)
            Rotate_Func();

        //角度修正 => 确保角色rotation的x和z永远为0
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }
}
