using UnityEngine;

public class P_STATS : MonoBehaviour
{
    P_STATE p;
    public Animator DamageAnimator;

    void Start()
    {
        p = GetComponent<P_STATE>();
    }

    public void TakeHP(int hp)
    {
        DamageAnimator.Play("Damage");
        p.HP -= hp;
        if (p.HP < 0)
        {
            PlayerDeath();
        }
    }
    public void PlayerDeath()
    {
        CoreController.instance.StartAutopilot();
    }
}
