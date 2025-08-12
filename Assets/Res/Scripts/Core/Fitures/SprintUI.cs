using UnityEngine;
using UnityEngine.UI;

public class SprintUI : MonoBehaviour
{
    public P_STATE _state;
    public Image fillImage;



    void Update()
    {
        if (_state.isSprint)
        {
            if (_state.isMoved)
            {
                Sprint();
            }

        }
        else
        {
            Relax();
        }

    }
    void Sprint()
    {
        float x = Mathf.MoveTowards(fillImage.fillAmount, 0, Time.deltaTime * 0.3f);
        fillImage.fillAmount = x;
    }
    void Relax()
    {
        float x = Mathf.MoveTowards(fillImage.fillAmount, 1, Time.deltaTime * 0.2f);
        fillImage.fillAmount = x;
    }
}
