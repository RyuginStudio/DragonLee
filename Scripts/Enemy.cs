/*
 * 时间：2018年5月5日14:15:34
 * 作者：vszed
 * 功能：敌人类
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum em_Enemy
    {
        DragonLee,
        RedBrambleback,  //绯红印记树怪
        box
    }

    //敌人种类
    public em_Enemy enemyKind;

    //HP 生命值
    public float HealthPoint = 100;

    //SP 法力值
    public float MagicPoint = 100;

    //敌人攻击目标
    public GameObject attackTargetObj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void confirmAttackTarget(GameObject target)
    {
        attackTargetObj = target;
    }
}
