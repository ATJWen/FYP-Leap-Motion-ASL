using System;
using System.Collections.Generic;
using System.Threading;

class CombineModels
{
    string word = "TEST";
    string emotion = "TEST";
    SignLanguagInterpretor sli = new SignLanguagInterpretor();
    EmotionRecognition er = new EmotionRecognition();


    public void GetFaceFromWebcam()
    {
        LiveWebcam cam = new LiveWebcam();

        while(true)
        {
            cam.start_webcam();
        }
    }

    public void GetWordFromInterpretor()
    {       
        try
        {
            while(true)
            {
                sli.Interpret();                
            }
        } 
        catch (InvalidOperationException e)
        {
            
        }
    }

    public void GetFacialExpression()
    {
        try
        {
            while(true)
            {
                er.GetEmotion();
            }
        } 
        catch (InvalidOperationException e)
        {
            
        }
    }

    public void GetWordFromInterpretor_output()
    {
        try
        {
            while(true)
            {            
                word = sli.GetWordOutput();
            }
        } 
        catch (InvalidOperationException e)
        {
            
        }
    }

    public void GetFacialExpression_output()
    {
        try
        {
            while(true)
            {
                emotion = er.GetEmotionOutput();
            }
        } 
        catch (InvalidOperationException e)
        {
            
        }
    }

    public void CombineModel()
    {
        while(true)
        {
            try
            {
                if(emotion == "neutral" && word != null)
                {
                    Console.WriteLine(word.ToLower());
                }
                else if (emotion == "happy" && word != null)
                {
                    Console.WriteLine(word);
                }

                else
                {
                    
                }
            } 
            catch (InvalidOperationException e)
            {
                
            }
        }
    }

    public void GenerateWord()
    {
        // ThreadStart webcamthread = new ThreadStart(GetFaceFromWebcam);
        // Thread wthread = new Thread(webcamthread);
        // wthread.Start();

        ThreadStart signlanguagethread = new ThreadStart(GetWordFromInterpretor);
        Thread th1 = new Thread(signlanguagethread);
        th1.Start();

        ThreadStart facialexpressionthread = new ThreadStart(GetFacialExpression);
        Thread th2 = new Thread(facialexpressionthread);
        th2.Start();

        ThreadStart signlanguageoutputthread = new ThreadStart(GetWordFromInterpretor_output);
        Thread th3 = new Thread(signlanguageoutputthread);
        th3.Start();

        ThreadStart facialexpressionoutputthread = new ThreadStart(GetFacialExpression_output);
        Thread th4 = new Thread(facialexpressionoutputthread);
        th4.Start();

        ThreadStart modelthread = new ThreadStart(CombineModel);
        Thread mth = new Thread(modelthread);
        mth.Start();
    }
}