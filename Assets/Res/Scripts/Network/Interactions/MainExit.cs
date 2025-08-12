using UnityEngine;

public class MainExit : MonoBehaviour, iLongInteractable
{
    MainEnter mainEnter;
    P_MY_PLAYER player;
    void Start()
    {
        mainEnter = FindFirstObjectByType<MainEnter>();
        player = GameObject.FindFirstObjectByType<P_MY_PLAYER>();
    }
    public void Interact()
    {
        player.GetComponent<P_MOVE>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = mainEnter.TeleportBack.position;
        Debug.Log("Test Player Teleport");
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<P_MOVE>().enabled = true;
        GameTimeController.instance.EnterOutside();

    }
}
