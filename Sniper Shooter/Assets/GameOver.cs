using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void OnTriggerEnter(Collider other) => GameManager.Instance.LevelFailed();
}
