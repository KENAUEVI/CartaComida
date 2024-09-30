using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Card card;
    private GameObject canvas;
    private HandPlacing playerArea;
    private FieldPlacing dropZone;

    private CardPlacing initialPlacing;
    private bool isOverDropZone;
    private bool isDragging;

    public bool IsDragging { get => isDragging; }

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Game Scene");
        playerArea = canvas.transform.Find("Player Area").GetComponent<HandPlacing>();
        card = GetComponent<Card>();
        isDragging = false;
        isOverDropZone = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Drop_Zone"))
        {
            isOverDropZone = true;
            dropZone = collision.gameObject.GetComponent<FieldPlacing>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Drop_Zone"))
        {
            isOverDropZone = false;
            dropZone = null;
        }
    }


    public void StartDrag()
    {
        if(card.Interactive && card.Grabbable)
        {
            isDragging = true;
            initialPlacing = transform.parent.GetComponent<CardPlacing>();

            transform.SetParent(canvas.transform);
            card.transform.localEulerAngles = 0 * Vector3.forward;
        }
    }

    public void EndDrag()
    {
        if(card.Interactive && card.Grabbable)
        {
            isDragging = false;

            if (isOverDropZone)
            {
                if (initialPlacing != dropZone)
                {
                    initialPlacing.RemoveCard(card);
                    FreeDropZoneIfOccupied();
                    dropZone.AddCard(card);
                }
                else
                {
                    initialPlacing.ReturnCardToPosition(card);
                }
            }
            else
            {
                if (initialPlacing != playerArea)
                {
                    initialPlacing.RemoveCard(card);
                    playerArea.AddCard(card);
                }
                else
                {
                    initialPlacing.ReturnCardToPosition(card);
                }
            }
        }
    }

    private void FreeDropZoneIfOccupied()
    {
        Card extraCard = null;
        if (card is Spice)
        {
            extraCard = dropZone.SpiceCard;
        }
        else if (card is Monster)
        {
            extraCard = dropZone.MonsterCard;
        }

        if (extraCard != null)
        {
            dropZone.RemoveCard(extraCard);
            playerArea.AddCard(extraCard);
        }
    }
}
