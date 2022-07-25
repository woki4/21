using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerHand playerHand;
    public GameController gameController;
    public TextMeshProUGUI scoreText;
    public CardDeck cardDeck;
    public float cardMovingSpeed;

    public Transform cardDeckNewPosition;
    
    private List<Card> _cards = new List<Card>();

    public void StartTakeCards()
    {
        StartCoroutine(TakingCards());
    }

    private IEnumerator TakingCards()
    {
        scoreText.transform.parent.gameObject.SetActive(true);
        cardDeck.transform.position = cardDeckNewPosition.position;
        
        while (GetScore() < 21)
        {
            if (GetScore() >= 17)
            {
                int stopChance = Random.Range(0, 2);

                if (stopChance == 0)
                {
                    break;
                }
            }
            
            TakeCard();
            yield return new WaitForSeconds(1f);
        }
        
        if (GetScore() > 21 || GetScore() < playerHand.GetScore())
        {
            yield return new WaitForSeconds(0.5f);
            gameController.Win();
        }
        else if (GetScore() >= playerHand.GetScore())
        {
            yield return new WaitForSeconds(0.5f);
            gameController.Lose();
        }
    }

    public void TakeCard()
    {
        Card card = cardDeck.GetCard();
        StartCoroutine(MoveCard(card));
    }

    private IEnumerator MoveCard(Card card)
    {
        _cards.Add(card);
        
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
    }

    private int GetScore()
    {
        int score = 0;
        
        for (int i = 0; i < _cards.Count; i++)
        {
            score += _cards[i].value;
        }

        return score;
    }
}
