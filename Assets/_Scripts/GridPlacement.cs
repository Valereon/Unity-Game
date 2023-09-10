using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;


public class GridPlacement : MonoBehaviour
{
    public GameObject[] primitves;
    GameObject objToPlace;
    int selectedObj = 0;
    int oldSelectedObj = 0;
    public string objTag = "PlacedObject";

    public List<GameObject> placedObjects = new List<GameObject>();


    // Start is called before the first frame update
    void OnDisable() {
        Destroy(objToPlace);
    }

    void OnEnable() {
        objToPlace = InstatiateSquare(0, 0, selectedObj);
    }





    // Update is called once per frame
    void Update()
    {
        
        objToPlace = SnapToGrid(objToPlace);
        selectedObj = SelectNewObject();
        objToPlace = ChangeObjects(objToPlace);

        if (Input.GetMouseButtonDown(0))
        {
            if(!IsSpaceFree(objToPlace.transform.position))
                return;

            PlaceObject(objToPlace);
        }
    }





    GameObject InstatiateSquare(int x, int y, int Obj)
    {
        GameObject newObj = null;
        if(Obj == 0)
            newObj = Instantiate(primitves[0], transform);
        else if(Obj == 1)
            newObj = Instantiate(primitves[1], transform);
        else if(Obj == 2)
            newObj = Instantiate(primitves[2], transform);
        else if(Obj == 3)
            newObj = Instantiate(primitves[3], transform);

        newObj.transform.position = new Vector3(x, y, 0);
        newObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return newObj;
    }

    void PlaceObject(GameObject Obj)
    {
        GameObject obj = Instantiate(Obj, objToPlace.transform.position, Quaternion.identity);
        obj.tag = objTag;
        obj.AddComponent<Rigidbody2D>();
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        if(obj.name == "Capsule(Clone)(Clone)")
        {
            obj.AddComponent<BoxCollider2D>();
            obj.AddComponent<ShipHelm>();
            obj.GetComponent<BoxCollider2D>().isTrigger = true;

        }
            
        else
            obj.AddComponent<BoxCollider2D>();
        

        placedObjects.Add(obj);
        

    }

    int SelectNewObject()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            return 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            return 1;
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            return 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            return 3;
        return selectedObj;
    }

    GameObject ChangeObjects(GameObject objToPlace)
    {
        if(selectedObj != oldSelectedObj)
        {
            oldSelectedObj = selectedObj;
            Destroy(objToPlace);
            objToPlace = InstatiateSquare(0, 0, selectedObj);
        }
        return objToPlace;
    }


    GameObject SnapToGrid(GameObject objToPlace)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 gridPoint = new Vector3(Mathf.RoundToInt(worldPoint.x * 2f), Mathf.RoundToInt(worldPoint.y * 2f), 0);
        objToPlace.transform.position = gridPoint / 2f;
        return objToPlace;
    }




    bool IsSpaceFree(Vector3 gridPoint)
    {


        Collider2D[] colliders = Physics2D.OverlapCircleAll(gridPoint, 0.1f);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "PlacedObject" || collider.gameObject.tag == "ShipPart")
                return false;
        }
        return true;


        // GameObject[] placedObjects = GameObject.FindGameObjectsWithTag("PlacedObject");
        // GameObject[] shipParts = GameObject.FindGameObjectsWithTag("ShipPart");
        // GameObject[] allObjects = placedObjects.Concat(shipParts).ToArray();

        // foreach(GameObject obj in allObjects){
        //     if(obj.transform.position == gridPoint){
        //         return false;
        //     }
        // }
        // return true;

        // foreach (GameObject obj in placedObjects)
        // {
        //     if (obj.transform.position == gridPoint)
        //     {
        //         return false;
        //     }
        // }
        // return true;
    }

    public void FreezeDuringBuilding()
    {
        foreach (GameObject obj in placedObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnfreezeAfterBuilding()
    {
        foreach (GameObject obj in placedObjects)
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

}