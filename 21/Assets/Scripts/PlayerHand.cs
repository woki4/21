using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameController gameController;
    public GameObject endTurnButton;
    public TextMeshProUGUI scoreText;
    public CardDeck cardDeck;
    public float cardMovingSpeed;
    
    private List<Card> _cards = new List<Card>();

    public void TakeCard()
    {
        Card card = cardDeck.GetCard();
        StartCoroutine(MoveCard(card));
    }

    private IEnumerator MoveCard(Card card)
    {
        _cards.Add(card);
        
        endTurnButton.SetActive(false);
        cardDeck.ActivateButton(false);

        float distanceToCard = Vector3.Distance(card.transform.position, transform.position);

        while (distanceToCard > 0.1f)
        {
            card.transform.position = Vector3.Lerp(card.transform.position, transform.position, cardMovingSpeed * Time.deltaTime);
            distanceToCard = Vector3.Distance(card.transform.position, transform.position);

            yield return null;
        }

        int score = GetScore();
        scoreText.text = "Баллы: " + score;
        card.transform.SetParent(transform);

        if (score == 21)
        {
            yield return new WaitForSeconds(0.5f);
            gameController.Win();
        }
        else if (score > 21)
        {
            yield return new WaitForSeconds(0.5f);
            gameController.Lose();
        }
        else
        {
            endTurnButton.SetActive(true);
            cardDeck.ActivateButton(true);
        }
    }

    public int GetScore()
    {
        int score = 0;
        
        for (int i = 0; i < _cards.Count; i++)
        {
            score += _cards[i].value;
        }

        return score;
    }
}
