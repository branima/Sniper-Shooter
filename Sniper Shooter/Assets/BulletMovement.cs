using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;

    public float timeBeforeDespawn;
    float spawnTime;

    void OnEnable() => spawnTime = Time.time;

    void Update()
    {
        if (Time.time - spawnTime > timeBeforeDespawn)
            ObjectPooler.Instance.Enqueue("bullet", gameObject);
    }
    void FixedUpdate() => rb.MovePosition(transform.position + transform.up * Time.fixedDeltaTime * speed * 50);
}
