using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _developersWindow;
    [SerializeField] private int _gameSceneId = 1;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneId);
    }

    public void OpenDevelopersWindow()
    {
        _developersWindow.SetActive(true);
    }

    public void CloseDevelopersWindow()
    {
        _developersWindow.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
