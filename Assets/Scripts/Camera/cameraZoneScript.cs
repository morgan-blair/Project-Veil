using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 dir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveCamera(dir);
        }
    }


    private void moveCamera(Vector2 movementVect)
    {
        cameraScript cameraCode = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraScript>();
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float camOffsetX = 0;
        float camOffsetY = 0;
        float moveCharAmountX = 0;
        float moveCharAmountY = 0;
        if (movementVect.x != 0)
        {
            moveCharAmountX = 2.5f;
            camOffsetX = 27;
        }
        if (movementVect.y != 0)
        {
            moveCharAmountY = 2.5f;
            camOffsetY = 16;
        }
        cameraCode.targetPos = new Vector2(cameraCode.transform.position.x + camOffsetX * movementVect.x, cameraCode.transform.position.y + camOffsetY * movementVect.y);
        playerPos.position = new Vector3(playerPos.transform.position.x + moveCharAmountX * movementVect.x, playerPos.transform.position.y + moveCharAmountY * movementVect.y, playerPos.transform.position.z);

    }



}
