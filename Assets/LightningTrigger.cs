using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : MonoBehaviour
{

    public bool movingDown = false;
    public bool movingUp = false;
    public float maxHeight = 16.5f;
    public float minHeight = 6.3f;
    public float speed = 0.01f;
    [SerializeField] private bool doorActivate = false;

    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {

        /*Instantiate(door, door.transform.position, door.transform.rotation);*/
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = door.transform.position;

        if (doorActivate)
        {
            if (p.y >= maxHeight)
            {
                movingDown = true;
            }
            if (p.y <= minHeight)
            {
                movingUp = true;
            }
            doorActivate = false;
        }

        if (movingUp == true)
        {
            p.y += speed;
        }
        if (movingDown == true)
        {
            p.y -= speed;
        }

        if (p.y > maxHeight)
        {
            p.y = maxHeight;
            movingUp = false;
        }
        if (p.y < minHeight)
        {
            p.y = minHeight;
            movingDown = false;
        }

        door.transform.position = p;
    }

    public bool OpenDoor()
    {
        return doorActivate;
        Debug.Log(doorActivate);
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "ThunderAbility")
        {
            doorActivate = true;
        }
    }
    
    public void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "ThunderAbility")
        {
            doorActivate = false;
        }
    }
}
