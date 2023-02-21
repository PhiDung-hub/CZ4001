using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public TextMeshProUGUI targetRemainingText;
    public TextMeshProUGUI timeElapsedText;
    public TextMeshProUGUI winText;
    public uint targetRemaining;
    const uint TOTAL_TARGET = 12;
    private bool gameEnded;


    void Start()
    {
        gameEnded = false;
        winText.enabled = false;
        targetRemaining = TOTAL_TARGET;
        UpdateTargetRemainingText();
    }

    void Update()
    {
        if (!gameEnded)
        {
            uint seconds = (uint)Time.time;
            SetTimeText(seconds);
        }
    }

    public void UpdateTargetRemainingText()
    {
        if (targetRemaining == 0)
        {
            winText.enabled = true;
            winText.text = string.Format("You Win!\nTime = {0}", displayTime((uint)Time.time));
            gameEnded = true;
            timeElapsedText.enabled = false;
            targetRemainingText.enabled = false;
        }
        targetRemainingText.text = targetRemaining.ToString() + " left";
    }

    void SetTimeText(uint seconds)
    {
        string timeText = displayTime(seconds);
        timeElapsedText.SetText(timeText);
    }

    string displayTime(uint seconds)
    {
        uint hours = seconds / 3600;
        uint minutes = (seconds % 3600) / 60;
        uint secs = seconds % 60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, secs);
    }
}
