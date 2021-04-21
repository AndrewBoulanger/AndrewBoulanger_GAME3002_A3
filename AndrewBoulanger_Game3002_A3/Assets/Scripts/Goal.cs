using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Text instructionsText;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        instructionsText.text = "press up or w to enter goal";
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && Input.GetAxis("Vertical") > 0)
        {
            gameOverText.text = "Goal!";
            instructionsText.text = "you can press esc to quit the application";
            Time.timeScale = 0;
        }
    }
}
