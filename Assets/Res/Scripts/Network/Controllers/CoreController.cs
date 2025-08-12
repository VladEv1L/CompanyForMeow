using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoreController : MonoBehaviour
{
    NetworkVariable<int> SelectedPlanet = new NetworkVariable<int>();
    public MainShipDoor MainShipDoor_;
    public AudioSource AS;
    public AudioClip FirstMissionopenDoorSound;
    public AudioClip LandingSound;
    public static CoreController instance;
    public int GrabTargetCount = 0;
    public int GrabCollected = 0;
    CinemachineImpulseSource _impulse;
    public GameObject MissionDescrition;

    public AudioClip EveningNotification;
    public GameObject EveningNoticifationUI;
    public GameObject PLAYER_MAIN;
    public GameObject Autopilot;


    public GameObject ResultPanel;
    public TMP_Text GrabCount;
    public TMP_Text Ocenka;

    public TMP_Text CollectedTMP;
    public GameObject LifeStatus;
    public SideController _sideController;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();
        SelectedPlanet.Value = 1;
        //StartMission();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ScanGrabsCountValue()
    {
        // Запасной вариант
        iItem[] scannedObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<iItem>().ToArray();

    }


    public void AddPriceCount(int price)
    {
        GrabTargetCount += price;
    }
    public void AddCollectedCount(int price)
    {
        GrabCollected += price;
    }

    public void SelectPlanet(int p)
    {

    }
    public void StartMission()
    {
        StartCoroutine(StartMissionCor());

    }
    IEnumerator StartMissionCor()
    {

        // Закрываем лобби. - на будущее

        // Загружаем Enviroment - спорно для 1го игрока, но пока оставляем 
        LoadEnv(SelectedPlanet);

        yield return new WaitForSeconds(5);
        // Даём импульс + звук посадки
        AS.PlayOneShot(LandingSound, 0.3f);
        _impulse.GenerateImpulse();

        // Генерируем базу - на будущее
        // Пока вместо генератора используем статичную базу
        //GenerateLevel(); // пока выключаем. уровень моделим в сцене планеты

        // Включаем наш монитор (пока так)

        //
        CollectedTMP.gameObject.SetActive(true);
        UpdateCollectedTMp();
        // Разблокируем дверь и Открываем дверь
        yield return new WaitForSeconds(3);
        AS.PlayOneShot(FirstMissionopenDoorSound);
        yield return new WaitForSeconds(0.7f);
        MainShipDoor_.isLocked = false;
        MainShipDoor_.Interact();
        yield return new WaitForSeconds(2f);
        MissionDescrition.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameTimeController.instance.StartGameSessionTime();
        yield return new WaitForSeconds(5f);
        MissionDescrition.SetActive(false);


        // 




    }
    public void LoadEnv(NetworkVariable<int> planetID)
    {
        switch (planetID.Value)
        {
            case 1:
                SceneManager.LoadScene("Planet_1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                break;
        }

    }
    public void GenerateLevel()
    {
        SceneManager.LoadScene("FakeLevel_1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
    public void SetPlayerEveningNotification()
    {
        StartCoroutine(SetPlayerEveningNotificationCor());
    }
    IEnumerator SetPlayerEveningNotificationCor()
    {
        AS.PlayOneShot(EveningNotification);
        EveningNoticifationUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        EveningNoticifationUI.SetActive(false);
    }

    public void StartAutopilot()
    {
        StartCoroutine(StartAutopilotCor());
    }
    IEnumerator StartAutopilotCor()
    {
        PLAYER_MAIN.SetActive(false);
        Autopilot.SetActive(true);
        yield return new WaitForSeconds(1f);

        if (MainShipDoor_.isOpened)
        {
            MainShipDoor_.Interact();
        }
        MainShipDoor_.isLocked = true;

        if (PLAYER_MAIN.GetComponent<P_STATE>().HP <= 0)
        {
            LifeStatus.SetActive(true);
            GameTimeController.instance.timeMultiplier = 0;
            GameTimeController.instance.updateInterval = 9999;
        }

        yield return new WaitForSeconds(5f);
        GetMissionResultats();
    }
    public void GetMissionResultats()
    {
        StartCoroutine(GetMissionResultatsCor());
    }
    IEnumerator GetMissionResultatsCor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PLAYER_MAIN.GetComponent<P_STATE>().HP <= 0 || _sideController.inSide == false)
        {
            GrabCollected = 0;

        }
        ResultPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        if (GrabCollected != 0)
        {
            GrabCount.text = GrabCollected + "/" + GrabTargetCount + "(" + (int)GetResultProcent(GrabTargetCount, GrabCollected) + "%)";

        }
        else
        {
            GrabCount.text = GrabCollected + "/" + GrabTargetCount + "(0%)";
        }
        yield return new WaitForSeconds(3);
        float h;
        if (GrabCollected != 0)
        {
            h = GetResultProcent(GrabTargetCount, GrabCollected);
            Ocenka.text =
                      h <= 0 ? "ERR" :
                      h < 25 ? "F" :
                      h < 50 ? "D" :
                      h < 75 ? "C" :
                      h < 90 ? "B" : "A";
        }
        else
        {
            Ocenka.text = "F";
        }





    }
    public float GetResultProcent(float a, float b)
    {
        float x = (100 / a) * b;
        //float y = Convert.ToInt32(x);
        return x;
    }
    public void UpdateCollectedTMp()
    {
        CollectedTMP.text = "[ Собрано ] \n " + GrabCollected + "/" + GrabTargetCount;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Mechanic", LoadSceneMode.Single);
    }
}
