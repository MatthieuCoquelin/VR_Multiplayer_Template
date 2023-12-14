using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create leg movement when Moving 
public class IKFootSolver : MonoBehaviour
{
    public bool isMovingForward;

    /// <summary>
    /// We need to reference the other leg to alternate them 
    /// </summary>

    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] Transform body = null;
    [SerializeField] IKFootSolver otherFoot = null;
    [SerializeField] float speed = 4;
    [SerializeField] float stepDistance = .2f;
    [SerializeField] float stepLength = .2f;
    [SerializeField] float sideStepLength = .1f;

    [SerializeField] float stepHeight = .3f;
    [SerializeField] Vector3 footOffset;

    public Vector3 footRotOffset;
    public float footYPosOffset = 0.1f;

    public float rayStartYOffset = 0;
    public float rayLength = 1.5f;
    
    float footSpacing;
    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;
    float lerp;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    /// <summary>
    /// when the body move the legs adapt they size depending on the floor level
    /// </summary>

    void Update()
    {
        // check for head player movement
        transform.position = currentPosition + Vector3.up * footYPosOffset;
        transform.localRotation = Quaternion.Euler(footRotOffset);

        // the ray give the floor position 
        Ray ray = new Ray(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);

        Debug.DrawRay(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);
        
        // if raycast floor
        if (Physics.Raycast(ray, out RaycastHit info, rayLength, terrainLayer.value))
        {
            // compute new position if the step is big enouth and a step is not happeniing
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                Vector3 direction = Vector3.ProjectOnPlane(info.point - currentPosition,Vector3.up).normalized;

                float angle = Vector3.Angle(body.forward, body.InverseTransformDirection(direction));

                // may adap leg movement for the side
                isMovingForward = angle < 50 || angle > 130;

                if(isMovingForward)
                {
                    newPosition = info.point + direction * stepLength + footOffset;
                    newNormal = info.normal;
                }
                else
                {
                    newPosition = info.point + direction * sideStepLength + footOffset;
                    newNormal = info.normal;
                }

            }
        }

        // leg movement 
        if (lerp < 1)
        {
            // lerp illustrate the movement : one step to another 
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);

            // Sin on Y axis is here to have movement more natural
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        // visualize result of the positon calculation 
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.1f);
    }


    // return leg movement 
    public bool IsMoving()
    {
        return lerp < 1;
    }



}
