using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class PlatformDataStructureGenerator : MonoBehaviour 
{
    private int dataSetLength = 4;

    private void Start()
    {
        GenerateDataStructure(new ObstacleStructures.FloorJumpFloor());    
    }


    public void GenerateDataStructure(Obstacle obstacle)
    {
        if(obstacle.Name == "")
        {
            print("Obstable string empty");
            return;
        }

        UnpackObstacleStructure(obstacle);
     
    }

    private void UnpackObstacleStructure(Obstacle obstacle)
    {
        var characterList = NewCharacterList();
        var obstacleLength = obstacle.Name.Length;

        Debug.Log("Is obstacle length more than 4: " + (obstacleLength > 4) + ". It is " + obstacleLength);

        for(int i = 0; i < dataSetLength; i++)
        {
            var index = GetIndex(2, i);

            if(i > (obstacleLength - 1))
            {
                // Use the difficulty to determine what should be placed.


                print("No more obstacles left to place. Inserting floor tile.");
                characterList[index] = 'F';
                continue;
            }
            else
            {
                var currentCharacter = obstacle.Name[i];
                characterList[index] = currentCharacter;
            }
        }
        PrintDataStructureToTextFile(obstacle.Name, characterList);
        GetComponent<PlatformWorldObjectGenerator>().CreateWorldObject(characterList);
    }


    private void PrintDataStructureToTextFile(string name, List<char> characterList)
    {
        var filename = "obstacle-output.txt";

        if(File.Exists(filename))
        {
            Debug.Log(filename + " already exists.. Deleting.");
            File.Delete(filename);
        }
        var outputFile = File.CreateText(filename);
        outputFile.WriteLine("Obstacle data output for " + name);


        print("Characterset size: " + characterList);
        string line = "";
        for(int i = 0; i < characterList.Count; i++)
        {
            if((i % dataSetLength == 0) && (i > 0))
            {
                print("Outputting line " + line);
                outputFile.WriteLine(line + "\n");
                line = "";
            }
            line += characterList[i];

        }
        outputFile.Write("\n\n");
        outputFile.Close();

       
    }

    private List<char> NewCharacterList()
    {
        return new List<char>(new char[] 
        { 
            '*', '*', '*', '*',
            '*', '*', '*', '*',
            '*', '*', '*', '*',
            '*', '*', '*', '*'
        });
    }

    private int GetIndex(int row, int column)
    {
        var index = ((dataSetLength * row) + column);
        print("ID for row (" + row + ") and column (" +column + ") is " + index);
        return index;
    }
}
