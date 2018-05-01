using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
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

    }

    public void doRun()
    {
        m_animator.SetBool("isRun", true);
    }

    public void finishRun()
    {
        m_animator.SetBool("isRun", false);
    }
}
