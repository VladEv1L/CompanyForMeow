using UnityEngine;
using System;


public class BodyCamEffect : MonoBehaviour
{
     P_STATE state;
    float lx;
    float ly;
    public float MaxAngle = 15;
    public float Multipl = 1;
    public float offsetX;
    public float offsetY;
    void Start()
    {
        state = GameObject.FindFirstObjectByType<P_STATE>();
    }
    void Update()
    {
        lx += Input.GetAxis("Mouse X") * Multipl;
        ly += Input.GetAxis("Mouse Y") * Multipl;
        lx = Math.Min(Math.Max(lx, -MaxAngle), MaxAngle);
        ly = Math.Min(Math.Max(ly, -MaxAngle), MaxAngle);
        this.transform.localEulerAngles = new Vector3(-ly + (offsetX), lx + (offsetY), 0f);

    }
}
