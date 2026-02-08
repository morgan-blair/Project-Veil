using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 targetPos;
    public bool camFollowPlayer;
    public Transform targetPosPlayer;
    public float zoom;
    private Camera cameraComp;
    public float camSpeed;
    void Start()
    {
        cameraComp = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camFollowPlayer)
        {
            targetPos = targetPosPlayer.position;
        }
        if (transform.position.x < targetPos.x)
        {
            transform.position = new Vector3(transform.position.x + camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x > targetPos.x)
            {
                transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            }
        }
        if (transform.position.x > targetPos.x)
        {
            transform.position = new Vector3(transform.position.x - camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x < targetPos.x)
            {
                transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            }
        }
        if (transform.position.y < targetPos.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + camSpeed * Time.deltaTime, transform.position.z);
            if (transform.position.y > targetPos.y)
            {
                transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);
            }
        }
        if (transform.position.y > targetPos.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - camSpeed * Time.deltaTime, transform.position.z);
            if (transform.position.y < targetPos.y)
            {
                transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            }
        }
        cameraComp.orthographicSize = zoom;

    }
}
