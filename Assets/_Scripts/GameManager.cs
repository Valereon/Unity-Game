using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    ShipCreator shipCreator;

    private void Start() {
        shipCreator = GetComponent<ShipCreator>();
        

    }

    // Update is called once per frame
    void Update()
    {   
        if(shipCreator.isBuildMode || shipCreator.isShipMode){
            player.gameObject.SetActive(false);
        }else{
            player.gameObject.SetActive(true);
        }

    }




}
