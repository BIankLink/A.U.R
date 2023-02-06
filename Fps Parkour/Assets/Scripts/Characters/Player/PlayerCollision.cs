using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float FloorCheckRadius; //how large the detection for the floors is
    public float bottomOffset; //offset from player centre
    public float WallCheckRadius; //how large the detection for the walls is
    public float frontOffset; //offset from the players centre 
    public float RoofCheckRadius; //the amount we check before standing up 
    public float upOffset; //offset upwards

    //public float LedgeGrabForwardPos; //the position in front of the player where we check for ledges
    //public float LedgeGrabUpwardsPos;//the position in above of the player where we check for ledges
    //public float LedgeGrabDistance; //the distance the ledge can be from our raycast before we grab it (this is projects from the top of the wall grab position, downwards

    public LayerMask FloorLayers; //what layers we can stand on
    public LayerMask WallLayers;  //what layers we can wall run on
    public LayerMask RoofLayers; //what layers we cannot stand up under (for crouching
    //public LayerMask LedgeGrabLayers; //what layers we will grab onto

    public bool CheckFloor(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * bottomOffset);
        Collider[] colHit= Physics.OverlapSphere(pos,FloorCheckRadius,FloorLayers);
        if(colHit.Length > 0)
        {
            // there is ground below us
            return true;
        }
        return false;
    }

    //Perform same function only for Wall Layers
    public bool CheckWalls(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * frontOffset);
        Collider[] colHit = Physics.OverlapSphere(pos, WallCheckRadius, WallLayers);
        if (colHit.Length > 0)
        {
            // there is ground below us
            return true;
        }
        return false;
    } 
    public bool CheckRoof(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * upOffset);
        Collider[] colHit = Physics.OverlapSphere(pos, RoofCheckRadius, RoofLayers);
        if (colHit.Length > 0)
        {
            // there is ground below us
            return true;
        }
        return false;
    } 
    

}
