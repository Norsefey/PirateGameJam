using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    bool transitionStarted = false;
    IEnumerator ChangeScene()
    {
        transitionStarted = true;
        Camera.main.GetComponent<CameraFollow>().followTarget = false;
        PlayerStats.Instance.ReachedFinishLine();
        yield return new WaitForSeconds(3);
        // load the upgrade Scene
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !transitionStarted)
        {
            StartCoroutine(ChangeScene());
        }
    }
}
