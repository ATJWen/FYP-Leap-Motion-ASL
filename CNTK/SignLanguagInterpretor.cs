using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Vision;

class SignLanguagInterpretor
{
    public string output_word;
    
    public string Interpret()
    {
        Console.WriteLine("STARTING SIGN LANGUAGE INTERPRETER");

        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
        string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\..\Assets\Plugins\LeapMotion\Core\Screenshots\data_input");
        string unityScreenshotsPath = Path.GetFullPath(sFile);

        MLContext mlContext = new MLContext();
        
        //Define DataViewSchema for data preparation pipeline and trained model
        DataViewSchema modelSchema;

        // Load trained model
        ITransformer trainedModel = mlContext.Model.Load("handmodel.zip", out modelSchema);

        IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: unityScreenshotsPath, useFolderNameAsLabel: true);
        IDataView shuffledData = mlContext.Data.LoadFromEnumerable(images);

        var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                inputColumnName: "Label",
                outputColumnName: "LabelAsKey")
            .Append(mlContext.Transforms.LoadRawImageBytes(
                outputColumnName: "Image",
                imageFolder: unityScreenshotsPath,
                inputColumnName: "ImagePath"));

        IDataView preProcessedData = preprocessingPipeline
                .Fit(shuffledData)
                .Transform(shuffledData);

        TrainTestData trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
        TrainTestData validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

        IDataView trainSet = trainSplit.TrainSet;
        IDataView validationSet = validationTestSplit.TrainSet;
        IDataView testSet = validationTestSplit.TestSet;

        string predictedword;
        while(true){
            predictedword = ClassifySingleImage(mlContext, testSet, trainedModel);
            // Console.WriteLine(predictedword);
            output_word = predictedword;
        }
    }
    public static string ClassifySingleImage(MLContext mlContext, IDataView data, ITransformer trainedModel)
    {
        PredictionEngine<ModelInput, ModelOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);
        ModelInput image = mlContext.Data.CreateEnumerable<ModelInput>(data,reuseRowObject:true).FirstOrDefault();
        ModelOutput prediction = predictionEngine.Predict(image);

        // Console.WriteLine("Classifying single image");
        return prediction.PredictedLabel;
    }

    public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
    {
        var files = Directory.GetFiles(folder, "*",
            searchOption: SearchOption.AllDirectories);

        foreach (var file in files)
        {
            if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png"))
                continue;

            var label = Path.GetFileName(file);

            if (useFolderNameAsLabel)
                label = Directory.GetParent(file).Name;
            else
            {
                for (int index = 0; index < label.Length; index++)
                {
                    if (!char.IsLetter(label[index]))
                    {
                        label = label.Substring(0, index);
                        break;
                    }
                }
            }

            yield return new ImageData()
            {
                ImagePath = file,
                Label = label
            };
        }
    }

    public string GetWordOutput(){
        return output_word;
    }
}


class ImageData
{
    public string ImagePath { get; set; }

    public string Label { get; set; }
}

class ModelInput
{
    public byte[] Image { get; set; }
    
    public UInt32 LabelAsKey { get; set; }

    public string ImagePath { get; set; }

    public string Label { get; set; }
}

class ModelOutput
{
    public string ImagePath { get; set; }

    public string Label { get; set; }

    public string PredictedLabel { get; set; }
}

