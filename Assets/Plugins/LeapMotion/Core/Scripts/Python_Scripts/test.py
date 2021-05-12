import UnityEngine as ue
import os
from time import time

# milliseconds = int(time()*1000)
# startscreenshot = True

# while(startscreenshot):
#     ue.ScreenCapture.CaptureScreenshot(os.path.abspath("C:/Users/lenovo/Desktop/SIGN LANGUAGE AI/ASL Ultraleap/Assets/Plugins/LeapMotion/Core/Screenshots/screenshot" + str(i) + ".jpg"))
#     i+=1
#     ue.Debug.Log("Screenshot " + str(i))
#     if keyboard.is_pressed('q'):
#         startscreenshot = False

ue.ScreenCapture.CaptureScreenshot(os.path.abspath("C:/Users/lenovo/Desktop/SIGN LANGUAGE AI/ASL Ultraleap/Assets/Plugins/LeapMotion/Core/Screenshots/data_input/screenshot1620233641650.jpg"))
ue.Debug.Log("SNAP")

