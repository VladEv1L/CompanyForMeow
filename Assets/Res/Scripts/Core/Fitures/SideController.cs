using UnityEngine;

public class SideController : MonoBehaviour
{
    public bool inSide;
    void Start()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.tag == "Player")
        {
            inSide = true;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.tag == "Player")
        {
            inSide = false;
        }
    }
}
