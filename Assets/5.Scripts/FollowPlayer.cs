using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public BoxCollider hitBox;
    public CapsuleCollider capsule;
    public int health = 60;

    public Transform target;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health>0)
            agent.SetDestination(target.position);

        if (Vector3.Distance(target.position, transform.position) <= 2f && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && health>0)
        {
           StartCoroutine( Attack());
            
        }
        if (health <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            StartCoroutine(Die());
            
        }
   

    }
    IEnumerator Die()
    {
        anim.SetTrigger("doDie");
        agent.enabled = false;
        capsule.enabled = false;
        yield return new WaitForSeconds(2);

        Destroy(this.gameObject);
    }

    IEnumerator Attack()
    {
        agent.speed = 0;
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.4f);
        hitBox.enabled = true;
        yield return new WaitForSeconds(1.5f);
        hitBox.enabled = false;
        agent.speed = 4f;

    }
}
