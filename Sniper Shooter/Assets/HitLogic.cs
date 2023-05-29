using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitLogic : MonoBehaviour
{

    public Rigidbody rb;
    public ParticleSystem ps;
    public Animator animator;
    public Collider enemyCollider;
    public NavMeshAgent navMeshAgent;
    public Transform ragdoll;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            ps.Play();
            Kill(false);
            ObjectPooler.Instance.Enqueue("bullet", collision.gameObject);
        }
    }

    public void Kill(bool withPhysics)
    {
        animator.SetTrigger("death");
        rb.isKinematic = !withPhysics;
        //rb.useGravity = withPhysics;
        enemyCollider.enabled = false;
        navMeshAgent.enabled = false;
        CollectiveEnemyLogic.Instance.EnemyDown(transform);
    }

    public void Kill(float explosiveForce, float explosionRadius)
    {
        ragdoll.parent = null;
        ragdoll.gameObject.SetActive(true);
        ragdoll.Rotate(ragdoll.rotation.eulerAngles + new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), Random.Range(-25, 25)));
        ragdoll.GetComponentInChildren<Rigidbody>().AddExplosionForce(explosiveForce * 2f, transform.position, explosionRadius, explosiveForce, ForceMode.VelocityChange);
        gameObject.SetActive(false);
        CollectiveEnemyLogic.Instance.EnemyDown(transform);
    }
}
