using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip[] DefaultSoundSteps;
    public AudioClip[] MetallSoundSteps;
    public AudioClip[] ConcreteSoundSteps;
    public AudioClip[] CarpetSoundSteps;
    public AudioClip DeadSound;
    P_STATE state;


    [Header("Step Sounds")]
    public bool StepSound;
    float StepWalkTime;
    public float StepRate;

    int n;
    int previousN;


    void Start()
    {
        state = GetComponent<P_STATE>();
    }
    void Update()
    {
        if (state.isMoved)
        {
            StepSoundControl();
        }
        if (state.isSprint) {
            StepRate = 0.3f;
        }
        else {
            StepRate = 0.4f;
        }

    }

    public void StepSoundControl()
    {
        StepWalkTime += Time.deltaTime;
        if (StepWalkTime >= StepRate)
        {
            StepWalkTime = 0;
            PlayStepSound();
        }

    }
    public void PlayStepSound()
    {
        Debug.Log("Звук шагов");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            if (hit.transform.GetComponent<Collider>().sharedMaterial != null)
            {

                string PhysicsMaterialName = hit.transform.gameObject.GetComponent<Collider>().sharedMaterial.name;
                switch (PhysicsMaterialName)
                {
                    case "Metall":

                        this.PlayOneStepServerRpc("Metall", 0.3f);
                        break;

                    case "Concrete":

                        this.PlayOneStepServerRpc("Concrete", 0.3f);
                        break;
                        case "Carpet":

                        this.PlayOneStepServerRpc("Carpet", 0.3f);
                        break;

                    default:
                        this.PlayOneStepServerRpc("Default", 0.3f);
                        break;
                }
            }
            else
            {
                this.PlayOneStepServerRpc("Default", 0.3f);


            }

        }

    }

    public void PlayOne(AudioClip c)
    {
        AS.PlayOneShot(c);
    }

    public void PlayOneStepServerRpc(string Name, float vol)
    {

        switch (Name)
        {
            case "Metall":
                n = UnityEngine.Random.Range(0, MetallSoundSteps.Length);
                while (previousN == n)
                {
                    n = Random.Range(0, MetallSoundSteps.Length);

                }
                AS.PlayOneShot(MetallSoundSteps[n], vol);
                PlayOneStepClientRpc(Name, n, vol);

                break;
            case "Concrete":
                n = UnityEngine.Random.Range(0, ConcreteSoundSteps.Length);
                AS.PlayOneShot(ConcreteSoundSteps[n], vol);
                PlayOneStepClientRpc(Name, n, vol);

                break;
                case "Carpet":
                n = UnityEngine.Random.Range(0, CarpetSoundSteps.Length);
                AS.PlayOneShot(CarpetSoundSteps[n], vol);
                PlayOneStepClientRpc(Name, n, vol);

                break;
            default:
                n = UnityEngine.Random.Range(0, DefaultSoundSteps.Length);
                AS.PlayOneShot(DefaultSoundSteps[n], vol);
                PlayOneStepClientRpc(Name, n, vol);

                break;

        }
        previousN = n;


    }
    public void PlayOneStepClientRpc(string Name, int n, float vol)
    {

        switch (Name)
        {
            case "Metall":
                AS.PlayOneShot(MetallSoundSteps[n], vol);
                break;
            case "Concrete":
                AS.PlayOneShot(ConcreteSoundSteps[n], vol);
                break;
                case "Carpet":
                AS.PlayOneShot(CarpetSoundSteps[n], vol);
                break;
            default:
                AS.PlayOneShot(DefaultSoundSteps[n], vol);
                break;

        }
    }

    public void PlayDeadSoundServerRpc()
    {
        PlayDeadSoundClientRpc();

    }

    void PlayDeadSoundClientRpc()
    {
        PlayOne(DeadSound);
    }





}
