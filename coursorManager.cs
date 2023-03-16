using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coursorManager : MonoBehaviour
{
    public Texture2D cursorOpen;
    public Texture2D cursorClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Cursor.SetCursor(cursorClose, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(cursorOpen, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
