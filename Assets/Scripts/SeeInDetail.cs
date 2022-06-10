using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeeInDetail : MonoBehaviour, IPointerClickHandler
{
    private GameObject canvas;
    private GameObject zoomShadow;
    private Card card;
    private Image cardImage;

    private float cardScale = 1.2f;
    private float cardScaleIncreased = 1.4f;
    private float cardScaleZoom = 6f;

    private CardPlacing initialPlacing;
    private bool inZoom;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        zoomShadow = canvas.transform.Find("ZoomShadow").gameObject;
        card = GetComponent<Card>();
        cardImage = GetComponent<Image>();
        inZoom = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && (!inZoom) && card.Interactive)
        {
            ActivateZoom();
        }
    }

    public void IncreaseSize()
    {
        if((!inZoom) && card.Interactive)
        {
            transform.localScale = cardScaleIncreased * Vector2.one;
        }
    }

    public void ReturnSize()
    {
        if ((!inZoom) && card.Interactive)
        {
            transform.localScale = cardScale * Vector2.one;
        }
    }

    private void ActivateZoom()
    {
        inZoom = true;
        initialPlacing = transform.parent.GetComponent<CardPlacing>();

        zoomShadow.SetActive(true);
        cardImage.raycastTarget = false;

        transform.SetParent(canvas.transform);
        transform.localPosition = Vector2.zero;
        card.transform.localEulerAngles = 0 * Vector3.forward;
        transform.localScale = cardScaleZoom * Vector2.one;

        zoomShadow.GetComponent<ScreenDarken>().OnScreenClicked += DeactivateZoom;
    }

    private void DeactivateZoom()
    {
        inZoom = false;

        zoomShadow.SetActive(false);
        cardImage.raycastTarget = true;

        initialPlacing.ReturnCardToPosition(card);
        transform.localScale = cardScale * Vector2.one;
    }
}
