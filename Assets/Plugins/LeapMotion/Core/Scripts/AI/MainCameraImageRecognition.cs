using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    void CamCapture(){
        // Camera Cam = GetComponent<Camera>();

        // RenderTexture currentRT = RenderTexture.active;
        // RenderTexture.active = Cam.targetTexture;
 
        // Cam.Render();
 
        // Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
        // Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
        // Image.Apply();
        // RenderTexture.active = currentRT;

        // var Bytes = Image.EncodeToPNG();
        // Destroy(Image);
 
        // File.WriteAllBytes(Application.dataPath + "/Screenshots/" + FileCounter + ".png", Bytes);
        // FileCounter++;
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screenshots/" + fileCount + ".png");
        fileCount++;
    }

}
