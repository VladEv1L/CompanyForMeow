
using UnityEngine;
using System;

public class P_FLASHLIGHT : MonoBehaviour
{


    P_STATE p;
    float lx;
    float ly;
    public float MaxAngle = 15;
    public float Multipl = 1;
    public float offsetX;
    public float offsetY;
    void Start()
    {

        p = GameObject.FindFirstObjectByType<P_STATE>();

    }

    // Update is called once per frame
    void Update()
    {
        lx += p.LookInput.x * 0.1f;
        ly += p.LookInput.y * 0.1f;
        lx = Math.Min(Math.Max(lx, -MaxAngle), MaxAngle);
        ly = Math.Min(Math.Max(ly, -MaxAngle), MaxAngle);
        this.transform.localEulerAngles = new Vector3(-ly + (offsetX), lx + (offsetY), 0f);
    }
}
