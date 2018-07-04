using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadTracking : MonoBehaviour
{
    private readonly XRNode nodeType = XRNode.Head;
    public CapsuleCollider playerCollider;
    private SphereCollider headCollider;

	// Use this for initialization
	void Start () {
        headCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 headsetPos = InputTracking.GetLocalPosition(nodeType);
        Quaternion headsetRot = InputTracking.GetLocalRotation(nodeType);

        transform.localPosition = headsetPos;
        transform.localRotation = headsetRot;

        //playerCollider.transform.localPosition = transform.localPosition;
        playerCollider.transform.rotation = Quaternion.identity;
        playerCollider.height = headsetPos.y / transform.localScale.y; // Height is definitely right, center is wrong
        // playerCollider height already adjusted for scale
        playerCollider.center = new Vector3(playerCollider.center.x, - playerCollider.height / 2f + headCollider.radius, playerCollider.center.z);

        //transform.parent.position = Vector3.zero;
    }
}
