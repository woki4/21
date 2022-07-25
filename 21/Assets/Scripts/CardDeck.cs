using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    public List<CardInfo> cardsInfos;
    public Card cardPrefab;
    public Button button;

    private void Start()
    {
        ShuffleCards();
    }

    private void ShuffleCards()
    {
        int last = cardsInfos.Count - 1;
        
        for (int i = 0; i < last; ++i) 
        {
            int random = Random.Range(i, cardsInfos.Count);
            (cardsInfos[i], cardsInfos[random]) = (cardsInfos[random], cardsInfos[i]);
        }
    }

    public Card GetCard()
    {
        Card card = Instantiate(cardPrefab, transform.parent);
        card.transform.position = transform.position;

        CardInfo cardInfo = cardsInfos[cardsInfos.Count - 1];
        cardsInfos.Remove(cardInfo);
        
        card.Initialize(cardInfo);

        return card;
    }

    public void ActivateButton(bool activate)
    {
        button.enabled = activate;
    }
}