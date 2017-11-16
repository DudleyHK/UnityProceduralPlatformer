using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformDataStructureGenerator : MonoBehaviour 
{
    

    private void Start()
    {
        GenerateDataStructure(ObstacleStringLiterals.GROUND_JUMP_GROUND);    
    }


    public void GenerateDataStructure(string obstacle)
    {
        if(obstacle == "")
        {
            print("Obstable string empty");
            return;
        }

        UnpackObstacleString(obstacle);
     
    }

    private void UnpackObstacleString(string obstacle)
    {
        var characterList = NewCharacterList();
        var obstacleLength = obstacle.Length;

        Debug.Log("Is obstacle length more than 4: " + (obstacleLength > 4) + ". It is " + obstacleLength);

        for(int i = 0; i < 4; i++)
        {
            var index = GetIndex(2, i);

            if(i > (obstacleLength - 1))
            {
                print("No more obstacles left to place. Inserting floor tile.");
                characterList[index] = 'G';
                continue;
            }
            else
            {
                var currentCharacter = obstacle[i];
                characterList[index] = currentCharacter;
            }
        }
        PrintDataStructureToTextFile(obstacle, characterList);
        GetComponent<PlatformWorldObjectGenerator>().CreateWorldObject(characterList);
    }


    private void PrintDataStructureToTextFile(string obstacleStr, List<char> characterList)
    {
        var filename = "obstacle-output.txt";

        if(File.Exists(filename))
        {
            Debug.Log(filename + " already exists.. Deleting.");
            File.Delete(filename);
        }

        var outputFile = File.CreateText(filename);

        outputFile.WriteLine("Obstacle data output for " + obstacleStr + "\n\n");


        var line = characterList[0] + " " + characterList[1] + " " + characterList[2] + " " + characterList[3];
        outputFile.WriteLine(line + "\n");

        line = characterList[4] + " " + characterList[5] + " " + characterList[6] + " " + characterList[7];
        outputFile.WriteLine(line + "\n");

        line = characterList[8] + " " + characterList[9] + " " + characterList[10] + " " + characterList[11];
        outputFile.WriteLine(line + "\n");

        line = characterList[12] + " " + characterList[13] + " " + characterList[14] + " " + characterList[15];
        outputFile.WriteLine(line + "\n");

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
        var index = ((4 * row) + column);
        print("ID for row (" + row + ") and column (" +column + ") is " + index);
        return index;
    }
}
