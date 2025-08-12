using UnityEngine;

public class P_FLASHLIGHT_BASE_VER : MonoBehaviour
{
    public bool IsEnabled;
    public GameObject Light;
    public PlayerAudioController PAC;
    public AudioClip FlashLightSound;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsEnabled)
            {
                //Выключаем
                Light.SetActive(false);
                PAC.PlayOne(FlashLightSound);
                IsEnabled = false;

            }
            else
            {
                Light.SetActive(true);
                PAC.PlayOne(FlashLightSound);
                IsEnabled = true;
                //Включаем
            }
        }

    }
}
