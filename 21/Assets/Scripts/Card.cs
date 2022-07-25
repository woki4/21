using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image image;

    public int value => _cardInfo.value;
    private CardInfo _cardInfo;

    public void Initialize(CardInfo cardInfo)
    {
        _cardInfo = cardInfo;
        image.sprite = cardInfo.sprite;
    }
}
