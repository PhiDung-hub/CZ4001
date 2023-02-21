using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI counterText;
    public float seconds, minutes;
    public float TimeLimit = 1;
    
	// public GameObject winTextObject;

    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        // winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        counterText.text = "Timer: "+minutes.ToString("00")+":"+seconds.ToString("00");
    }
    //     if (minutes < TimeLimit)
    //     {
    //         minutes = (int)(Time.time / 60f);
    //         seconds = (int)(Time.time % 60f);
    //         counterText.text = "Timer: "+minutes.ToString("00")+":"+seconds.ToString("00");
    //     }
    //     else
    //     {
    //         stopGame();
    //     }
    // }
    // void stopGame()
    // {
    //     winTextObject.SetActive(true);
    // }
}
