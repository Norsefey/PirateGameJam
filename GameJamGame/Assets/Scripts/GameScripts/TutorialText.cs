using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] Button nextButt;
    [SerializeField] Button startButt;

    // Start is called before the first frame update
    void Start()
    {
        //text.text = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowControls()
    {
        text.text = "--Controls--\r\n\"W\" " +
            "- Accelerate\r\n\"S\" " +
            "- Reverse\r\n\"Left Shift\" " +
            "- Brake\r\n\"Space\" " +
            "- Boost\r\n" +
            "--Look--\r\n" +
            "Hold R- Look Behind\r\n\"F\" " +
            "- Toggle Rear-view Mirror";

        nextButt.gameObject.SetActive(false);
        startButt.gameObject.SetActive(true);
    }
}
