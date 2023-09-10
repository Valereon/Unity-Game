using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ShipHelm : MonoBehaviour
{
    Vector3 origin;
    GameManager gm;
    GridPlacement gridPlacement;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridPlacement = gm.GetComponent<GridPlacement>();
        origin = transform.position;

    }
    
    private void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.E)){
            MakeShip();

        }
        if(Input.GetKeyDown(KeyCode.Q)){
            transform.Rotate(0, 0, -90);
        }
    }


    // GameObject CheckIfShipPart(){
    //     GameObject[] placedObjects = GameObject.FindGameObjectsWithTag("ShipPart");
    //     foreach (GameObject obj in placedObjects)
    //     {
    //         if (obj.transform.position == origin)
    //         {
    //             return obj;
    //         }
    //     }
    //     return null;
    // }



    void MakeShip(){
        

        float distance = 0.5f;
        List<Collider2D> colliders = new();
        while(GetColliders(distance).Length > 0){
            GetColliders(distance).ToList().ForEach(colliders.Add);

            try{
                colliders.RemoveAt(colliders.IndexOf(GetComponent<Collider2D>()));
            }catch (System.ArgumentOutOfRangeException){
                Debug.Log("No collider to remove");
            }
            distance += 0.5f;
        }   


        foreach(Collider2D collider in colliders){

            gridPlacement.placedObjects.Remove(collider.gameObject);
            collider.transform.SetParent(gameObject.transform);
            gameObject.GetComponent<Rigidbody2D>().mass += collider.GetComponent<Rigidbody2D>().mass;
            Destroy(collider.GetComponent<Rigidbody2D>());
        }
    }



    Collider2D[] GetColliders(float distance){
        return Physics2D.OverlapCircleAll(new Vector3(origin.x + distance, origin.y + distance, origin.z), distance);
    }
}

    

