using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class SerializableList<T>
{
    public List<T> list;
}

public class LeaderboardController : MonoBehaviour
{
    public Transform playerEntryContainer;
    public GameObject playerEntryPrefab;

    private HashSet<PlayerData> playerDataSet = new HashSet<PlayerData>();
    [SerializeField]
    private List<PlayerData> playerDataList = new();
    [SerializeField]
    private SerializableList<PlayerData> serializablePlayerDataList = new();
    private const string PlayerDataFileName = "playerdata.json";

    private Coroutine scrollToTopCoroutine;
    private Coroutine scrollToBottomCoroutine;
    private ScrollRect scrollRect;

    // Load saved player data from JSON file
    private void LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            //List<PlayerData> data = JsonUtility.FromJson<List<PlayerData>>(json);
            //playerDataList = new(data);
            serializablePlayerDataList = JsonUtility.FromJson<SerializableList<PlayerData>>(json);
            playerDataList = new(serializablePlayerDataList.list);
            playerDataSet = new HashSet<PlayerData>(playerDataList);
        }
    }

    // Save current player data to JSON file
    private void SavePlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
        playerDataList = new(playerDataSet);
        serializablePlayerDataList.list = new(playerDataSet);
        string json = JsonUtility.ToJson(serializablePlayerDataList);
        File.WriteAllText(filePath, json);
    }

    // Method to add a player's data to the leaderboard
    public void AddPlayerDataToLeaderboard(string playerName, int[] scores)
    {
        PlayerData playerData = new PlayerData
        {
            playerName = playerName,
            scores = scores
        };
        playerDataSet.Add(playerData);
        SavePlayerData();
    }

    // Instantiate and populate player entry prefabs in the scroll view
    private void PopulateLeaderboard()
    {
        ClearLeaderboard();

        int positionIndex = 1;

        foreach (PlayerData playerData in playerDataSet)
        {
            GameObject playerEntry = Instantiate(playerEntryPrefab, playerEntryContainer);
            TextMeshProUGUI playerNameText = playerEntry.transform.GetChild(0).Find("Body").GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI positionText = playerEntry.transform.GetChild(0).Find("Position").GetComponentInChildren<TextMeshProUGUI>();
            //Text scoresText = playerEntry.transform.Find("Scores").GetComponent<Text>();

            playerNameText.text = playerData.playerName;
            positionText.text = positionIndex.ToString();

            positionIndex++;
            //scoresText.text = string.Join(", ", playerData.scores);
        }
    }

    // Clear previous player entry prefabs
    private void ClearLeaderboard()
    {
        foreach (Transform child in playerEntryContainer)
        {
            Destroy(child.gameObject);
        }
    }

    // Method to display the leaderboard
    public void DisplayLeaderboard()
    {
        PopulateLeaderboard();
    }

    // Example usage
    private void Start()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();

        LoadPlayerData();
        AddPlayerDataToLeaderboard(PlayerPrefs.GetString(Strings.Username), Strings.GetChaptersProgressions());
        DisplayLeaderboard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (scrollToBottomCoroutine != null)
                StopCoroutine(scrollToBottomCoroutine);
            scrollToTopCoroutine = StartCoroutine(ScrollToTop());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (scrollToTopCoroutine != null)
                StopCoroutine(scrollToTopCoroutine);
            scrollToBottomCoroutine = StartCoroutine(ScrollToBottom());
        }
    }

    private IEnumerator ScrollToTop()
    {
        float scrollSpeed = (1 - scrollRect.verticalNormalizedPosition) / 2f;
        while (scrollRect.verticalNormalizedPosition != 1)
        {
            if (scrollRect.verticalNormalizedPosition >= 1)
            {
                scrollRect.verticalNormalizedPosition = 1;
                yield break;
            }
            Debug.Log("scrolling to top");
            scrollRect.verticalNormalizedPosition += scrollSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ScrollToBottom()
    {
        float scrollSpeed = scrollRect.verticalNormalizedPosition / 2f;
        while (scrollRect.verticalNormalizedPosition != 0)
        {
            if (scrollRect.verticalNormalizedPosition <= 0)
            {
                scrollRect.verticalNormalizedPosition = 0;
                yield break;
            }
            Debug.Log("scrolling to bottom");
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
