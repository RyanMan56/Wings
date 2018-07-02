using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadTracking : MonoBehaviour
{
    private readonly XRNode nodeType = XRNode.Head;
    public CapsuleCollider playerCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 headsetPos = InputTracking.GetLocalPosition(nodeType);
        Quaternion headsetRot = InputTracking.GetLocalRotation(nodeType);

        transform.localPosition = headsetPos;
        transform.localRotation = headsetRot;

        playerCollider.transform.localPosition = transform.localPosition;
        playerCollider.transform.rotation = Quaternion.identity;
        playerCollider.height = headsetPos.y / transform.localScale.y;
        playerCollider.center = new Vector3(playerCollider.center.x, headsetPos.y / 2 - playerCollider.height / 2, playerCollider.center.z);
    }
}
