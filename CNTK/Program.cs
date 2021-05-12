using System;
using System.Collections.Generic;

namespace CNTK
{
    class Program
    {
        
        static void Main(string[] args)
        {            
            // CombineModels cm = new CombineModels();
            // cm.GenerateWord();
            Webcam cam = new Webcam();
            cam.start_webcam();
        }

    }


    
}
