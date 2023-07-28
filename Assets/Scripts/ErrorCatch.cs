using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorCatch : MonoBehaviour
{
    string filePath = "";
    public GameObject errorPanel;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        filePath = Path.Combine(Application.persistentDataPath, "errorLog.txt");
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
