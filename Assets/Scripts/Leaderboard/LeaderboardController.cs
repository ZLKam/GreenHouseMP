using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    private HashSet<PlayerData> playerDataSet = new HashSet<PlayerData>();
    private const string PlayerDataFileName = "playerdata.json";

    // Load saved player data from JSON file
    private void LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            List<PlayerData> playerDataList = JsonUtility.FromJson<List<PlayerData>>(json);
            playerDataSet = new HashSet<PlayerData>(playerDataList);
        }
    }

    // Save current player data to JSON file
    private void SavePlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PlayerDataFileName);
        List<PlayerData> playerDataList = new List<PlayerData>(playerDataSet);
        string json = JsonUtility.ToJson(playerDataList);
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

    // Method to display the leaderboard
    public void DisplayLeaderboard()
    {
        Debug.Log("Leaderboard:");
        foreach (PlayerData playerData in playerDataSet)
        {
            string scoresString = string.Join(", ", playerData.scores);
            Debug.Log(playerData.playerName + ": " + scoresString);
        }
    }

    // Example usage
    void Start()
    {
        LoadPlayerData();
        DisplayLeaderboard();
    }

    //look for a specific trigger
    //when triggered check for the current registered name and total of badges collected
    //prefab will have script to then check it and adjust its own values
    //how do i store players from the leaderboard when they leave the game
}
