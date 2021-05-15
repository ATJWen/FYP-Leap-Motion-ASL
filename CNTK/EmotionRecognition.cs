using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Vision;

class EmotionRecognition
{
    public string output_emotion;
    
    public string GetEmotion()
    {
        Console.WriteLine("STARTING EMOTION INTERPRETER");

        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
        string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\assets\screenshot");
        string faceImagePath = Path.GetFullPath(sFile);

        MLContext mlContext = new MLContext();
        
        //Define DataViewSchema for data preparation pipeline and trained model
        DataViewSchema modelSchema;

        // Load trained model
        ITransformer trainedModel = mlContext.Model.Load("facemodel.zip", out modelSchema);

        IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: faceImagePath, useFolderNameAsLabel: true);
        IDataView shuffledData = mlContext.Data.LoadFromEnumerable(images);

        var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                inputColumnName: "Label",
                outputColumnName: "LabelAsKey")
            .Append(mlContext.Transforms.LoadRawImageBytes(
                outputColumnName: "Image",
                imageFolder: faceImagePath,
                inputColumnName: "ImagePath"));

        IDataView preProcessedData = preprocessingPipeline
                .Fit(shuffledData)
                .Transform(shuffledData);

        TrainTestData trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
        TrainTestData validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

        IDataView trainSet = trainSplit.TrainSet;
        IDataView validationSet = validationTestSplit.TrainSet;
        IDataView testSet = validationTestSplit.TestSet;

        string predictedemotion;
        while(true){
            predictedemotion = ClassifySingleImage(mlContext, testSet, trainedModel);
            // Console.WriteLine(predictedemotion);
            output_emotion = predictedemotion;
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

    public String GetEmotionOutput(){
        return output_emotion;
    }
}