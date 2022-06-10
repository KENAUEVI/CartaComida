using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private GameObject canvas;
    private CardPlacing playerArea;
    private Card card;

    private CardPlacing dropZone;
    private CardPlacing initialPlacing;
    private bool isDragging;
    private bool isOverDropZone;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        playerArea = canvas.transform.Find("Player Area").GetComponent<CardPlacing>();
        card = GetComponent<Card>();
        isDragging = false;
        isOverDropZone = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Drop_Zone"))
        {
            isOverDropZone = true;
            dropZone = collision.gameObject.GetComponent<CardPlacing>();
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

    // a carta está sendo arrastada
    public void StartDrag()
    {
        if(card.Interactive)
        {
            isDragging = true;
            initialPlacing = transform.parent.GetComponent<CardPlacing>();

            transform.SetParent(canvas.transform);
            card.transform.localEulerAngles = 0 * Vector3.forward;
        }
    }

    // SetParent serve pra modificar quem é pai de quem, consequentemente o que fica em cima do que
    // a carta não está sendo arrastada
    public void EndDrag()
    {
        if(card.Interactive)
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
            extraCard = ((FieldPlacing)dropZone).SpiceCard;
        }
        else if (card is Monster)
        {
            extraCard = ((FieldPlacing)dropZone).MonsterCard;
        }

        if (extraCard != null)
        {
            dropZone.RemoveCard(extraCard);
            playerArea.AddCard(extraCard);
        }
    }

    // Update is called once per frame
    // Atualizando a posição inicial da carta
    private void Update()
    {
        if (isDragging) 
        {
            transform.position = new Vector2(Input.mousePosition.x,  Input.mousePosition.y);
        }
    }
}
