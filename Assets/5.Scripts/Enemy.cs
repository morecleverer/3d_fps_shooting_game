using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public BoxCollider hitBox;
    public CapsuleCollider capsule;
    public int health = 60;
    public int type;
    public int speed;
    bool isDie = false;
    public bool isAttack = false;
    public float dist;
    public TrailRenderer trailRenderer;
    MemoryPool memoryPool;
    Player player;

    public Transform target;

    public void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        player = target.GetComponent<Player>();
    }

    public void Setup(MemoryPool pool)
    {
        health = type == 0 ? 60 : type == 1 ? 80 : 160;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        memoryPool = pool;
        speed = type == 0 ? 4 : 3;
        agent.enabled = true;
        capsule.enabled = true;
        isDie = false;
        agent.speed = speed;
        player = target.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health>0 && !isAttack )
            agent.SetDestination(target.position);
        
            if (Vector3.Distance(target.position, transform.position) <= dist && health > 0)
            {
                if (!isAttack)
                {
                    print(2);
                    isAttack = true;
                    StartCoroutine(Attack());
                }

            }
        
        if (health <= 0 && !isDie)
        {
            isDie = true;
            StartCoroutine(Die());
            
        }
        agent.speed = speed;

    }
    IEnumerator Die()
    {
        anim.SetTrigger("doDie");
        anim.SetInteger("rand", Random.Range(0, 2));
        agent.enabled = false;
        capsule.enabled = false;
        yield return new WaitForSeconds(2);
        player.catchEnemy++;
        if(type!=3)
        memoryPool.DeactivatePoolItem(this.gameObject);
    }

    IEnumerator Attack()
    {
        agent.speed = 0;
        anim.SetBool("isAttack",true);
        yield return new WaitForSeconds(0.1f);
        anim.applyRootMotion = true;
        hitBox.enabled = true;
        if(type!=0)
            trailRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }
        hitBox.enabled = false;
        anim.SetBool("isAttack", false);
        anim.applyRootMotion = true;
        if(type!=0)
            trailRenderer.enabled = false;

        agent.speed = speed;
        isAttack = false;

    }
}
