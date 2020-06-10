using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    //=========================================== Variables ===========================================//
    //   This script is located on the 'ButtonHandler' empty game object in the 'Title' game scene.

    // Scene Wide
    public GameObject mainCamera;
    public GameObject titleCanvas;
    public GameObject modeSelectionCanvas;
    public GameObject p1ModeSelectionCanvas;
    private bool cameraMoving = false;
    private int cameraMoveNumber;
    private Vector3 destinationCameraPosition;

    // Title Screen
    public Button soloButton;

    // Mode Select Screen
    public Button fightOrTrainButton;
    public Image fightImage;
    public Image trainImage;
    private bool fightOrTrain = true;

    public Image hazardOnImage;
    public Image hazardOffImage;
    private bool hazardBool;

    public Image lineAttackOnImage;
    public Image lineAttackOffImage;
    private bool lineAttackBool;

    public int windLevelInt = 0;
    public Image windLevel0Image;
    public Image windLevel1Image;
    public Image windLevel2Image;
    public Image windLevel3Image;
    public Image windLevel4Image;

    public int stageLevelInt = 1;
    public Image stageLevel0Image;
    public Image stageLevel1Image;
    public Image stageLevel2Image;


    // Kite Selection Screen





    public void Start()
    {
        titleCanvas.SetActive(true);
        modeSelectionCanvas.SetActive(false);
        p1ModeSelectionCanvas.SetActive(false);
    }

    public void Update()
    {
        if (cameraMoving == true)
        {
            CameraMoves(cameraMoveNumber);

            if (mainCamera.transform.position == destinationCameraPosition)
            {
                cameraMoving = false;
            }
        }
    }


    private void CameraMoves(int moveNumber)
    {
        switch (moveNumber)
        {
            // Title Screen to Mode Selection (Solo / VS Button)
            case 1:
                Quaternion modeSelectionCameraRotation = Quaternion.LookRotation(new Vector3(1, 0, 0), Vector3.up);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destinationCameraPosition, 2 * Time.deltaTime);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, modeSelectionCameraRotation, 2 * Time.deltaTime);
                break;

            // Mode Selection to Kite Selection
            case 2:
                Quaternion titleSelectionCameraRotation = Quaternion.LookRotation(new Vector3(.7f, .3f, 1), Vector3.up);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destinationCameraPosition, 2 * Time.deltaTime);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, titleSelectionCameraRotation, 2 * Time.deltaTime);
                break;
        }
    }



    //=========================================== UI Button Methods ===========================================//

    // ----------------------- Title Canvas ----------------------- //
    public void SoloButton()
    {
        cameraMoving = true;
        cameraMoveNumber = 1;
        destinationCameraPosition = new Vector3(28, 11.5f, 110);

        titleCanvas.SetActive(false);
        modeSelectionCanvas.SetActive(true);
        p1ModeSelectionCanvas.SetActive(true);


        // Sets the default selection
        fightOrTrainButton.Select();
    }

    public void LocalPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NetworkButton()
    {

    }

    public void OptionsButton()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }


    // ----------------------- Mode Selection Canvas ----------------------- //
    
    public void ModeSelectBackButton()
    {
        cameraMoving = true;
        cameraMoveNumber = 2;
        destinationCameraPosition = new Vector3(11, 5, 11);

        titleCanvas.SetActive(true);
        modeSelectionCanvas.SetActive(false);

        // Sets the default selection
        soloButton.Select();
    }

    public void FightOrTrainButton()
    {
        fightOrTrain = !fightOrTrain;

        if (fightOrTrain == true)
        {
            fightImage.enabled = true;
            trainImage.enabled = false;
        }
        else
        {
            trainImage.enabled = true;
            fightImage.enabled = false;
        }
    }

    public void HazardButton()
    {
        hazardBool = !hazardBool;

        if (hazardBool == true)
        {
            hazardOnImage.enabled = true;
            hazardOffImage.enabled = false;
        }
        else
        {
            hazardOffImage.enabled = true;
            hazardOnImage.enabled = false;
        }
    }

    public void LineAttackButton()
    {
        lineAttackBool = !lineAttackBool;

        if (lineAttackBool == true)
        {
            lineAttackOnImage.enabled = true;
            lineAttackOffImage.enabled = false;
        }
        else
        {
            lineAttackOffImage.enabled = true;
            lineAttackOnImage.enabled = false;
        }
    }


    public void WindLowerButton()
    {
        SetWindLevel(false);
    }
    public void WindRaseButton()
    {
        SetWindLevel(true);
    }
    private void SetWindLevel(bool theWay)
    {
        if(theWay == true)
        {
            if (windLevelInt >= 4)
            { return; } else {
                windLevelInt++;
            }
        } else if(theWay == false)
        {
            if(windLevelInt <= 0)
            { return; } else {
                windLevelInt--;
            }
        }

        switch(windLevelInt)
        {
            case 0:
                windLevel0Image.enabled = true;
                windLevel1Image.enabled = false;
                windLevel2Image.enabled = false;
                windLevel3Image.enabled = false;
                windLevel4Image.enabled = false;
                break;
            case 1:
                windLevel0Image.enabled = false;
                windLevel1Image.enabled = true;
                windLevel2Image.enabled = false;
                windLevel3Image.enabled = false;
                windLevel4Image.enabled = false;
                break;
            case 2:
                windLevel0Image.enabled = false;
                windLevel1Image.enabled = false;
                windLevel2Image.enabled = true;
                windLevel3Image.enabled = false;
                windLevel4Image.enabled = false;
                break;
            case 3:
                windLevel0Image.enabled = false;
                windLevel1Image.enabled = false;
                windLevel2Image.enabled = false;
                windLevel3Image.enabled = true;
                windLevel4Image.enabled = false;
                break;
            case 4:
                windLevel0Image.enabled = false;
                windLevel1Image.enabled = false;
                windLevel2Image.enabled = false;
                windLevel3Image.enabled = false;
                windLevel4Image.enabled = true;
                break;
        }
    }


    public void StageLowerButton()
    {
        SetStageLevel(false);
    }
    public void StageRaseButton()
    {
        SetStageLevel(true);
    }
    private void SetStageLevel(bool theWay)
    {
        if (theWay == true)
        {
            if (stageLevelInt >= 2)
            { return; } else {
                stageLevelInt++;
            }
        }
        else if (theWay == false)
        {
            if (stageLevelInt <= 0)
            { return; } else {
                stageLevelInt--;
            }
        }

        switch (stageLevelInt)
        {
            case 0:
                stageLevel0Image.enabled = true;
                stageLevel1Image.enabled = false;
                stageLevel2Image.enabled = false;
                break;
            case 1:
                stageLevel0Image.enabled = false;
                stageLevel1Image.enabled = true;
                stageLevel2Image.enabled = false;
                break;
            case 2:
                stageLevel0Image.enabled = false;
                stageLevel1Image.enabled = false;
                stageLevel2Image.enabled = true;
                break;
        }
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
}
