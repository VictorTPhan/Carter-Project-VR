using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceController : MonoBehaviour
{
    public GameObject item;
    public Camera cam;
    public KeyCode placeKey = KeyCode.E;
    public KeyCode rotateKey = KeyCode.R;
    public GameObject itemHolder;
    private int rotatePos = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Player").transform.GetChild(0).GetComponent<Camera>();
        itemHolder = GameObject.Find("ItemHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null && Input.GetKeyDown(placeKey))
        {
            PlaceItem();
        }

        if (item && Input.GetKeyDown(placeKey))
        {
            PickUpItem();
        }

        if (item && Input.GetKeyDown(rotateKey))
        {
            RotateItem();
        }
    }
    void PlaceItem()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit == this.gameObject && itemHolder.transform.childCount == 1)
            {
                Transform placedItem = itemHolder.transform.GetChild(0);
                item = placedItem.gameObject;
                placedItem.GetComponent<PickUpController>().Drop();
                placedItem.GetComponent<Rigidbody>().isKinematic = true;
                placedItem.SetParent(objectHit.transform);
                placedItem.transform.position = objectHit.transform.position;
                placedItem.transform.rotation = Quaternion.identity;
                if (placedItem.GetComponent<PipeType>().pipeType == PipeType.Type.Tpipe)
                {
                    placedItem.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
            }
        }
    }
    void PickUpItem()
    {
        if (transform.childCount == 0)
        {
            foreach (BallGenerator bg in item.GetComponentsInChildren<BallGenerator>())
            {
                Debug.Log("asd");
                bg.output = false;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            item = null;
            rotatePos = 0;
        }
    }
    void RotateItem()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit == transform.GetChild(0).gameObject)
            {
                if(objectHit.GetComponent<PipeType>().pipeType == PipeType.Type.Lpipe)
                {
                    RotateLPipe();
                } 
                else if (objectHit.GetComponent<PipeType>().pipeType == PipeType.Type.pipe)
                {
                    RotateNormalPipe();
                } 
                else if (objectHit.GetComponent<PipeType>().pipeType == PipeType.Type.Tpipe)
                {
                    RotateTPipe();
                }
            }
        }
    }

    void RotateLPipe()
    {
        switch (rotatePos)
        {
            case 0:
                transform.rotation = Quaternion.identity;
                rotatePos++;
                break;
            case 1:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 90, 90));
                rotatePos++;
                break;
            case 2:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 90, -90));
                rotatePos++;
                break;
            default:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                rotatePos = 0;
                break;
        }
    }

    void RotateNormalPipe()
    {
        switch (rotatePos)
        {
            case 0:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 90, 90));
                rotatePos++;
                break;
            default:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                rotatePos = 0;
                break;
        }
    }

    void RotateTPipe()
    {
        switch (rotatePos)
        {
            case 0:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 90, -90));
                rotatePos++;
                break;
            case 1:
                transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                rotatePos++;
                break;
            case 2:
                transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                rotatePos++;
                break;
            default:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                rotatePos = 0;
                break;
        }
    }
}
