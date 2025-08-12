using UnityEngine;
using UnityEngine.SceneManagement;

public class P_MENU : MonoBehaviour
{
    public bool isOpened;
    public GameObject UI_Menu;
    P_MOVE _move;
    P_LOOK _look;
    P_CURSOR _cursor;
    P_STATE _state;
    void Start()
    {
        _state = GetComponent<P_STATE>();
        _move = GetComponent<P_MOVE>();
        _look = GetComponent<P_LOOK>();
        _cursor = GetComponent<P_CURSOR>();



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpened)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        UI_Menu.SetActive(true);
        _state.isMoved = false;
        _move.enabled = false;
        _look.enabled = false;
        _cursor.GetCursorState(false);
        isOpened = true;

    }
    public void CloseMenu()
    {
        UI_Menu.SetActive(false);
        _move.enabled = true;
        _look.enabled = true;
        _cursor.GetCursorState(true);
        isOpened = false;
    }

    public void EnterMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
