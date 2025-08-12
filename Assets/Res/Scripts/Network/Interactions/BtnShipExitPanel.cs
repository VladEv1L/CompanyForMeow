using UnityEngine;

public class BtnShipExitPanel : MonoBehaviour, iShortInteractable
{
    public Animator BtnAnimator;
    public string AnimatorStateName;
    AudioSource AS;
    public AudioClip ClickSound;
    public MainShipDoor ShipDoors;
    public bool isOpenBtn;
    public bool isCloseBtn;

    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        BtnAnimator.Play(AnimatorStateName);
        AS.PlayOneShot(ClickSound);
        // + Добавить уже механику открытия самой двери к обращению
        if (isOpenBtn)
        {
            if (!ShipDoors.isOpened)
            {
                ShipDoors.Interact();
            }
        }
        if (isCloseBtn)
        {
            if (ShipDoors.isOpened)
            {
                ShipDoors.Interact();
            }
        }

    }
}
