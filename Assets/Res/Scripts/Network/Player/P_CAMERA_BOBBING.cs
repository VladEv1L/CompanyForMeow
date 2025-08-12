using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CAMERA_BOBBING : MonoBehaviour
{
    public Transform headTransform;
    public Transform cameraTransform;

    public float bobFrequency = 5f;
    public float bobHorizontalAmplitude = 0.1f;
    public float bobVerticalAmplitude = 0.1f;
    [Range(0, 1)] public float headBobSmooth = 0.1f;

    P_STATE PC;
    float walkTime;
    Vector3 targetCamPos;
    void Start()
    {
        PC = GameObject.FindAnyObjectByType<P_STATE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PC.isMoved) walkTime = 0;
        else
        {
            walkTime += Time.deltaTime;
        }

        targetCamPos = headTransform.position + CalculateHeadBobOffset(walkTime);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCamPos, headBobSmooth);
        if ((cameraTransform.position - targetCamPos).magnitude <= 0.001)
        {
            cameraTransform.position = targetCamPos;
        }

    }

    public Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;
        if (t > 0)
        {
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmplitude;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;
            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }
        return offset;
    }
}
