using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Text userDisplay;
    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            userDisplay.text = "User: " + DBManager.username;
        }
        playButton.interactable = DBManager.LoggedIn;
    }
    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToScribe()
    {
        SceneManager.LoadScene(3);
    }
}
