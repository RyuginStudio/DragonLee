using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public bool isRun = false;
    public float runSpeed = 8;
    public Vector3 runTargetPos;

    public bool isSkill = false;

    private static Run instance;

    public static Run getInstance()
    {
        return instance;
    }

    private Animator m_animator;
    private CharacterController m_characterController;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_characterController = GetComponent<CharacterController>();
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        doRun();
    }

    public void RunToPos(Vector3 pos, bool isSkill = false)
    {
        this.isSkill = isSkill;

        if (!isSkill)
        {
            this.runTargetPos = pos;
            m_animator.SetBool("isRun", true);
            isRun = true;
            //transform.LookAt(pos);
        }
        else
        {
            this.runTargetPos = pos;
            isRun = true;
        }
    }

    public void doRun()
    {
        if (!isSkill && this.isRun && Character.getInstance().status == Character.CharacterStatus.Run)
        {
            Vector3 v = Vector3.ClampMagnitude(runTargetPos - transform.position, runSpeed * Time.deltaTime);  //注解3 限制移动       
            m_characterController.Move(v);

            if (Mathf.Abs(Vector3.Distance(transform.position, runTargetPos)) <= .5f)
                finishRun();
        }
        else if (isSkill)
        {
            Vector3 v = Vector3.ClampMagnitude(runTargetPos - transform.position, runSpeed * Time.deltaTime);  //注解3 限制移动       
            m_characterController.Move(v);
        }
    }

    public void finishRun()
    {
        isRun = false;
        m_animator.SetBool("isRun", false);
        //Debug.Log("finishRun()");      
    }
}
