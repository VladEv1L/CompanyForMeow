using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject RezhimsPanel;
    void Start()
    {

    }

    public void OpenRezhims()
    {
        RezhimsPanel.SetActive(true);
    }
    public void CloseRezhims()
    {
        RezhimsPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Mechanic", LoadSceneMode.Single);
    }
}
