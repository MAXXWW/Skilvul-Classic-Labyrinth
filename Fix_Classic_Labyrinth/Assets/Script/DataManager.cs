using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static void SaveData(GameManager player)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetFloat(currentSceneName, player.Data.Time);
        PlayerPrefs.Save();
    }

    // Mengembalikan nilai PlayerData makanya tidak menggunakan void
    public static PlayerData LoadData()
    {
        var tmpData = new PlayerData();
        string currentSceneName = SceneManager.GetActiveScene().name;

        tmpData.Time = PlayerPrefs.GetFloat(currentSceneName, 0f);
        return tmpData;
    }
}
