using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    public Animation gunAnimation;
    bool zoomedIn;

    public GameObject crosshair;

    public Transform bullet;
    public Transform bulletSpawnPoint;

    bool goldenBullet;
    public GameObject bulletCameraComplex;

    RaycastHit hit;

    public static GameLoop Instance;
    void Awake() => Instance = this;

    public void GunZoom()
    {
        if (!zoomedIn)
        {
            gunAnimation.Play("zoomIn");
            CameraMovement.Instance.ZoomedMouseSensitivity();
        }
        else
        {
            gunAnimation.Play("zoomOut");
            CameraMovement.Instance.NormalMouseSensitivity();
        }

        crosshair.SetActive(!crosshair.activeSelf);
        zoomedIn = !zoomedIn;
    }

    public void Shoot()
    {
        AudioManager.Instance.Play("sniper");
        if (goldenBullet)
        {
            if (!(Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.up, out hit) && hit.transform.tag.Contains("zombie")))
                return;

            BulletMovement bm = ObjectPooler.Instance.SpawnFromPool("bullet", bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<BulletMovement>();
            //BULLET MISS //BulletMovement bm = ObjectPooler.Instance.SpawnFromPool("bullet", bulletSpawnPoint.position + bulletSpawnPoint.right * 1f, bulletSpawnPoint.rotation).GetComponent<BulletMovement>();
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = Time.timeScale * 0.005f;
            bulletCameraComplex.transform.parent = bm.transform;
            bulletCameraComplex.transform.localPosition = Vector3.zero;
            bulletCameraComplex.SetActive(true);
        }
        else
            ObjectPooler.Instance.SpawnFromPool("bullet", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void EnableGoldenBullet() => goldenBullet = true;

    public bool IsGoldenBullet() => goldenBullet;
    public bool IsZoomed() => zoomedIn;
}
