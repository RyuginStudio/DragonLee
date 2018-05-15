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
    public float CharacterAttackRate = 1.68f;

    private CharacterController m_characterController;

    private void Awake()
    {
        instance = this;
        status = CharacterStatus.Idle;
        m_characterController = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        characterGravity();
    }

    //状态机更新
    public void updateStatus(CharacterStatus status)
    {
        this.status = status;
    }

    public void characterGravity()
    {
        //考虑重力因素
        int Gravity = 20;
        var mDir = Vector3.zero;
        mDir = transform.TransformDirection(mDir);
        float y = mDir.y - Gravity * Time.deltaTime;

        mDir = new Vector3(mDir.x, y, mDir.z);
        m_characterController.Move(mDir);
    }
}
