using System.Collections;

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    private float ScreenCenterX;
    private Rigidbody2D rigi;

    private bool isGrounded = true;
    private bool isJumping = false;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        ScreenCenterX = Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (touch.phase == TouchPhase.Began)
            {
                if (GetComponent<Collider2D>().OverlapPoint(worldPoint) && isGrounded)
                {
                    Debug.Log("Jumped");
                    rigi.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                    isGrounded = false;
                    isJumping = true;
                }
                 else if (touch.position.x > ScreenCenterX && isJumping == false)
                {
                    //Move Right
                    Debug.Log("Moved Right");
                    rigi.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                    //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                }
                else if(touch.position.x < ScreenCenterX && isJumping == false)
                {
                    //Move Left
                    Debug.Log("Moved Left");
                    rigi.AddForce(Vector2.right * -moveSpeed, ForceMode2D.Impulse);
                    //transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
                }
                
                
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
