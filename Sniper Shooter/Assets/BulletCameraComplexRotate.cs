using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCameraComplexRotate : MonoBehaviour
{

    public float speed;
    void FixedUpdate() => transform.Rotate(0f, 0f, Time.fixedDeltaTime * 90f * speed);
}
