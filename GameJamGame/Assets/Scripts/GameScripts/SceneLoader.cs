using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadAScene(int index)
    {
            SceneManager.LoadScene(index);
    }

    public void TryAgain()
    {
        if (PlayerStats.Instance != null)
        {
            if (PlayerStats.Instance.levelOneCompleted)
                SceneManager.LoadScene(2);
            else
                SceneManager.LoadScene(1);
        }
    }

}
