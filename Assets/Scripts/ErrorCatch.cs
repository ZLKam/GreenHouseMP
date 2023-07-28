using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorCatch : MonoBehaviour
{
    string filePath = "";
    public GameObject errorPanel;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_STANDALONE
        filePath = Path.Combine(Application.persistentDataPath, "errorLog.txt");
#endif
#if UNITY_ANDROID
        try
        {
            using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    filePath = ajo.Call<AndroidJavaObject>("getExternalFilesDir").Call<string>("getAbsolutePath");
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Error fetching native Android external storage dir: " + e.Message);
        }
#endif
        Application.logMessageReceivedThreaded += HandleLog;
        SceneManager.LoadScene("Main Menu");
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
#if !UNITY_EDITOR
        if (type == LogType.Error || type == LogType.Exception)
        {
            string logMessage = $"{DateTime.Now.ToString()} {type}: {condition}\nStack Trace: {stackTrace}\n";
            File.AppendAllText(filePath, logMessage);
            UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "{0}", condition + stackTrace);
            StartCoroutine(ShowError());
        }
#endif
    }

    private void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    private IEnumerator ShowError()
    {
        GameObject error = Instantiate(errorPanel, GameObject.Find("Canvas").transform);
        yield return new WaitForSecondsRealtime(2f);
        Destroy(error);
    }
}
