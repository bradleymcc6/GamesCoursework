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
    public GameObject[] cells;
    public GameObject currentCell;
    public GameObject wellDone;
    bool gameover;

    List<int> playedCells = new List<int>();
    List<GameObject> aICells = new List<GameObject>();
    List<int> aICellsTried = new List<int>();
    int greenScore;


    // Start is called before the first frame update
    void Start()
    {
        bool playerPlayed = false;
        bool aIplayed = false;
        cells = GameObject.FindGameObjectsWithTag("Cell");
        int score = 0;
        greenScore = 0;
     
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
                    
                    if (greenScore >= 6)
                    {


                        //Check for player winning on rows
                        if (cells[0].GetComponent<Image>().color == Color.green && cells[1].GetComponent<Image>().color == Color.green &&
                            cells[2].GetComponent<Image>().color == Color.green || cells[3].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[5].GetComponent<Image>().color == Color.green || cells[6].GetComponent<Image>().color == Color.green && cells[7].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                         
                            wellDone.SetActive(true);
                            yourTurn.SetActive(false);
                            gameover = true;
                        }


                        //Check for player winning on columns
                        if (cells[0].GetComponent<Image>().color == Color.green && cells[3].GetComponent<Image>().color == Color.green &&
                            cells[6].GetComponent<Image>().color == Color.green || cells[1].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[7].GetComponent<Image>().color == Color.green || cells[2].GetComponent<Image>().color == Color.green && cells[5].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                            
                            wellDone.SetActive(true);
                            yourTurn.SetActive(false);
                            gameover = true;
                        }

                        //Check for player winning diagonally
                        if (cells[6].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[2].GetComponent<Image>().color == Color.green || cells[0].GetComponent<Image>().color == Color.green && cells[4].GetComponent<Image>().color == Color.green &&
                            cells[8].GetComponent<Image>().color == Color.green)
                        {
                           
                            wellDone.SetActive(true);
                            yourTurn.SetActive(false);
                            gameover = true;
                        }

                    }
                }
            }
        }



       
        aIPressedButton();
    }

    public void aIPressedButton()
    {
        if (gameover != true)
        {
        playerPlayed = false;
        yourTurn.SetActive(false);
     
        aICells.Clear();
        for (int x = 0; x < cells.Length; x++)
        {

            if (!playedCells.Contains(x) & cells[x].GetComponent<Image>().color != Color.red)
            {

                aICells.Add(cells[x]);

            }


        }

            if (aICells.Count > 0)
            {


                int aIMove = UnityEngine.Random.Range(0, aICells.Count);
                AIMinMax(aICells, 0, true);
            }



       
        yourTurn.SetActive(true);
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
                    
                    aICellsTried.Add(x);
                 
                    aICells[x].GetComponent<Image>().color = Color.red;
   
                    bestChoice = MathF.Min(choice, bestChoice);
                    aICells[x].GetComponent<Image>().color = Color.white;
                    
              
                if (bestChoice < choice)
                {
                    bestChoice = choice;
                         
                 }
                    aICells[(int)choice].GetComponent<Image>().color = Color.red;
                }
                
            }
            return (int)bestChoice;
        }
        else {
        
         
            float bestChoice = Mathf.Infinity*-1;
            for (int x = 0; x < aICells.Count; x++)
            {
                float choice = 0f;
                if (playedCells.Contains(x))
                {
                    
                    playedCells.Add(x);

                  
                    bestChoice = MathF.Min(choice, bestChoice);
          
                  

                    if (bestChoice < choice)
                    {
                        bestChoice = choice;


                    }
                    aICells[(int)choice].GetComponent<Image>().color = Color.red;
                }
                
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
