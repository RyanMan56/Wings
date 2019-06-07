using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform leftHandObj = null;
    public Transform rightHandObj = null;
    public Transform head = null;
    public Transform extra;
    private float raycastDistance = 1;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform hip;
    public float footMod = 0.12f;
    public Transform target;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                //if (lookObj != null)
                //{
                //    animator.SetLookAtWeight(1);
                //    animator.SetLookAtPosition(lookObj.position);
                //}

                Vector3 rotAdjust = new Vector3(-head.localEulerAngles.y, head.localEulerAngles.z, -head.localEulerAngles.x);
                Quaternion rot = Quaternion.Euler(rotAdjust);
                animator.SetBoneLocalRotation(HumanBodyBones.Neck, rot);
                //animator.SetLookAtWeight(1);
                //animator.SetLookAtPosition(target.position);

                //animator.Set

                // Set the left hand target position and rotation, if one has been assigned
                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

                IkFoot(leftFoot, AvatarIKGoal.LeftFoot);
                IkFoot(rightFoot, AvatarIKGoal.RightFoot);                
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }

    void IkFoot(Transform foot, AvatarIKGoal ikFoot)
    {
        // Bit shift index of layer to get layer mask (i.e. 1 << 8 goes from 000000001 100000000)
        int layerMask = 1 << 9; // 9 is Player Body layer

        // Since we want to collide with everything BUT Player Body, invert bitmask with ~
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(foot.position, transform.TransformDirection(-Vector3.up), out hit, raycastDistance, layerMask))
        {
            Debug.DrawRay(foot.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.red);

            animator.SetIKPositionWeight(ikFoot, 1);
            animator.SetIKRotationWeight(ikFoot, 1);
            animator.SetIKPosition(ikFoot, hit.point + Vector3.up * footMod);
            animator.SetIKRotation(ikFoot, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
        }
        else if (Physics.Raycast(new Vector3(foot.position.x, hip.position.y, foot.position.z), transform.TransformDirection(-Vector3.up), out hit, raycastDistance, layerMask))
        {
            Debug.DrawRay(new Vector3(foot.position.x, hip.position.y, foot.position.z), transform.TransformDirection(-Vector3.up) * hit.distance, Color.red);

            animator.SetIKPositionWeight(ikFoot, 1);
            animator.SetIKRotationWeight(ikFoot, 1);
            animator.SetIKPosition(ikFoot, hit.point + Vector3.up * footMod);
            animator.SetIKRotation(ikFoot, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
        }
        else
        {
            Debug.DrawRay(foot.position, transform.TransformDirection(-Vector3.up) * raycastDistance, Color.white);
        }
    }
}