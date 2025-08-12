using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int DestroyTime = 5;
    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
    }


}
