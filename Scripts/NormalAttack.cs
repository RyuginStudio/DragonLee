/*
 * 时间：2018年5月1日16:10:38
 * 作者：vszed
 * 功能：英雄普攻
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public float AttackRate = 3;
    public float currentTime;
    public float AttackRateUpdate;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        doAttack();
    }

    /*
     * 执行普攻条件：
     * 1.角色攻速
     * 2.角色生存
     * 3.被攻击者生存 
     * 4.角色无"眩晕"等状态   
    */
    void doAttack()
    {
        if (currentTime - AttackRateUpdate > AttackRate)// && TODO:满足以上条件)
        {
            AttackRateUpdate = Time.time;
            m_animator.SetFloat("NormalAttackWay", Random.Range(0, 4));  //普攻四种攻击动画
            m_animator.SetBool("isNormalAttack", true);           
        }
    }
}