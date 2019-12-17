using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrainReturnLuL : MonoBehaviour
{
    public void OnBack()
    {
        SceneManager.LoadScene("Title");
    }
}
