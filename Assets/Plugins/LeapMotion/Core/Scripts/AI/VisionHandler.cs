using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool keepRunning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("YES");

            if(!keepRunning)
            {
                keepRunning = true;
                Debug.Log("BEGIN");
            }

            else
            {
                keepRunning = false;
                Debug.Log("END");
            }
        }

        if(keepRunning)
        {
            ScreenshotHandler.TakeScreenshot_static(Screen.width, Screen.height);
        }
    }
}
