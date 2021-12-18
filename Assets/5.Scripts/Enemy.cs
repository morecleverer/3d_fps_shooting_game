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
    public TrailRenderer trailRenderer;
    MemoryPool memoryPool;
    public Player player;

    public Transform target;

    public void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    public void Setup(MemoryPool pool)
    {
        health = type == 1 ? 80 : 60;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        memoryPool = pool;
        speed = type == 0 ? 3 : 2;
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

        if (Vector3.Distance(target.position, transform.position) <= 2f && !isAttack && health>0)
        {
            isAttack = true;
           StartCoroutine( Attack());
            
        }
        if (health <= 0 && !isDie)
        {
            isDie = true;
            StartCoroutine(Die());
            
        }
   

    }
    IEnumerator Die()
    {
        anim.SetTrigger("doDie");
        anim.SetInteger("rand", Random.Range(0, 2));
        agent.enabled = false;
        capsule.enabled = false;
        yield return new WaitForSeconds(2);
        player.catchEnemy++;
        memoryPool.DeactivatePoolItem(this.gameObject);
    }

    IEnumerator Attack()
    {
        agent.speed = 0;
        anim.SetBool("isAttack",true);
        anim.applyRootMotion = true;
        yield return new WaitForSeconds(0.3f);
        hitBox.enabled = true;
        if(type==1)
            trailRenderer.enabled = true;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
            
        }
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
        hitBox.enabled = false;
        anim.SetBool("isAttack", false);
        anim.applyRootMotion = true;
        if(type==1)
            trailRenderer.enabled = false;

        agent.speed = speed;
        isAttack = false;

    }
}
