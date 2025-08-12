
using UnityEngine;

public class P_MOVE : MonoBehaviour
{
    CharacterController CC;
    P_STATE p;
   
    void Start()
    {
        //if (!IsOwner) this.enabled = false;
        CC = GetComponent<CharacterController>();
        p = GetComponent<P_STATE>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            p.isSprint = true;
        }
        else
        {
           p.isSprint = false;
        }*/

        if (p.isSprint)
        {
            CC.SimpleMove(GetMoveDirection() * p.moveSprintSpeed);
        }
        else
        {
            CC.SimpleMove(GetMoveDirection() * p.moveSpeed);
        }
    }

    Vector3 GetMoveDirection()
    {
        if (p.moveDirection != new Vector3(0, 0, 0))
        {
            p.isMoved = true;
        }
        else
        {
            p.isMoved = false;
        }
        return p.moveDirection = transform.forward * p.MoveInput.y + transform.right * p.MoveInput.x;

    }
}
