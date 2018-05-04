/*
 * 时间：2018年5月5日02:40:25
 * 作者：vszed
 * 功能：平A伤害 => 完成动画走A效果，所以要挂载到model上
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_A_Hurt : MonoBehaviour
{
    public int score;
    private float currentTime;
    private float update;

    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        update = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
    }

    //平A伤害
    public void ValidAttacks()
    {
        score += 10;
        if (score == 150)
        {
            float costTime = currentTime - update;
            Debug.Log(costTime);
        }
    }
}
