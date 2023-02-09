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

    public float LedgeGrabForwardPos; //the position in front of the player where we check for ledges
    public float LedgeGrabUpwardsPos;//the position in above of the player where we check for ledges
    public float LedgeGrabDistance; //the distance the ledge can be from our raycast before we grab it (this is projects from the top of the wall grab position, downwards

    public LayerMask FloorLayers; //what layers we can stand on
    public LayerMask WallLayers;  //what layers we can wall run on
    public LayerMask RoofLayers; //what layers we cannot stand up under (for crouching
    public LayerMask LedgeGrabLayers; //what layers we will grab onto
    
    public bool CheckFloor(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * bottomOffset);
        
        if(Physics.CheckSphere(pos,FloorCheckRadius,FloorLayers))
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
       
        if (Physics.CheckSphere(pos, WallCheckRadius, WallLayers))
        {
            // there is ground below us
            return true;
        }
        return false;
    } 
    public bool CheckRoof(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * upOffset);
        
        if (Physics.CheckSphere(pos, RoofCheckRadius, RoofLayers))
        {
            // there is ground below us
            return true;
        }
        return false;
    }
    public Vector3 CheckLedges()
    {
        Vector3 RayPos = transform.position + (transform.forward * LedgeGrabForwardPos) + (transform.up * LedgeGrabUpwardsPos);

        RaycastHit hit;
        if (Physics.Raycast(RayPos, -transform.up, out hit, LedgeGrabDistance, LedgeGrabLayers))
            return hit.point;


        return Vector3.zero;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position + (-transform.up * bottomOffset);
        Gizmos.DrawWireSphere(pos, FloorCheckRadius);

        Gizmos.color = Color.red;
        pos = transform.position + (transform.forward * frontOffset);
        Gizmos.DrawWireSphere(pos, WallCheckRadius);

        //roof check
        Gizmos.color = Color.green;
        Vector3 Pos3 = transform.position + (transform.up * upOffset);
        Gizmos.DrawSphere(Pos3, RoofCheckRadius);

        //Ledge check
        Gizmos.color = Color.black;
        Vector3 Pos4 = transform.position + (transform.forward * LedgeGrabForwardPos) + (transform.up * LedgeGrabUpwardsPos);
        Gizmos.DrawLine(Pos4, Pos4 + (-transform.up * LedgeGrabDistance));
    }

}
