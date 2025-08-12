
using UnityEngine;

public class P_ANIMATOR : MonoBehaviour
{
    Animator PL_Animator;
    P_STATE p;
    float currentX;
    float currentY;
    void Start()
    {

        p = GetComponent<P_STATE>();
        PL_Animator = p.ModelAnimator;
    }


    void Update()
    {
        
        currentX = PL_Animator.GetFloat("MoveX");
        currentY = PL_Animator.GetFloat("MoveY");

        PL_Animator.SetBool("isMove", p.isMoved);
        PL_Animator.SetFloat("MoveX", Mathf.Lerp(currentX, p.MoveInput.x, Time.deltaTime * 10));
        PL_Animator.SetFloat("MoveY", Mathf.Lerp(currentY, p.MoveInput.y, Time.deltaTime * 10));
    }
}
