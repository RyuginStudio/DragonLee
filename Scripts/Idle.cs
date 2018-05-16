/*
 * 时间：2018年5月1日22:41:54
 * 作者：vszed
 * 功能：idle动画的选择和切换
 * 注意：需要放置在Model下 => idle播放完后的动画事件
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private static Idle instance;
    public static Idle getInstance()
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

    public void doIdleAnm()
    {
        m_animator.SetFloat("IdleWay", Random.Range(0, 5));
    }

}