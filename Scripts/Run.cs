using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public bool isRun = false;
    public float runSpeed = 1;
    public Vector3 runTargetPos;

    private static Run instance;

    public static Run getInstance()
    {
        return instance;
    }

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
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

    public void RunToPos(Vector3 pos)
    {
        this.runTargetPos = pos;
        m_animator.SetBool("isRun", true);
        isRun = true;
        //transform.LookAt(pos);
    }

    public void doRun()
    {
        if (this.isRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, this.runTargetPos, this.runSpeed * Time.deltaTime);

            if (transform.position == runTargetPos)
            {
                //Debug.Log(runTargetPos);
                finishRun();
            }
        }
    }

    public void finishRun()
    {
        isRun = false;
        m_animator.SetBool("isRun", false);
        //Debug.Log("finishRun()");      
    }
}
