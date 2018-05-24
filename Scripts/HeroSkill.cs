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
    public Dictionary<string, bool> skillStatus;

    private Animator m_animator;

    private static HeroSkill instance;

    public static HeroSkill getInstance()
    {
        return instance;
    }

    public GameObject Q_target;

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
            Q_target = null;
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

                if (skillStatus["Q_two_stage"] && this.Q_target != null)
                {
                    StartCoroutine(doSkill(1));
                    skillStatus["Q_two_stage"] = false;
                }
                else
                {
                    StartCoroutine(doSkill(0));
                    skillStatus["Q_two_stage"] = true;
                }

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                //StartCoroutine(doSkill(1));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                E_two_stage_update = Time.time;
                canDoNextSkill = false;

                if (skillStatus["E_two_stage"])
                {
                    StartCoroutine(doSkill(5));
                    skillStatus["E_two_stage"] = false;
                }
                else
                {
                    StartCoroutine(doSkill(4));
                    skillStatus["E_two_stage"] = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                canDoNextSkill = false;
                StartCoroutine(doSkill(6));
            }
        }
    }

    IEnumerator doSkill(int skillKind)
    {
        NormalAttack.getInstance().attackTargetObj = null;
        Run.getInstance().finishRun();
        m_animator.SetFloat("HeroSkillKind", skillKind);
        m_animator.SetBool("isHeroSkill", true);

        switch (skillKind)
        {
            case 0:
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer("Enemy"));
                    //SmoothLookAt.getInstance().Init_Rotate(hitInfo.point);
                    gameObject.transform.LookAt(hitInfo.point);
                    yield return new WaitForSeconds(.1f);
                    var prefabQ = Instantiate(Resources.Load("Prefab/BlindMonkQ"), GameObject.Find("Q_pos").transform.position, new Quaternion());

                    var vec1 = new Vector3(hitInfo.point.x, GameObject.Find("Q_pos").transform.position.y, hitInfo.point.z);
                    var vec2 = GameObject.Find("Q_pos").transform.position;
                    var dir = vec1 - vec2;
                    Ray rayToTarget = new Ray(vec2, dir);

                    ((GameObject)prefabQ).GetComponent<BlindMonkQ>().targetPos = rayToTarget.GetPoint(1000);
                    break;
                }
            case 1:
                {
                    Vector3 tarPos = new Vector3(Q_target.transform.position.x, GameObject.Find("Q_pos").transform.position.y, Q_target.transform.position.z);
                    //SmoothLookAt.getInstance().Init_Rotate(tarPos);
                    gameObject.transform.LookAt(tarPos);
                    Run.getInstance().RunToPos(tarPos, true);
                    Q_target = null;
                    break;
                }
            default:
                break;
        }
    }

    public void finishSkill()
    {
        m_animator.SetBool("isHeroSkill", false);
        canDoNextSkill = true;
    }

    public void canDoSkill()
    {
        m_animator.SetBool("isEndSkill", true);
    }
}