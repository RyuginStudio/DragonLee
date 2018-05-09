using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //HP 生命值
    public float HealthPoint = 100;

    //SP 法力值
    public float MagicPoint = 100;

    private static Character instance;

    public static Character getInstance()
    {
        return instance;
    }

    public enum CharacterStatus  //TODO:后期加入禁锢、晕眩等
    {
        Idle,
        Run,
        NormalAttack,
        Skill,
        Die
    }
    public CharacterStatus status;

    //攻速
    public float CharacterAttackRate = 1;

    private void Awake()
    {
        instance = this;
        status = CharacterStatus.Idle;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //状态机更新
    public void updateStatus(CharacterStatus status)
    {
        this.status = status;
    }
}
