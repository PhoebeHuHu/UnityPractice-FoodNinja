using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrail : MonoBehaviour
{
    private Vector3 cursorPos;
    private Vector3 worldPos;
    private float zPos;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        zPos = -Camera.main.transform.position.z;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the position of mouse
        cursorPos = Input.mousePosition;
        cursorPos.z = zPos;//the mousePosition dont have a z pos, so we need to set the z pos
        //transfer the screen pos to 3D world pos
        worldPos = Camera.main.ScreenToWorldPoint(cursorPos);
        //update the cursor trail object's pos make it equal to the mouse pos
        transform.position = worldPos;
        //show the cursor trail only when user press the left mouse button
        if (Input.GetMouseButton(0) && gameManager.isGameActive)
        {
            GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
