using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotation : MonoBehaviour
{

    public float speed;
    void FixedUpdate() => transform.Rotate(0f, 90f * speed * Time.fixedDeltaTime, 0f);
}
