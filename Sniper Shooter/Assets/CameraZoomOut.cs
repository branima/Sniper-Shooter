using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{

    public Transform cam;

    int currActive;
    Transform currActiveTransform;

    bool reposition;
    float repoTime;

    public float speed;

    public static CameraZoomOut Instance;
    private void Awake()
    {
        Instance = this;
        currActive = 0;
        currActiveTransform = transform.GetChild(currActive);
        reposition = false;
        repoTime = 0f;
    }

    void Update()
    {
        if (reposition)
        {
            repoTime += Time.deltaTime * 0.1f * speed;
            if (cam.localPosition != currActiveTransform.localPosition)
                cam.localPosition = Vector3.Lerp(cam.localPosition, currActiveTransform.localPosition, repoTime);

            if (cam.localRotation != currActiveTransform.localRotation)
                cam.localRotation = Quaternion.Lerp(cam.localRotation, currActiveTransform.localRotation, repoTime);

            if (cam.localPosition == currActiveTransform.localPosition && (cam.localRotation == currActiveTransform.localRotation || Vector3.Distance(cam.localRotation.eulerAngles, currActiveTransform.localRotation.eulerAngles) < 0.001f))
                reposition = false;
        }
    }

    public void ChangeCamera()
    {
        currActive++;
        if (currActive == transform.childCount)
            currActive = 0;
        //UnityEngine.Debug.Log(currActive);
        currActiveTransform = transform.GetChild(currActive);
        reposition = true;
        repoTime = 0f;
    }
}
