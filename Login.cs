using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;
    public string[] response;
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }    
    public void CallLogin() {
        StartCoroutine(LoginUser());
    }
    
    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://lit-citadel-61404.herokuapp.com/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text[0] == '0')
            {
                Debug.Log("User logged in successfully.");
                DBManager.username = nameField.text;
                response = www.downloadHandler.text.Split('\t');
                DBManager.score = int.Parse(response[1]);
                DBManager.id = int.Parse(response[2]);
                SceneManager.LoadScene(3);                       
            }
            else
            {
                Debug.Log("User login failed. Error #" + www.downloadHandler.text);
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
