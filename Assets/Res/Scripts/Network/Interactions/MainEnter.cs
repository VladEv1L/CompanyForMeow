using System;
using UnityEngine;

public class MainEnter : MonoBehaviour, iLongInteractable
{
    public Transform TeleportBack;
    public Vector3 TeleportTo;
    public Vector3 TeleportRotation;
    P_MY_PLAYER player;
    public AudioSource EnteredAudioDoorSource;
    public AudioClip EnteredAudioClip;
    void Start()
    {
        player = GameObject.FindFirstObjectByType<P_MY_PLAYER>();
    }
    public void Interact()
    {
        player.GetComponent<P_MOVE>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = TeleportTo;
        player.transform.eulerAngles = TeleportRotation;
        Debug.Log("Test Player Teleport");
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<P_MOVE>().enabled = true;
        EnteredAudioDoorSource.PlayOneShot(EnteredAudioClip);

GameTimeController.instance.EnterInside();

    }
}
