/*
 * 时间：2018年5月1日16:10:38
 * 作者：vszed
 * 功能：英雄普攻 => 普通攻击造成的伤害在脚本Walk_A_Hurt
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
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
        m_animator.SetBool("isNormalAttack", false);
    }

    //存放触发器内敌人
    public List<GameObject> EnemyTriggerList;

    private void OnTriggerEnter(Collider other)
    {
        //进入触发器的敌人加入列表
        foreach (var item in EnemyTriggerList)
        {
            if (!other.GetComponentInParent<Enemy>() || item == other.GetComponentInParent<Enemy>().gameObject)
                return;
        }

        if (other.GetComponentInParent<Enemy>())
            EnemyTriggerList.Add(other.GetComponentInParent<Enemy>().gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        //停留触发器的敌人加入列表
        foreach (var item in EnemyTriggerList)
        {
            if (!other.GetComponentInParent<Enemy>() || item == other.GetComponentInParent<Enemy>().gameObject)
                return;
        }

        if (other.GetComponentInParent<Enemy>())
            EnemyTriggerList.Add(other.GetComponentInParent<Enemy>().gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //删除离开触发器的敌人
        for (var idx = 0; idx < EnemyTriggerList.Count; idx++)  //不能用forEach报异常
        {
            if (other.GetComponentInParent<Enemy>() && EnemyTriggerList[idx] == other.GetComponentInParent<Enemy>().gameObject)
                EnemyTriggerList.Remove(EnemyTriggerList[idx]);
        }
    }

    public void MoveAndAttackTargetObj()
    {
        if (attackTargetObj)
        {
            foreach (var item in EnemyTriggerList)
            {
                if (item == attackTargetObj)
                {
                    Run.getInstance().finishRun();
                    SmoothLookAt.getInstance().Init_Rotate(attackTargetObj.transform.position);  //朝向    
                    doAttack();
                    return;
                }
            }
            Run.getInstance().RunToPos(attackTargetObj.transform.position);              //坐标
            SmoothLookAt.getInstance().Init_Rotate(attackTargetObj.transform.position);  //朝向      
        }
    }
}