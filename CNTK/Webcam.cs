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
        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
        string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\assets\neutral");
        string faceImagePath = Path.GetFullPath(sFile);
        Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
        bitmap.Save(faceImagePath + i + ".jpg");
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