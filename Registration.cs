using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }    
    public void CallRegister() {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://lit-citadel-61404.herokuapp.com/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("User created successfully.");
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("User creation failed. Error #" + www.downloadHandler.text);
            }
        }
        // WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        // yield return www;
        // if (www.text == "0")
        // {
        //     Debug.Log("User created successfully.");
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        // }
        // else
        // {
        //     Debug.Log("User creation failed. Error #" + www.text);
        // }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
