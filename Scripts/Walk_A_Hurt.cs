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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidAttacks()
    {
        if (NormalAttack.getInstance().attackTargetObj)  //有效攻击
        {
            Debug.Log("ValidAttacks()");
            NormalAttack.getInstance().attackTargetObj.GetComponent<InjureSystem>().beInjured(100);

            //攻击特效
            var sfxAudio = GameObject.Find("SFX").GetComponent<AudioSource>();
            sfxAudio.clip = GameObject.FindWithTag("Player").GetComponentInChildren<HeroSounds>().normalAttackSFX[Random.Range(0, GameObject.FindWithTag("Player").GetComponentInChildren<HeroSounds>().normalAttackSFX.Length)];
            sfxAudio.Play();
        }
    }
}
