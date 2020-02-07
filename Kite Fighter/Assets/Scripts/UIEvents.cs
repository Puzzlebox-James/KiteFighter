using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    public void FlightTrainingButton()
    {
        SceneManager.LoadScene("TrainingScene");
    }

    public void FightButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToggleWind(bool value)
    {
        if (value == true)
        {
            KiteMovement.windOn = true;
        }
        else
        {
            KiteMovement.windOn = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
