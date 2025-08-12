using UnityEngine;
using System.Collections;
using TMPro;

public class GameTimeController : MonoBehaviour
{
    public static GameTimeController instance;


    private float elapsedTime = 0f;

    public int timeMultiplier = 10;

    public float updateInterval = 3f;


    private const int startHours = 8; 
    private const int startMinutes = 0; 
    private const int startSeconds = 0; 
    public GameObject TimerUI;
    public TMP_Text TimeTXT;


    private const float IN_MIN = 8f;
    private const float IN_MAX = 22f;
    private const float OUT_MIN = 0.8f;
    private const float OUT_MAX = 0.1f;
    public float firstValue;
    int hours;
    bool isInside;

    Color CurrentAmbientColor;

    bool isEvening; //For Autopilot
    bool isEveningNotification; //For Autopilot

    bool isAlarmNotification;
    public AudioClip _alarmSound;
    public GameObject AlarmUI;


    void Awake()
    {
        instance = this;

    }
    void Start()
    {
        //StartGameSessionTime();

        // RenderSettings.ambientLight = CurrentAmbientColor;
    }
    public void StartGameSessionTime()
    {
        TimerUI.SetActive(true);
       
        StartGameTime();

       
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval);

            elapsedTime += updateInterval * timeMultiplier;

            hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            string timerText = string.Format("{0:D2}:{1:D2}", hours, minutes);

            //Debug.Log(timerText);
            TimeTXT.text = timerText;
            SetAmbientColor();

            if (!isEveningNotification)
            {
                if (hours == 18)
                {
                    isEveningNotification = true;
                    CoreController.instance.SetPlayerEveningNotification();
                }
                
            }
            //Spawn Enemy
            if (!isAlarmNotification)
            {
                if (hours == 12)
                {
                    isAlarmNotification = true;
                  MonsterSpawner.instance.SpawnMonster();
                    this.GetComponent<AudioSource>().PlayOneShot(_alarmSound);
                    AlarmUI.SetActive(true);
                }
                
            }
            if (hours == 22)
                {
                    
                    CoreController.instance.StartAutopilot();
                }
        }
    }

    /// <summary>
    /// Метод для старта времени с заданных значений
    /// </summary>
    public void StartGameTime()
    {
        // Устанавливаем начальное время
        elapsedTime = startHours * 3600 + startMinutes * 60 + startSeconds;
    }
    public void SetAmbientColor()
    {
        CurrentAmbientColor = new Color(MapValue(hours), MapValue(hours), MapValue(hours));
        if (!isInside)
        {
            RenderSettings.ambientLight = CurrentAmbientColor;
        }

        //Debug.Log("Color debug: " +CurrentAmbientColor);
    }

    private float MapValue(float x)
    {
        return (x - IN_MIN) * (OUT_MAX - OUT_MIN) / (IN_MAX - IN_MIN) + OUT_MIN;
    }

    public void EnterInside()
    {
        isInside = true;
        // На базе
        RenderSettings.ambientLight = new Color(0.02f, 0.02f, 0.02f); ;

    }
    public void EnterOutside()
    {
        isInside = false;
        // Env
        SetAmbientColor();
    }
}