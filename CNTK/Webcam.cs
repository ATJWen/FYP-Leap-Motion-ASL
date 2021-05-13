using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;

class Webcam
{
    public int i = 0;
    public void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
        bitmap.Save(@"C:\Users\lenovo\Desktop\SIGN LANGUAGE AI\ASL Ultraleap\CNTK\assets\neutral\" + i + ".jpg");
    }
    public void start_webcam()
    {
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice); 
        VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
        while(true)
        {
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
            i++;
        }

    }

    
}