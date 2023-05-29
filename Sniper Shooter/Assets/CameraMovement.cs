using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{

    public float mouseSensitivity;
    float currMouseSensitivity;

    float rawCamRotation, rawPlayerRotation, hor, ver, ogCamYRotation;

    public static CameraMovement Instance;
    void Awake() => Instance = this;

    void Start()
    {
        rawCamRotation = 0f;
        //rawPlayerRotation = 90f;
        rawPlayerRotation = ogCamYRotation = transform.rotation.eulerAngles.y;
        hor = 0f;
        ver = 0f;
        currMouseSensitivity = mouseSensitivity;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            hor = Input.GetAxis("Mouse X");
            ver = Input.GetAxis("Mouse Y");

            rawCamRotation -= ver * Time.deltaTime * currMouseSensitivity * 10f;
            rawPlayerRotation += hor * Time.deltaTime * currMouseSensitivity * 10f;

            transform.localRotation = Quaternion.Euler(Mathf.Clamp(rawCamRotation, -90f, 90f), 0f, 0f);
            transform.root.rotation = Quaternion.Euler(0f, Mathf.Clamp(rawPlayerRotation, ogCamYRotation - 25f, ogCamYRotation + 25f), 0f);
            //transform.root.rotation = Quaternion.Euler(0f, Mathf.Clamp(rawPlayerRotation, ogCamYRotation - 70f, ogCamYRotation + 70f), 0f);
            //transform.root.Rotate(Vector3.up * hor * Time.deltaTime * currMouseSensitivity * 10f);
        }
        else if (Input.GetMouseButtonUp(0) && GameLoop.Instance.IsZoomed() && (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.name.Contains("Zoom")))
            GameLoop.Instance.Shoot();
    }

    public void NormalMouseSensitivity() => currMouseSensitivity = mouseSensitivity;
    public void ZoomedMouseSensitivity() => currMouseSensitivity = mouseSensitivity * 0.25f;
}
