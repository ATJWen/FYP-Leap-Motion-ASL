using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

public class MainCameraImageRecognition : MonoBehaviour
{

    private int fileCount = 0;
    private bool keepRunning = false;

    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = 300;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown("space"))
        {
            Debug.Log("SPACE");

            if(!keepRunning)
            {
                keepRunning = true;
                Debug.Log("START");
            }

            else
            {
                keepRunning = false;
                Debug.Log("END");
            }
        }

        if(keepRunning)
        {
            CamCapture();
            Debug.Log("Screenshot taken");
        }
    }
    void CamCapture()
    {
        // ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screenshots/" + fileCount + ".png");
        // fileCount++;

        string path = Application.dataPath + "/Plugins/LeapMotion/Core/Scripts/Python_Scripts/test.py";
        PythonRunner.RunFile(path);
    }

}
