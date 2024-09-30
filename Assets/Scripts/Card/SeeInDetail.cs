using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeeInDetail : MonoBehaviour, IPointerClickHandler
{
    private GameObject canvas;
    private GameObject zoomUI; 
    private ScreenDarken zoomShadow;
    private Transform cardPosition;
    private Text cardNameText;
    private Text cardInfoText;

    private Card card;
    private Image cardImage;
    private DragDrop cardDragDrop;

    private CardPlacing initialPlacing;
    private bool inZoom;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Game Scene");
        zoomUI = canvas.transform.Find("Zoom UI").gameObject;
        zoomShadow = zoomUI.transform.Find("Zoom Shadow").GetComponent<ScreenDarken>();
        cardPosition = zoomUI.transform.Find("Card Position");
        cardNameText = zoomUI.transform.Find("Card Name").GetComponent<Text>();
        cardInfoText = zoomUI.transform.Find("Card Info").GetComponent<Text>();
        card = GetComponent<Card>();
        cardImage = GetComponent<Image>();
        cardDragDrop = GetComponent<DragDrop>();
        inZoom = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && !inZoom && card.Interactive && !cardDragDrop.IsDragging)
        {
            ActivateZoom();
        }
    }

    public void IncreaseSize()
    {
        if (!inZoom && card.Interactive)
        {
            if (card.Battling)
            {
                transform.localScale = Card.cardScaleBattlingIncreased * Vector2.one;
            }
            else
            {
                transform.localScale = Card.cardScaleIncreased * Vector2.one;
            }
        }
    }

    public void ReturnSize()
    {
        if (!inZoom && card.Interactive)
        {
            if (card.Battling)
            {
                transform.localScale = Card.cardScaleBattling * Vector2.one;
            }
            else
            {
                transform.localScale = Card.cardScale * Vector2.one;
            }
        }
    }

    private void ActivateZoom()
    {
        inZoom = true;
        initialPlacing = transform.parent.GetComponent<CardPlacing>();

        zoomUI.SetActive(true);
        cardImage.raycastTarget = false;

        transform.SetParent(canvas.transform);
        transform.localPosition = cardPosition.localPosition;
        card.transform.localEulerAngles = 0 * Vector3.forward;
        transform.localScale = Card.cardScaleZoom * Vector2.one;

        cardNameText.text = card.GetCardName();
        cardInfoText.text = card.GetCardExplanationText();

        zoomShadow.OnScreenClicked += DeactivateZoom;
    }

    private void DeactivateZoom()
    {
        inZoom = false;

        zoomUI.SetActive(false);
        cardImage.raycastTarget = true;

        initialPlacing.ReturnCardToPosition(card);
        transform.localScale = Card.cardScale * Vector2.one;
    }
}
