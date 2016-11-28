using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuscript : MonoBehaviour
{

    public Canvas exitMenu;
    public Button Play;
    public Button Exit;

    // Use this for initialization
    void Start()
    {
        exitMenu = exitMenu.GetComponent<Canvas>();
        Play = Play.GetComponent<Button>();
        Exit = Exit.GetComponent<Button>();
        exitMenu.enabled = false;
    }

    public void ExitPress()
    {
        exitMenu.enabled = true;
        Play.enabled = false;
        Exit.enabled = false;
    }

    public void NoPress()
    {
        exitMenu.enabled = false;
        Play.enabled = true;
        Exit.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Menu_Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}