using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera myCamera;
    private static ScreenshotHandler instance;
    private bool TakeScreenshotOnNextFrame;

    public int fileCount = 0;

    private void Awake()
    {
        myCamera = gameObject.GetComponent<Camera>();
        instance = this;
    }

    private void OnPostRender()
    {
        if(TakeScreenshotOnNextFrame)
        {
            TakeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Screenshots/" + fileCount + ".png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        TakeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
