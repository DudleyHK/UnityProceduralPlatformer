using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;

// This unpacks the data files
public class DataReader : ScriptableObject 
{
    public ushort dataLength = 4;

    private List<char> characterList;
    private int width = 0;
    private int height = 0;


    /// <summary>
    /// Create list which represents a 2D array of data. Read and return the width and height
    ///     and finally return the newly made array of char data.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public List<char> ExtractLevelData(string file, ref int width, ref int height)
    {
        FileInfo     sourceFile = new FileInfo(file);
        StreamReader reader  = sourceFile.OpenText();
        characterList = new List<char>();


        string text;
        do
        {
            text = reader.ReadLine();

            if(text == null) continue;
            if(text == "")   continue;
            if(text == "")   continue;

            // Get the width and height values of the data.
            if(text[0] == 'W')
            {
                width = GetValue(text);

            }
            else if(text[0] == 'H')
            {
                height = GetValue(text);
            }
            else
            {
                // run through the line and add it to an array
                foreach(var character in text)
                {
                    characterList.Add(character);
                }
            }
            Debug.Log(text);
        }
        while(text != null);

        return characterList;
    }


    private int GetValue(string text)
    {
        var splitStr = text.Split(':');
        var appendedStr = splitStr[1].Trim(' ');
        return int.Parse(appendedStr);
    }











    public List<char> RunDataReader(Obstacle obstacle)
    {
        if (obstacle.Name == "")
        {
            Debug.Log("Obstable string empty");
            return null;
        }
        GenerateData(obstacle);

        return characterList;
    }


    private void GenerateData(Obstacle obstacle)
    {
        FillList(obstacle);
        EditList(obstacle);
    }

    private void FillList(Obstacle obstacle)
    {
        characterList = NewCharacterList();
        var obstacleLength = obstacle.Name.Length;

        for (var i = 0; i < dataLength; i++)
        {
            var index = GetIndex(2, i);

            if (index >= characterList.Count) Debug.Log("ERROR");

            if (i > (obstacleLength - 1))
            {
                //Debug.Log("No more obstacles left to place. Inserting floor tile.");
                characterList[index] = GetLastCharacter(obstacle.Difficulty);
                continue;
            }
            else
            {
                var currentCharacter = obstacle.Name[i];
                characterList[index] = currentCharacter;
            }
        }
    }


    private void EditList(Obstacle obstacle)
    {
        for (var i = 0; i < characterList.Count; i++)
        {
            var character = characterList[i];
            switch (character)
            {
                case 'F': characterList[i + dataLength] = 'G'; break;
                case 'J': characterList[i + dataLength] = 'D'; break;
                case 'j': characterList[i + dataLength] = 'G'; break;
                case 'G': break;
            }
        }
    }



    private char GetLastCharacter(int difficulty)
    {
        //Debug.Log("No more obstacles left to place. Inserting floor tile.");
        if (difficulty == 1)
        {
            // Easy level difficulty
            return 'F';
        }
        else if (difficulty == 2)
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
        var index = ((dataLength * row) + column);
        //Debug.Log("ID for row (" + row + ") and column (" + column + ") is " + index);
        return index;
    }


    private char JumpCost(ushort costOfFailure)
    {
        if (costOfFailure == 1)
        {
            return 'G';
        }
        else if (costOfFailure == 2)
        {
            return 'D';
        }
        else
        {
            return 'D';
        }
    }
}
