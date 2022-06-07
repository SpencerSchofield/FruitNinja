using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSceneController : MonoBehaviour
{
    public const int gridRows = 2; //no of card rows
    public const int gridCols = 4; //no of card columns
    public const float offsetX = 4f;
    public const float offsetY = 5f;
    private MainCard firstRevealed; //first card selected
    private MainCard secondRevealed; //second card selected

    [SerializeField] private MainCard originalCard; //the original card in the scene
    [SerializeField] private Sprite[] images; // the different fruits under the cards
    [SerializeField] private TextMesh scoreLabel; // the score label

    private void Start() {
        Vector3 startPos = originalCard.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3}; //indexes of images
        numbers = ShuffleArray(numbers); //shuffles index numbers so the cards are not ordered

        for(int i = 0; i < gridCols; i++) //sets out cards on screen
        {
            for(int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x; //sets x position
                float posY = (offsetY * j) + startPos.y; //sets y position
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    /* 
    * method that shuffles integers in a given array 
    */
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[]; //clones original array
        for(int i = 0; i < newArray.Length; i++) //shuffles array values around randomly
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray; //returns new shuffled array
    }

    /* 
    * method to prevent multiple cards being revealed
    */
    public bool canReveal
    {
        get { return secondRevealed == null; } 
    }

    /*  
    * sets values of revealed cards to respective variables
    */
    public void CardRevealed(MainCard card)
    {
        if(firstRevealed == null) //if null no cards have been flipped before
        {
            firstRevealed = card;
        }
        else //sets value of card to the second variable
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch()); //checks to see if they both match
        }
    }

    /* 
    * checks to see if the two revealed cards match and hides them if they dont
    */
    private IEnumerator CheckMatch()
    {
        if(firstRevealed.id == secondRevealed.id) //if theres a match add to score
        {
            SoundManagerScript.PlaySound("CardMatch");
        }
        else //waits and then hides cards again
        {
            yield return new WaitForSeconds(0.5f);

            firstRevealed.HideCard();
            secondRevealed.HideCard();
        }

        //reset values back to null for next turn
        firstRevealed = null;
        secondRevealed = null;

    }

    /* 
    * restart method for use with button, reloads the scene
    */
    public void Restart()
    {
        SceneManager.LoadScene("GameScene_cardGame");
    }

    /* 
    * menu method for use with button, loads the menu scene
    */
    public void Menu()
    {
        SceneManager.LoadScene("GameHUD_menu");
    }
}
