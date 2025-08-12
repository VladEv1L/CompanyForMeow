
using UnityEngine;

public class P_INPUT : MonoBehaviour
{

    INPUT_CONTROLLER _inputController;
    void Start()
    {
        //if (!IsOwner) this.enabled = false;
        P_STATE p = GetComponent<P_STATE>();
        _inputController = new INPUT_CONTROLLER();
        _inputController.PLAYER.MOVE.performed += ctx => p.MoveInput = ctx.ReadValue<Vector2>();
        _inputController.PLAYER.LOOK.performed += ctx => p.LookInput = ctx.ReadValue<Vector2>();
        _inputController.Enable();
    }
}
