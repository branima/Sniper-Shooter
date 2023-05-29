using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveEnemyLogic : MonoBehaviour
{
    [SerializeField]
    int aliveEnemies;

    public BulletCameraComplexRotate bulletCamera;

    public GameObject gameplayUI;

    public static CollectiveEnemyLogic Instance;
    void Awake() => Instance = this;

    void Start() => aliveEnemies = transform.childCount;

    public void EnemyDown(Transform hitEnemy)
    {
        aliveEnemies--;
        if (aliveEnemies == 1)
            GameLoop.Instance.EnableGoldenBullet();
        else if (aliveEnemies == 0)
        {
            if (!bulletCamera.gameObject.activeSelf)
                Invoke("LevelComplete", 2f);
            else
                Invoke("LevelComplete", 1f);
            bulletCamera.transform.parent = hitEnemy;
            CameraZoomOut.Instance.ChangeCamera();
            gameplayUI.SetActive(false);
        }
    }

    void LevelComplete() => GameManager.Instance.LevelComplete();
}
