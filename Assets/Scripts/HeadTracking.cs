using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadTracking : MonoBehaviour
{
    private readonly XRNode nodeType = XRNode.Head;
    public CapsuleCollider playerCollider;
    private SphereCollider headCollider;
    public GameObject avatar;

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
        
        playerCollider.transform.rotation = Quaternion.identity;
        playerCollider.height = headsetPos.y / transform.localScale.y + headCollider.radius;
        playerCollider.center = new Vector3(playerCollider.center.x, - playerCollider.height / 2f + headCollider.radius, playerCollider.center.z);

        //transform.parent.position = Vector3.zero;
    }
}
