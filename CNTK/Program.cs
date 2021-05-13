using System;
using System.Collections.Generic;

namespace CNTK
{
    class Program
    {
        
        static void Main(string[] args)
        {            
            CombineModels cm = new CombineModels();
            cm.GenerateWord();

            // HandTrainModel hm = new HandTrainModel();
            // hm.HandModelTraining();

            // LiveWebcam cam = new LiveWebcam();
            // cam.start_webcam();

            // FaceTrainModel ftm = new FaceTrainModel();
            // ftm.FaceModelTraining();

            // EmotionRecognition er = new EmotionRecognition();
            // er.Emotion();
        }

    }


    
}
