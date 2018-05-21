/*
 * 时间：2018年5月20日21:31:02
 * 作者：vszed
 * 功能：天音波Q挂载到天音波预制体上
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindMonkQ : MonoBehaviour
{
    public Vector3 targetPos;

    private void Awake()
    {
        Destroy(gameObject.GetComponent<MeshRenderer>(), .4f);
        Destroy(gameObject.GetComponent<SphereCollider>(), .4f);
        Destroy(gameObject, 2);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GetComponent<AudioSource>().Play();
            HeroSkill.getInstance().Q_target = other.gameObject;
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<SphereCollider>());
            Destroy(gameObject, 2);
        }
    }
}
