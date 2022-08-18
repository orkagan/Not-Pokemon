using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Vector3 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            transform.position.x + Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
            transform.position.y + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        //mousePos = Input.mousePosition;
        //transform.rotation = Quaternion.Euler(new Vector2(0,Mathf.Atan2(mousePos.x,mousePos.y)));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Haven't added a jump yet.");
        }
    }
}
