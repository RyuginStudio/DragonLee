/*
 * 时间：2018年5月20日18:45:03
 * 作者：vszed
 * 功能：角色技能 => q、w、e、r
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkill : MonoBehaviour
{
    private float currentTime;
    private float Q_two_stage_update;
    private float W_two_stage_update;
    private float E_two_stage_update;
    public float canDoTwoStageTime;
    public bool canDoNextSkill = true;


    //技能状态：true为可激活第二段
    Dictionary<string, bool> skillStatus;

    private Animator m_animator;

    private static HeroSkill instance;

    public static HeroSkill getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        currentTime = Time.time;
        Q_two_stage_update = Time.time;
        W_two_stage_update = Time.time;
        E_two_stage_update = Time.time;

        m_animator = GetComponentInChildren<Animator>();
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        skillStatus = new Dictionary<string, bool>();

        skillStatus.Add("Q_two_stage", false);
        skillStatus.Add("W_two_stage", false);
        skillStatus.Add("E_two_stage", false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        two_stage_destroy();
        prepareSkill();
    }

    //一定时间不触发2段技能则销毁2段技能
    void two_stage_destroy()
    {
        if (skillStatus["Q_two_stage"] && currentTime - Q_two_stage_update > canDoTwoStageTime)
        {
            Q_two_stage_update = Time.time;
            skillStatus["Q_two_stage"] = false;
        }
        if (skillStatus["W_two_stage"] && currentTime - W_two_stage_update > canDoTwoStageTime)
        {
            W_two_stage_update = Time.time;
            skillStatus["W_two_stage"] = false;
        }
        if (skillStatus["E_two_stage"] && currentTime - E_two_stage_update > canDoTwoStageTime)
        {
            E_two_stage_update = Time.time;
            skillStatus["E_two_stage"] = false;
        }
    }

    void prepareSkill()
    {
        if (canDoNextSkill)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Q_two_stage_update = Time.time;
                canDoNextSkill = false;

                if (skillStatus["Q_two_stage"])
                {
                    doSkill(1);
                    skillStatus["Q_two_stage"] = false;
                }
                else
                {
                    doSkill(0);
                    skillStatus["Q_two_stage"] = true;
                }

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                //doSkill();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                E_two_stage_update = Time.time;
                canDoNextSkill = false;

                if (skillStatus["E_two_stage"])
                {
                    doSkill(5);
                    skillStatus["E_two_stage"] = false;
                }
                else
                {
                    doSkill(4);
                    skillStatus["E_two_stage"] = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                canDoNextSkill = false;
                doSkill(6);
            }
        }
    }

    void doSkill(int skillKind)
    {
        NormalAttack.getInstance().attackTargetObj = null;
        Run.getInstance().finishRun();
        m_animator.SetFloat("HeroSkillKind", skillKind);
        m_animator.SetBool("isHeroSkill", true);

        switch (skillKind)
        {
            default:
                break;
        }
    }

    public void finishSkill()
    {
        m_animator.SetBool("isHeroSkill", false);
        canDoNextSkill = true;
    }
}
