using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class PlatformDataStructureGenerator : MonoBehaviour 
{
    private int dataSetLength = 4;

    private List<Obstacle> obstcaleStructures = new List<Obstacle>(new Obstacle[] 
    {
        new ObstacleStructures.FloorJumpFloor(),
        new ObstacleStructures.FloorJumpFloor(),
        new ObstacleStructures.FloorJumpFloor(),
        new ObstacleStructures.FloorJumpFloor(),
        new ObstacleStructures.FloorJumpFloor()
     });


    private List<List<char>> characterLists = new List<List<char>>();

    private void Start()
    {
        foreach(var obstacle in obstcaleStructures)
        {
            GenerateDataStructure(obstacle);    
        }



        GetComponent<PlatformWorldObjectGenerator>().CreateWorldObject(characterLists);
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

        for(var i = 0; i < dataSetLength; i++)
        {
            var index = GetIndex(2, i);

            if(i > (obstacleLength - 1))
            {
                print("No more obstacles left to place. Inserting floor tile.");
                characterList[index] = GetLastCharacter(obstacle.Difficulty);
                continue;
            }
            else
            {
                var currentCharacter = obstacle.Name[i];
                characterList[index] = currentCharacter;
            }
        }

        for(var i = 0; i < characterList.Count; i++)
        {
            var character = characterList[i];
            switch(character)
            {
                case 'F':
                    characterList[i + dataSetLength] = 'G';
                    print("F found at " + i);
                    break;
                case 'G':
                    break;
                case 'J':
                    print("J found at " + i);
                    characterList[i + dataSetLength] = JumpCost(obstacle.CostOfFailure);
                    break;
            }
        }



        PrintDataStructureToTextFile(obstacle.Name, characterList);
        characterLists.Add(characterList);
    }

    private void PrintDataStructureToTextFile(string name, List<char> characterList)
    {
        var filename = "obstacle-output.txt";

        if(File.Exists(filename))
        {
            File.Delete(filename);
        }
        var outputFile = File.CreateText(filename);
        outputFile.WriteLine("Obstacle data output for " + name);


        string line = "";
        for(int i = 0; i < characterList.Count; i++)
        {
            if((i % dataSetLength == 0) && (i > 0))
            {
                print("Outputting line " + line);
                outputFile.WriteLine(line + "\n");
                line = "";
            }
            print("Character in list is " + characterList[i]);
            line += characterList[i];
        }

        outputFile.Write(line + "\n\n");
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
        print("ID for row (" + row + ") and column (" + column + ") is " + index);
        return index;
    }

    private char GetLastCharacter(int difficulty)
    {
        print("No more obstacles left to place. Inserting floor tile.");
        if(difficulty == 1)
        {
            // Easy level difficulty
            return 'F';
        }
        else if(difficulty == 2)
        {
            // Medium level difficulty
            return 'F';

        }
        else
        {
            // Hard difficulty, turns into two jumps in a row. 
            return '*'; 
        }
    }

    private char JumpCost(ushort costOfFailure)
    {
        if(costOfFailure == 1)
        {
            return 'G';
        }
        else if(costOfFailure == 2)
        {
            return 'D';
        }
        else
        {
            return 'D';
        }
    }
}
