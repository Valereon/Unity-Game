using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipCreator : MonoBehaviour
{
    public bool isBuildMode = false;
    public bool isShipMode = false;
    GridPlacement gridPlacement;


    TextMeshProUGUI modeText;
    // Start is called before the first frame update
    void Start()
    {
        gridPlacement = GetComponent<GridPlacement>();
        modeText = GameObject.Find("Mode").GetComponent<TextMeshProUGUI>();
        UpdateModeText("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBuildMode();
        if(isBuildMode || isShipMode){
            gridPlacement.FreezeDuringBuilding();
        }
        else{
            gridPlacement.UnfreezeAfterBuilding();
        }
    }



    void UpdateModeText(string mode)
    {
        modeText.text = "Mode: " + mode;
    }

    void ChangeBuildMode(){
        if(isBuildMode && Input.GetKeyDown(KeyCode.B) && isShipMode == false){
            isShipMode = !isShipMode;
            isBuildMode = !isBuildMode;
            gridPlacement.objTag = "ShipPart";
            UpdateModeText("Ship");
        }
        else if(isShipMode && Input.GetKeyDown(KeyCode.B)){
            isShipMode = !isShipMode;
            gridPlacement.enabled = isBuildMode;
            UpdateModeText("Player");
        }
        else if(Input.GetKeyDown(KeyCode.B)){
            isBuildMode = !isBuildMode;
            gridPlacement.enabled = isBuildMode;
            gridPlacement.objTag = "PlacedObject";

            UpdateModeText("Build");
        }

    }

}
