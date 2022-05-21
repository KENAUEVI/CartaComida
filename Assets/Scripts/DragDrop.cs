using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Drop_Zone;

    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject drop_Zone;
    private bool isOverDropZone;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        Drop_Zone = GameObject.Find("Drop_Zone"); // apaga depois essa linha
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        drop_Zone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        drop_Zone = null;
    }

    // a carta está sendo arrastada
    public void StartDrag()
    {
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    // SetParent serve pra modificar quem é pai de quem, consequentemente o que fica em cima do que
    // a carta não está sendo arrastada
    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(drop_Zone.transform, false);
        }
        else 
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    // Update is called once per frame
    // Atualizando a posição inicial da carta
    void Update()
    {
        if (isDragging) 
        {
            transform.position = new Vector2(Input.mousePosition.x,  Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
