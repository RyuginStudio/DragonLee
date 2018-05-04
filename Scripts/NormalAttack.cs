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
    //已经进入攻击状态
    public bool beInAttackStatus = false;

    //普攻目标
    public GameObject attackTargetObj;

    //用于攻速
    public float currentTime;
    public float AttackRateUpdate;

    private Animator m_animator;

    private static NormalAttack instance;
    public static NormalAttack getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
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

        MoveAndAttackTargetObj();

        //doAttack();
    }

    /*
     * 执行普攻条件：
     * 1.角色攻速
     * 2.角色生存
     * 3.被攻击者生存
     * 4.角色无"眩晕"等状态
    */
    public void doAttack()
    {
        // && TODO:满足以上条件)
        if (currentTime - AttackRateUpdate > Character.getInstance().CharacterAttackRate
            && Character.getInstance().status != Character.CharacterStatus.Die)
        {
            AttackRateUpdate = Time.time;
            m_animator.SetFloat("NormalAttackWay", Random.Range(0, 4));  //普攻四种攻击动画
            m_animator.SetBool("isNormalAttack", true);
        }
    }

    public void finishAttack()
    {
        //TODO:被攻击者伤血
        m_animator.SetBool("isNormalAttack", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attackTargetObj)
            beInAttackStatus = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == attackTargetObj)
            beInAttackStatus = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == attackTargetObj)
            beInAttackStatus = false;
    }

    public void MoveAndAttackTargetObj()
    {
        if (attackTargetObj && !beInAttackStatus)
        {
            //Debug.Log("MoveAndAttackTargetObj()");
            Run.getInstance().RunToPos(attackTargetObj.transform.position);              //坐标
            SmoothLookAt.getInstance().Init_Rotate(attackTargetObj.transform.position);  //朝向
        }
        else if (attackTargetObj && beInAttackStatus)
        {
            Run.getInstance().finishRun();
            doAttack();
        }
        else if (!attackTargetObj)
        {
            beInAttackStatus = false;
        }
    }
}