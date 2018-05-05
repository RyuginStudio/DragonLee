/*
 * 时间：2018年5月5日13:13:02
 * 作者：vszed
 * 功能：受伤系统（挂载到所有活体生物上）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjureSystem : MonoBehaviour
{
    private static InjureSystem instance;

    public static InjureSystem getInstance()
    {
        return instance;
    }

    private void Awake()
    {
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

    //承受value伤害
    public void beInjured(float value)
    {
        if (GetComponent<Character>())
        {
            Character.getInstance().blood -= value;
        }
        else if (GetComponent<Enemy>())
        {
            Enemy.getInstance().blood -= value;
        }
    }
}
