using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Strings
{
    public static string TimeProfileCreated = "TimeProfileCreated";
    public static string Username = "Username";
    public static string ProfileImage = "ProfileImage";

    public static string ChapterOneProgressions = "ChapterOneProgressions";
    public static string ChapterTwoProgressions = "ChapterTwoProgressions";
    public static string ChapterThreeProgressions = "ChapterThreeProgressions";
    public static string ChapterTwoLevelOneCompleted = "ChapterTwoLevelOneCompleted";
    public static string ChapterTwoLevelTwoCompleted = "ChapterTwoLevelTwoCompleted";
    public static string ChapterTwoLevelThreeCompleted = "ChapterTwoLevelThreeCompleted";
    public static string ChapterThreeLevelOneCompleted = "ChapterThreeLevelOneCompleted";

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(ChapterOneProgressions);
        PlayerPrefs.DeleteKey(ChapterTwoProgressions);
        PlayerPrefs.DeleteKey(ChapterThreeProgressions);
        PlayerPrefs.DeleteKey(ChapterTwoLevelOneCompleted);
        PlayerPrefs.DeleteKey(ChapterTwoLevelTwoCompleted);
        PlayerPrefs.DeleteKey(ChapterTwoLevelThreeCompleted);
        PlayerPrefs.DeleteKey("Reviewed");
        PlayerPrefs.DeleteKey("firstTime");
    }

    public static bool IsFirstTime()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
