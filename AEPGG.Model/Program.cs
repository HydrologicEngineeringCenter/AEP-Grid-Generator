﻿using AEPGG.Model;

//Hard coded to local data. too big to upload to github. 
string lifecycleDirectoryPath = "D:\\AEP Grid\\Muncie_WAT\\runs\\Without_Project_Conditions\\FRA_50yr\\realization 1\\lifecycle 1\\";
string rasFilePath = "RAS\\Muncie.p08.hdf";
string outputFilePath = "D:\\AEP Grid\\muncie1D2D.hdf";
float[] theAEPs = [.99f, .5f, .2f, .1f, .02f];
string[] eventdirs = Directory.GetDirectories(lifecycleDirectoryPath); //returns directories without trailing "\\"



//initialize the computer
string seedfile = eventdirs[0] + "\\" + rasFilePath;
RasResultWrapper seedResult = new(seedfile);
AEPComputer computer = new(seedResult, 0.25f,20f);
//copy the seed file to the output file
File.Copy(seedfile, outputFilePath, true); 
for (int i = 0; i < eventdirs.Length; i++)
{
    string rasFile = eventdirs[i] + "\\" + rasFilePath;
    if (File.Exists(rasFile))
    {
        RasResultWrapper rasResult = new(rasFile);
        computer.AddResults(rasResult);
    }
}

AEPResultsWriter writer = new(outputFilePath);
bool success = writer.OverwriteMaxWSEinHDFResults(computer, .5f); // .5 = 2yr event
Console.WriteLine(success);
