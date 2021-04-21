using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeft = 10f;
    [SerializeField] private Text timerText = null;
    [SerializeField] private Text GameOverText = null;
    [SerializeField] private Text InstructionsText = null;
    private string displayString;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        
        timerText.text = string.Format("Time: {0}.{1:00}",((int)timeLeft /60), ((int)timeLeft%60));
        if (timeLeft <= 0.0f)
        {
            Time.timeScale = 0;
            timerText.text = string.Format("Time: 0.00");
            GameOverText.text = "time";
            InstructionsText.text = "press Q to reset";

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("game");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
