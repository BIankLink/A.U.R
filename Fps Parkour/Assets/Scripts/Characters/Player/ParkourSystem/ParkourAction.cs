using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ParkourAction",menuName ="Parkour/Parkour Action")]
public class ParkourAction : ScriptableObject
{
    [SerializeField] string animName;
    [SerializeField] string obstacleTag;

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    [field:SerializeField]public bool RotateToObstacle { get; private set; }
    [SerializeField] float postActionDelay;

    [Header("Target Mathching")]
    [SerializeField] bool enableTargetMatching = true;
    [SerializeField]protected AvatarTarget matchBodyPart;
    [SerializeField] float matchStartTime;
    [SerializeField] float matchTargetTime;
    [SerializeField] Vector3 matchPositionWeight = new Vector3(0,1,0);
    public Quaternion TargetRotation { get;  set; }
    public Vector3 MatchPosition { get; set; }
    public bool Mirror { get; set; }
    public virtual bool CheckIfPossible(ObstacleHitData hitData,Transform player)
    {
        if(!string.IsNullOrEmpty(obstacleTag)&& hitData.forwardHit.transform.tag != obstacleTag)
        {
            return false;
        }
        float height= hitData.heightHit.point.y - player.position.y;
        if(height < minHeight|| height> maxHeight)
        {
            return false;
        }
        if (RotateToObstacle)
        {
            TargetRotation =Quaternion.LookRotation(-hitData.forwardHit.normal);
        }
        if (enableTargetMatching)
        {
            MatchPosition = hitData.heightHit.point;
        }
        return true;
    }

    public bool EnableTargetMatching => enableTargetMatching;
    public AvatarTarget MatchBodyPart => matchBodyPart;
    public float MatchStartTime => matchStartTime;
    public float MatchTargetTime => matchTargetTime;
    public Vector3 MatchPositionWeight => matchPositionWeight;
    public string AnimationName { get { return animName; } }
    public float PostActionDelay => postActionDelay;
}
