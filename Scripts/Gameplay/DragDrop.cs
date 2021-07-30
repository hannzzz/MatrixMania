using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool canMove;
    private bool dragging;
    Collider2D thisCollider;
    Vector2 mousePos;
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        canMove = false;
        dragging = false;
    }

    void Update()
    {
        //Get mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) //On Click
        { 
            if (thisCollider == Physics2D.OverlapPoint(mousePos)) //Mouse on collider
            { 
                canMove = true;
            }
            else
            {
                canMove = false;
            }

            if (canMove)
            {
                dragging = true;
            }
        }

        if (dragging)
        { //Drag
            this.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        { //Drop
            canMove = false;
            dragging = false;
        }
    }

}
