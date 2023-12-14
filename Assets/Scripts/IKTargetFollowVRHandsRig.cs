using UnityEngine;

/// <summary>
/// Make a transform follow position and rotation
/// </summary>
[System.Serializable]
public class VRHandsMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

/// <summary>
/// The body will follow user head and hands point by point
/// </summary>
public class IKTargetFollowVRHandsRig : MonoBehaviour
{
    [Range(0, 1)]
    public float turnSmoothness = 0.1f;
    public VRHandsMap head;

    [Header("------------Left Hand------------")]
    public VRHandsMap m_leftWrist;

    public VRHandsMap m_leftIndex1;
    public VRHandsMap m_leftIndex2;
    public VRHandsMap m_leftIndex3;
    public VRHandsMap m_leftIndex4;

    public VRHandsMap m_leftLittle1;
    public VRHandsMap m_leftLittle2;
    public VRHandsMap m_leftLittle3;
    public VRHandsMap m_leftLittle4;

    public VRHandsMap m_leftMiddle1;
    public VRHandsMap m_leftMiddle2;
    public VRHandsMap m_leftMiddle3;
    public VRHandsMap m_leftMiddle4;

    public VRHandsMap m_leftRing1;
    public VRHandsMap m_leftRing2;
    public VRHandsMap m_leftRing3;
    public VRHandsMap m_leftRing4;

    public VRHandsMap m_leftThumb1;
    public VRHandsMap m_leftThumb2;
    public VRHandsMap m_leftThumb3;
    public VRHandsMap m_leftThumb4;

    [Header("------------Right Hand------------")]
    public VRHandsMap m_rightWrist;

    public VRHandsMap m_rightIndex1;
    public VRHandsMap m_rightIndex2;
    public VRHandsMap m_rightIndex3;
    public VRHandsMap m_rightIndex4;

    public VRHandsMap m_rightLittle1;
    public VRHandsMap m_rightLittle2;
    public VRHandsMap m_rightLittle3;
    public VRHandsMap m_rightLittle4;

    public VRHandsMap m_rightMiddle1;
    public VRHandsMap m_rightMiddle2;
    public VRHandsMap m_rightMiddle3;
    public VRHandsMap m_rightMiddle4;

    public VRHandsMap m_rightRing1;
    public VRHandsMap m_rightRing2;
    public VRHandsMap m_rightRing3;
    public VRHandsMap m_rightRing4;

    public VRHandsMap m_rightThumb1;
    public VRHandsMap m_rightThumb2;
    public VRHandsMap m_rightThumb3;
    public VRHandsMap m_rightThumb4;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;


    void LateUpdate()
    {
        transform.position = head.ikTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

        head.Map();

        m_leftWrist.Map();

        m_leftIndex1.Map();
        m_leftIndex2.Map();
        m_leftIndex3.Map();
        m_leftIndex4.Map();

        m_leftLittle1.Map();
        m_leftLittle2.Map();
        m_leftLittle3.Map();
        m_leftLittle4.Map();

        m_leftMiddle1.Map();
        m_leftMiddle2.Map();
        m_leftMiddle3.Map();
        m_leftMiddle4.Map();

        m_leftRing1.Map();
        m_leftRing2.Map();
        m_leftRing3.Map();
        m_leftRing4.Map();

        m_leftThumb1.Map();
        m_leftThumb2.Map();
        m_leftThumb3.Map();
        m_leftThumb4.Map();

        m_rightWrist.Map();

        m_rightIndex1.Map();
        m_rightIndex2.Map();
        m_rightIndex3.Map();
        m_rightIndex4.Map();

        m_rightLittle1.Map();
        m_rightLittle2.Map();
        m_rightLittle3.Map();
        m_rightLittle4.Map();

        m_rightMiddle1.Map();
        m_rightMiddle2.Map();
        m_rightMiddle3.Map();
        m_rightMiddle4.Map();

        m_rightRing1.Map();
        m_rightRing2.Map();
        m_rightRing3.Map();
        m_rightRing4.Map();

        m_rightThumb1.Map();
        m_rightThumb2.Map();
        m_rightThumb3.Map();
        m_rightThumb4.Map();
    }
}


