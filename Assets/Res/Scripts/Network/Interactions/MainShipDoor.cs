using UnityEngine;

public class MainShipDoor : MonoBehaviour
{
    public bool isOpened;
    public bool isLocked;
    public Animator AN;
    public AudioSource AS;
    public AudioClip DoorSound;
    public AudioClip ErrorSound;
    //public AudioClip SuccessSound;
    public float DelayTime;
    public bool isDelayed;
    public float delayedTime;
    public float Delay = 2;
    public void Interact()
    {
        if (isLocked)
        {
            AS.PlayOneShot(ErrorSound);
            return;
        }

        if (isDelayed) return;
        if (isOpened)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }

    }
    private void Update()
    {
        delayedTime += Time.deltaTime;
        if (delayedTime >= Delay)
        {
            isDelayed = false;


        }
        else
        {
            isDelayed = true;
        }
    }
    public void OpenDoor()
    {
        delayedTime = 0;
        AS.PlayOneShot(DoorSound);
        //AS.PlayOneShot(SuccessSound);
        AN.Play("LEFT_OPEN");
        AN.Play("RIGHT_OPEN");
        isOpened = true;
    }
    public void CloseDoor()
    {
        delayedTime = 0;
        AS.PlayOneShot(DoorSound);
        //AS.PlayOneShot(SuccessSound);
        AN.Play("LEFT_CLOSE");
        AN.Play("RIGHT_CLOSE");
        isOpened = false;
    }
}
