using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Scribe : MonoBehaviour
{
    public Text userDisplay;
    public InputField messageField;
    private void Awake()
    {
        // if (DBManager.username == null)
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        // }
        //userDisplay.text = "User: " + DBManager.username;
        CallLoad();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);    
        DBManager.username = null;
        DBManager.score = 0;
        DBManager.message = null;
        DBManager.id = 0;
        DBManager.LogOut();
    }    
    public void CallSave() {
        StartCoroutine(SaveMessage());
    }
    public void CallLoad() {
        StartCoroutine(LoadMessage());
    }
    IEnumerator SaveMessage()
    {
        WWWForm form = new WWWForm();
        form.AddField("message", messageField.text);
        form.AddField("id", DBManager.id);

        using (UnityWebRequest www = UnityWebRequest.Post("https://lit-citadel-61404.herokuapp.com/savemessage.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("Message saved successfully.");
                userDisplay.text = "Message saved";
                StartCoroutine(TextFade(userDisplay));
            }
            else
            {
                Debug.Log("Message save failed. Error #" + www.downloadHandler.text);
            }
        }
    }
    IEnumerator LoadMessage()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://lit-citadel-61404.herokuapp.com/loadmessage.php" + "?id=" + DBManager.id))
        {
            Debug.Log("https://lit-citadel-61404.herokuapp.com/loadmessage.php" + "?id=" + DBManager.id);
            yield return www.SendWebRequest();

            if (www.downloadHandler.text[0] == '0')
            {
                Debug.Log("Message loaded successfully.");
                messageField.text = www.downloadHandler.text.Split('\t')[1];
            }
            else if (www.downloadHandler.text == "N/A")
            {
                Debug.Log("No message");
            }
            else
            {
                Debug.Log("Message load failed. Error #" + www.downloadHandler.text);
            }
        }
    }
    
    IEnumerator TextFade(Text fadeText)
    {
       
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        fadeText.text = "";
    }

    //pw: takoyakifbk
}
