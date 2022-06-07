using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private CardSceneController controller; //card scene controller
    [SerializeField] private GameObject cardBack; //back of card

    /*  
    * method for when a card is pressed
    */
    public void OnMouseDown() 
    {
        if (cardBack.activeSelf && controller.canReveal) //checks if the card is hidden and is able to be revealed
        {
            cardBack.SetActive(false); //hides back of card
            controller.CardRevealed(this); //passes through this card to the controller method
            SoundManagerScript.PlaySound("CardFlip");
        }
    }

    private int _id;

    public int id 
    {
        get { return _id; }
    }

    /*  
    * method to change sprite under card based off the id passed in
    */
    public void ChangeSprite(int id, Sprite image) 
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image; //gets sprite renderer and changes sprite
    }

    /*  
    * method that activates the card to hide the fruit
    */
    public void HideCard()
    {
        cardBack.SetActive(true);
    }
}
