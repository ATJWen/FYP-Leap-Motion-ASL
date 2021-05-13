import UnityEngine as ue
import os
from time import time

# milliseconds = int(time()*1000)

# ue.ScreenCapture.CaptureScreenshot(os.path.abspath("C:/Users/lenovo/Desktop/SIGN LANGUAGE AI/ASL Ultraleap/Assets/Plugins/LeapMotion/Core/Screenshots/Training/{FOLDER_NAME}" + str(i) + ".jpg"))
ue.ScreenCapture.CaptureScreenshot(os.path.abspath("C:/Users/lenovo/Desktop/SIGN LANGUAGE AI/ASL Ultraleap/Assets/Plugins/LeapMotion/Core/Screenshots/data_input/screenshot1620233641650.jpg"))
ue.Debug.Log("SNAP")

