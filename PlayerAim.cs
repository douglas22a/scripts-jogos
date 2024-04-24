using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Rigidbody2D playerRb;

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - playerRb.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        playerRb.rotation = angle;


    }

    
}
