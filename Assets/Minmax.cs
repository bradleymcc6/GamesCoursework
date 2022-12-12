using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


//using static GameController;

public class Minmax : MonoBehaviour
{
    bool playerPlayed;
    bool aIPlayed;

    public GameObject yourTurn;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject[] cells;
    public GameObject currentCell;
    List<int> playedCells = new List<int>();
    //public GameObject[] aICells;
    List<GameObject> aICells = new List<GameObject>();
    List<int> aICellsTried = new List<int>();
    int greenScore;
    public enum WinningPatterns {row1, row2, row3, column1, column2, column3, bottomLeftToTopRight, topLeftToBottomRight};
    public Dictionary<WinningPatterns, int> greenCells = new Dictionary<WinningPatterns, int>();
    public Dictionary<WinningPatterns, int> redCells = new Dictionary<WinningPatterns, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        bool playerPlayed = false;
        bool aIplayed = false;
        cells = GameObject.FindGameObjectsWithTag("Cell");
        //aICells = GameObject.FindGameObjectsWithTag("Cell");
        int score = 0;
        greenScore = 0;
        foreach (WinningPatterns winningPatterns in Enum.GetValues(typeof(WinningPatterns)))
        {
            greenCells.Add(winningPatterns, score);
            redCells.Add(winningPatterns, score);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void playerPressedButton(int x)
    {
        if (playerPlayed == false)
        {
            currentCell = cells[x];
           
            if (cells[x].GetComponent<Image>().color == Color.red)
            {
                return;
            }
            aIPlayed = false;
            playerPlayed = true;

            cells[x].GetComponent<Image>().color = Color.green;
            playedCells.Add(x);
           
            for (int y = 0; y < cells.Length; y++)
            {
               
                if (cells[y].GetComponent<Image>().color == Color.green)
                {
                    greenScore++;
                    Debug.Log(greenScore);
                    if (greenScore >= 6)
                    {


                        //Check for player winning on rows
                        if (cells[0].GetComponent<Image>().color == Color.green && cells[1].GetComponent<Image>().color == Color.green &&
                            cells[2].GetComponent<Image>().color == Color.green || cells[3].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[5].GetComponent<Image>().color == Color.green || cells[6].GetComponent<Image>().color == Color.green && cells[7].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                            Debug.Log("3 cells");
                        }


                        //Check for player winning on columns
                        if (cells[0].GetComponent<Image>().color == Color.green && cells[3].GetComponent<Image>().color == Color.green &&
                            cells[6].GetComponent<Image>().color == Color.green || cells[1].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[7].GetComponent<Image>().color == Color.green || cells[2].GetComponent<Image>().color == Color.green && cells[5].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                            Debug.Log("3 cells");
                        }

                        //Check for player winning diagonally
                        if (cells[6].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[2].GetComponent<Image>().color == Color.green || cells[0].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                            Debug.Log("3 cells");
                        }

                    }
                }
            }
        } 



        changeButtonColour("player");
        aIPressedButton();
    }

    public void aIPressedButton()
    {

        
        playerPlayed = false;
        yourTurn.SetActive(false);
        //int colourOfRemainingCells = new Color.ToArgb(1f, 1f, 1f, 1f);
        aICells.Clear();
        for(int x = 0; x < cells.Length; x++)
        {
           
            if (!playedCells.Contains(x) & cells[x].GetComponent<Image>().color != Color.red) 
            {

                Debug.Log(playedCells[0].ToString());
                aICells.Add(cells[x]);

            }
           

        }

        //int random = new UnityEngine.Random();

        
        if (aICells.Count > 0)
        {


            int aIMove = UnityEngine.Random.Range(0, aICells.Count);
            //aICells[aIMove].GetComponent<Image>().color = Color.red;
            AIMinMax(aICells, 0, true);
            //GameObject cellToTurnRed = cells.Find(aIMove);
        }
        /* //Ensures AI cannot override already played cell
         if (playedCells.Contains(aIMove)){
             Debug.Log("AI SELECTED PLAYED CELL");
             aIMove = UnityEngine.Random.Range(0, aICells.Count);
             if (!playedCells.Contains(aIMove))
             {

             }

         }
          */

        

        Debug.Log(aICells.Count);
        yourTurn.SetActive(true);
    }

    public void changeButtonColour(string playerOrAI)
    {
        if (playerOrAI == "player")
        {
            //button.GetComponent<Image>().color = Color.green;
        }
    }

    //In this implementation the AI is set as the minimiser
    public float AIMinMax(List<GameObject> availableCells, int depth, bool isMinimising)
    {
        aICellsTried.Clear();
        //Color aiBlank = aICells[x].GetComponent<Image>().color

        if (isMinimising)
        {
            float choice = 0f;
            float bestChoice = Mathf.Infinity;
            for (int x = 0; x < aICells.Count; x++)
            {
                if (!aICellsTried.Contains(x))
                    {
                    Debug.Log(aICellsTried + "AI CELLS");
                    aICellsTried.Add(x);
                 
                    aICells[x].GetComponent<Image>().color = Color.red;
                    //float choice = AIMinMax(availableCells, depth + 1, false);
                    bestChoice = MathF.Min(choice, bestChoice);
                    aICells[x].GetComponent<Image>().color = Color.white;
                    Debug.Log(bestChoice + choice);
              
                if (bestChoice < choice)
                {
                    bestChoice = choice;
                        
                        
                    }
                    aICells[(int)choice].GetComponent<Image>().color = Color.red;
                }
                Debug.Log(bestChoice);
            }
            return (int)bestChoice;
        }
        else {
        
            //float choice = 0f;
            float bestChoice = Mathf.Infinity*-1;
            for (int x = 0; x < aICells.Count; x++)
            {
                float choice = 0f;
                if (playedCells.Contains(x))
                {
                    Debug.Log(aICellsTried + "AI CELLS");
                    playedCells.Add(x);

                    //float choice = AIMinMax(availableCells, depth + 1, true);
                    bestChoice = MathF.Min(choice, bestChoice);
          
                    Debug.Log(bestChoice + choice);

                    if (bestChoice < choice)
                    {
                        bestChoice = choice;


                    }
                    aICells[(int)choice].GetComponent<Image>().color = Color.red;
                }
                Debug.Log(bestChoice);
            }
            return (int)bestChoice;
        }
        return (int)Choice.Empty;




















    }


    public enum Choice
    {
        Red = -1, Empty = 0, Green = 1
    }


}
