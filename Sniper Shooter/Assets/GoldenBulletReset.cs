using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBulletReset : MonoBehaviour
{

    public GameObject bulletCameraComplex;


    void OnTriggerEnter(Collider other)
    {
        if (!GameLoop.Instance.IsGoldenBullet())
            return;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        bulletCameraComplex.SetActive(false);
        ObjectPooler.Instance.Enqueue("bullet", other.gameObject);
    }
}
