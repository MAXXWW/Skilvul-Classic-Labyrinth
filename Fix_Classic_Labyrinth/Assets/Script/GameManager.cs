using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] TMP_Text curTimeText;
    // [SerializeField] PlayerController player;
    [SerializeField] PhoneGravity player;
    [SerializeField] CameraTouchController cameraTouch;
    [SerializeField] Hole hole;
    [SerializeField] AudioSource sfxEnd;
    public PlayerData Data;
    float timer = 0f;
    float time;

    private void OnEnable()
    {
        Data = DataManager.LoadData();
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (hole.Entered && gameOverPanel.activeInHierarchy == false)
        {
            if (time <= Data.Time && Data.Time != 0f)
            {
                Data.Time = time;
                DataManager.SaveData(this);
            }
            else if (Data.Time == 0)
            {
                Data.Time = time;
                DataManager.SaveData(this);
            }

            Debug.Log("Best Time: " + Data.Time);
            bestTimeText.text = $"Best Time: {Data.Time}";
            curTimeText.text = $"Current Time: {time}";
            timerText.enabled = false;

            sfxEnd.Play();
            player.gameObject.SetActive(false);
            cameraTouch.enabled = false;
            gameOverPanel.SetActive(true);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                time += 1;
                timer = 0;
            }

            timerText.text = $"Time: {time}";
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
    }
}
