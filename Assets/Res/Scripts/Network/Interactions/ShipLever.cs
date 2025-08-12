using System.Collections;
using UnityEngine;

public class ShipLever : MonoBehaviour, iLongInteractable
{

    public Animator _leverAnimator;
    public AudioClip _startSound;
    AudioSource AS;
    public GameObject WaitingUI;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        StartCoroutine(ShipFlyCor());
        this.transform.gameObject.layer = 0;


    }
    IEnumerator ShipFlyCor()
    {

        _leverAnimator.Play("START");
        AS.PlayOneShot(_startSound);
        yield return new WaitForSeconds(1f);
        WaitingUI.SetActive(true);
        CoreController.instance.StartMission();
        yield return new WaitForSeconds(1f);
        WaitingUI.SetActive(false);
    }

}
