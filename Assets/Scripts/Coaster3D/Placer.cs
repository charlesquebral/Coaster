using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public float lerpSpeed = 10f;
    public float rotSpeed = 400f;
    public GameObject objectToPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            RotateMode();
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            LiftMode();
        }
        else
        {
            PlaceMode();
        }
    }

    void RotateMode()
    {
        if (objectToPlace != null)
        {
            objectToPlace.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * Time.deltaTime * -rotSpeed, 0);
        }
    }

    void LiftMode()
    {
        if (objectToPlace != null)
        {
            objectToPlace.transform.Translate(0, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * (rotSpeed / 3), 0);
        }
    }

    void PlaceMode()
    {
        if (objectToPlace != null)
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
            {
                return;
            }

            Vector3 newPos = new Vector3(hit.point.x, objectToPlace.transform.position.y, hit.point.z);
            objectToPlace.transform.position = Vector3.Lerp(objectToPlace.transform.position, newPos, lerpSpeed * Time.deltaTime);
        }
    }
}
