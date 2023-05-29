using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplosion : MonoBehaviour
{

    public Rigidbody rb;
    public ParticleSystem ps;

    public float explosionRadius;
    public float explosiveForce;

    public bool fallToExplode;

    bool exploded;
    bool bulleted;

    void OnCollisionEnter(Collision collision)
    {
        if (exploded)
            return;
        if (collision.gameObject.tag == "bullet")
        {
            if (fallToExplode)
            {
                rb.useGravity = true;
                bulleted = true;
                return;
            }
            Explode(collision);
        }
        else if (fallToExplode && bulleted)
            Explode(collision);
    }

    void Explode(Collision collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        rb.AddForce(collision.transform.up * 10f, ForceMode.VelocityChange);
        ps.transform.parent = null;
        ps.Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Rigidbody enemyRb;
        HitLogic hl;
        foreach (Collider item in hitColliders)
        {
            enemyRb = item.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                if (item.name.Contains("Explosive"))
                    enemyRb.AddExplosionForce(explosiveForce * 0.1f, transform.position, explosionRadius, explosiveForce * 0.09f, ForceMode.VelocityChange);
                else
                    enemyRb.AddExplosionForce(explosiveForce * 2f, transform.position, explosionRadius, explosiveForce, ForceMode.VelocityChange);
                hl = item.GetComponent<HitLogic>();
                if (hl != null && item.transform != transform)
                    hl.Kill(explosiveForce, explosionRadius);
            }
        }

        exploded = true;
    }
}
