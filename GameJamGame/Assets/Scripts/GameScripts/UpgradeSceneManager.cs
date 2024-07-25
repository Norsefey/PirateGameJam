using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSceneManager : MonoBehaviour
{
    private void Start()
    {
        PlayerStats.Instance.DisablePlayer();
    }
    public void GoToNextScene()
    {
        PlayerStats.Instance.EnablePlayer();

        int index = PlayerStats.Instance.levelIndex;
        SceneManager.LoadScene(index);
    }
}
