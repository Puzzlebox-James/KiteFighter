using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    // Variables on the 'Button Handler' Component, along with some hacky shit
    public GameObject mainCamera;
    private Vector3 modeSelectionCameraPosition = new Vector3(28, 11.5f, 110);

    private bool cameraMoving = false;


    public GameObject titleCanvas;
    public GameObject modeSelectionCanvas;

    // hacky references to the stage images
    public Slider stageSelectSlider;
    private float stageSliderValue;

    public Image stageImage;
    public Sprite cityRoofsStageImage;
    public Sprite chillHillsStageImage;
    public Sprite redCanyonStageImage;

    public void Start()
    {
        titleCanvas.SetActive(true);
        modeSelectionCanvas.SetActive(false);
    }


    public void FlightTrainingButton()
    {
        SceneManager.LoadScene("TrainingScene");
    }

    public void FightButtonClick()
    {
        cameraMoving = true;

        //SceneManager.LoadScene("GameScene");
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


    public void StageSelectSlider()
    {
        stageSliderValue = stageSelectSlider.GetComponent<Slider>().value;
        if (stageSliderValue == 0)
        {
            Debug.Log("0");
            stageImage.sprite = redCanyonStageImage;

        } else if (stageSliderValue == 1)
        {
            Debug.Log("1");
            stageImage.sprite = chillHillsStageImage;
        }
        else
        {
            Debug.Log("2");
            stageImage.sprite = cityRoofsStageImage;
        }
    }

    public void Update()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 faceDic = new Vector3(1, 0, 0);
        Quaternion modeSelectionCameraRotation = Quaternion.LookRotation(faceDic, Vector3.up);

        if (cameraMoving == true)
        {
            titleCanvas.SetActive(false);
            modeSelectionCanvas.SetActive(true);
            
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, modeSelectionCameraPosition, 2 * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, modeSelectionCameraRotation, 2 * Time.deltaTime);
            
            if (mainCamera.transform.position == modeSelectionCameraPosition)
            {
                cameraMoving = false;
            }
        }
    }



    public void Exit()
    {
        Application.Quit();
    }
}
