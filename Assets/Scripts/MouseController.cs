using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private SpriteRenderer cursor;
    public bool onMenu;
    public Sprite defaultCursor;
    public Sprite aimCursor;
    public Sprite shootingCursor;

    void Start()
    {
        Cursor.visible = false;
        cursor = GetComponent<SpriteRenderer>();
        onMenu = true;

        //MOCK
        cursor.sprite = aimCursor;
    }

    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;

        // cursor.sprite = aimCursor;

            if (Input.GetMouseButtonDown(0))
            {
                cursor.sprite = shootingCursor;
            }else if(Input.GetMouseButtonUp(0))
            {
                cursor.sprite = aimCursor;
            }
        
    }
}
