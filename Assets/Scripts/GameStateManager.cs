using System;
using System.Collections;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Playables;

public class GameStateManager : MonoBehaviour
{
    public TextMeshProUGUI targetRemainingText;
    public TextMeshProUGUI ammoAcountText;
    public TextMeshProUGUI timeElapsedText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI notificationText;
    public Material skyboxDark;
    public Material skyboxLight;
    public uint targetRemaining;
    const uint TOTAL_TARGET = 12;
    private bool gameEnded;
    public uint seconds;
    public Transform pistols;


    void Start()
    {
        gameEnded = false;
        winText.enabled = false;
        notificationText.enabled = false;
        notificationText.text = "";
        targetRemaining = TOTAL_TARGET;
        UpdateTargetRemainingText();
        RenderSettings.skybox = skyboxDark;

    }

    void Update()
    {
        if (!gameEnded)
        {
            uint seconds = (uint)Time.time;
            SetTimeText(seconds);
        }
    }

    public void toggleMenu()
    {
        timeElapsedText.enabled = !timeElapsedText.enabled;
        targetRemainingText.enabled = !targetRemainingText.enabled;
        notificationText.enabled = !notificationText.enabled;
    }

    public void UpdateTargetRemainingText()
    {
        IEnumerator LevelUpCoroutine()
        {
            notificationText.enabled = true;
            notificationText.text = string.Format("Your mental wellness has improved!\nStay healthy and keep going on your journey");
            timeElapsedText.enabled = false;
            targetRemainingText.enabled = false;

            yield return new WaitForSeconds(30f);
            RenderSettings.skybox = skyboxLight;

            notificationText.enabled = false;
            timeElapsedText.enabled = true;
            targetRemainingText.enabled = true;
            ammoAcountText.enabled = false;
        }
        if (targetRemaining == 0)
        {
            winText.enabled = true;
            winText.text = string.Format("Congrats, you have fully recovered <sprite=3> !\n{0}", displayTime((uint)Time.time));
            gameEnded = true;
            timeElapsedText.enabled = false;
            targetRemainingText.enabled = false;
        }
        else if (targetRemaining == 6)
        {
            StartCoroutine(LevelUpCoroutine());
            // Disable and destroy all gun for the next level
            foreach (Transform pistol in pistols)
            {
                Destroy(pistol.gameObject);
            }
            RenderSettings.skybox = skyboxLight;
        }

        targetRemainingText.text = targetRemaining.ToString() + " <sprite=18> left";
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
        seconds = secs;

        return string.Format("<sprite=16> {0:00}:{1:00}:{2:00}", hours, minutes, secs);
    }

    public void stateEnd()
    {
        winText.enabled = true;
        winText.text = string.Format("End!!!\nTime = {0} \n {1} Target Attacked ", displayTime((uint)Time.time), TOTAL_TARGET - targetRemaining);
        gameEnded = true;
        timeElapsedText.enabled = false;
        targetRemainingText.enabled = false;
    }
}
