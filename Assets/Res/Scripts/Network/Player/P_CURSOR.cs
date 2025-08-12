using UnityEngine;

public class P_CURSOR : MonoBehaviour
{
   public bool Locked;
    void Start()
    {
        
        GetCursorState(Locked);
    }
    public void GetCursorState(bool state)
    {
        if (state)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
