using System;
using System.Collections.Generic;
using System.Threading;

class CombineModels
{
    string word;
    string emotion;

    public void GetWordFromInterpretor()
    {
        SignLanguagInterpretor sli = new SignLanguagInterpretor();

        while(true)
        {
            sli.Interpret();                
            word = sli.output_word;
            Console.WriteLine(word);
        }
    }

    public void GetFacialExpression()
    {
        int i = 0;

        while(true)
        {
            i++;
            Console.WriteLine(i);
            Thread.Sleep(10000);
        }
    }

    public void GenerateWord()
    {
        ThreadStart signlanguagethread = new ThreadStart(GetWordFromInterpretor);
        Thread th1 = new Thread(signlanguagethread);
        th1.Start();

        ThreadStart facialexpressionthread = new ThreadStart(GetFacialExpression);
        Thread t2 = new Thread(facialexpressionthread);
        t2.Start();
    }
}