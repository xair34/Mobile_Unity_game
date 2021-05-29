using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Borders
{

    public float minXOffset = 1f, maxXOffset = 1f, minYOffset = 1f, maxYOffset = 1f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour
{
    // setup borders so player doesn't go off screen
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true;

    public static PlayerMoving instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // set player's movement area based on screen size
    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();
    }

    private void Update()
    {
        // mouse control
        if (controlIsActive)
        {

            if (Input.GetMouseButton(0))    
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); 
                mousePosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
            }

            // touch screen control
            if (Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),Mathf.Clamp(transform.position.y,borders.minY, borders.maxY),0);
        }
    }

   // player's moving area based on screen size
    void ResizeBorders()
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}
