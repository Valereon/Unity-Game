using System.Collections.Generic;
using System.Linq;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShipHelm : MonoBehaviour
{
    Vector3 origin;
    GameManager gm;
    ShipCreator shipCreator;
    GridPlacement gridPlacement;

    PlayerMovement playerMovement;

    int thrusterAmount;
    bool isOnShip = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        shipCreator = gm.GetComponent<ShipCreator>();
        gridPlacement = gm.GetComponent<GridPlacement>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        origin = transform.position;

    }
    
    private void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.E) && shipCreator.isShipMode){
            MakeShip();

        }
        else if(Input.GetKeyDown(KeyCode.Q) && shipCreator.isShipMode){
            transform.Rotate(0, 0, -90);
        }else if (Input.GetKeyDown(KeyCode.E) && !shipCreator.isBuildMode && shipCreator.isShipMode){

            BoardShip();
            isOnShip = true;

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
            foreach(Collider2D collider in GetColliders(distance)){
                
            }
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
        return Physics2D.OverlapCircleAll(new Vector3(origin.x, origin.y, origin.z), distance);
    }



    void BoardShip()
    {
        playerMovement.enabled = false;
        playerMovement.gameObject.transform.SetParent(transform);
        playerMovement.gameObject.transform.position = transform.position;
    }

    void FixedUpdate() {
        if(!isOnShip)
            return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        
        GetComponent<Rigidbody2D>().AddForce(direction * thrusterAmount * 5f, ForceMode2D.Force);

        if(Input.GetKey(KeyCode.LeftShift)){
            GetComponent<Rigidbody2D>().AddForce(direction * thrusterAmount * 10f, ForceMode2D.Impulse);
        }
    }
}

    

