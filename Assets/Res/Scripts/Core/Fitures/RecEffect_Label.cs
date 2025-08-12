using UnityEngine;
using UnityEngine.UI;

public class RecEffect_Label : MonoBehaviour
{
    public Image RecImage;
    void Start()
    {
        InvokeRepeating("BlinkRec", 0f, 1f);
    }

    void BlinkRec()
    {
        RecImage.enabled = !RecImage.enabled;
    }
}
