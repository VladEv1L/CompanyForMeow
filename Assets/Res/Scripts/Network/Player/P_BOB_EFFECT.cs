using UnityEngine;
using Unity.Cinemachine;

public class P_BOB_EFFECT : MonoBehaviour
{

    P_STATE state;
   public CinemachineBasicMultiChannelPerlin CBMCP;
    //public CinemachineCamera cam;


    void Start()
    {
        state = GetComponent<P_STATE>();
        //CBMCP = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       // CBMCP = cam.GetCinemachineComponent;

    }

    void Update()
    {
        if (state.isMoved)
        {

            CBMCP.AmplitudeGain = Mathf.Lerp(CBMCP.AmplitudeGain, 1f, Time.deltaTime * 10);
        }
        else
        {
            CBMCP.AmplitudeGain = Mathf.Lerp(CBMCP.AmplitudeGain, 0f, Time.deltaTime * 10);
        }


    }
}
