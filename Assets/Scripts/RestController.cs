using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestController : MonoBehaviour
{
    //string webUrl = "localhost:3000";
    string webUrl = "https://server-fred.herokuapp.com";
    string routeLogin = "/login";
    string routeRegister = "/register";

    #region ##### FUNCTIONS #####
    public void SendRestGetLogin(string pEmail, string pPassword, System.Action<Message>callBack)
    {
        Login login = new Login(pEmail, pPassword);

        StartCoroutine(LoginGet(webUrl, routeLogin, login, callBack));
    }

    public void SendRestPostRegister(string pEmail, string pPassword, System.Action<Message> callBack)
    {
        Login login = new Login(pEmail, pPassword);

        StartCoroutine(RegisterPost(webUrl, routeRegister, login, callBack));
    }
    #endregion

    #region ###### LOGIN #####
    public IEnumerator LoginGet(string url, string route, Login loginPlayer, System.Action<Message>callBack)
    {
        string urlNew = string.Format("{0}{1}/{2}/{3}", url, route, loginPlayer.Email, loginPlayer.Password);
        Debug.Log(urlNew);

        using (UnityWebRequest www = UnityWebRequest.Get(urlNew))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Message msgErr = new Message((int)www.responseCode, www.error);
                Debug.Log(www.error);
                callBack(msgErr);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                    Message msgRes = JsonUtility.FromJson<Message>(jsonResult);
                    callBack(msgRes);
                }
            }
        }
    }
    #endregion

    #region ###### REGISTER #####
    public IEnumerator RegisterPost(string url, string route, Login loginPlayer, System.Action<Message> callBack)
    {
        string urlNew = string.Format("{0}{1}", url, route);
        Debug.Log(urlNew);

        string jsonData = JsonUtility.ToJson(loginPlayer);

        using (UnityWebRequest www = UnityWebRequest.Post(urlNew, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Message msgErr = new Message((int)www.responseCode, www.error);
                Debug.Log(www.error);
                callBack(msgErr);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                    Message msgRes = JsonUtility.FromJson<Message>(jsonResult);
                    callBack(msgRes);
                }
            }
        }
    }
    #endregion

}
