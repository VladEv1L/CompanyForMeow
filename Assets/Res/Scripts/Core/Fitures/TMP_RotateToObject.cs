using UnityEngine;

public class TMP_RotateToObject : MonoBehaviour
{
 public Transform RotateTo;

    // Update is called once per frame
    void Update()
    {
         transform.LookAt(RotateTo);
    }
}
