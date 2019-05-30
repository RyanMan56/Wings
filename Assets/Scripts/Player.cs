using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject leftWing;
    private GameObject leftForearm;
    private GameObject leftUpperArm;
    private GameObject leftHand;
    private GameObject leftEndEffector;

    private GameObject rightWing;
    private GameObject rightForearm;
    private GameObject rightUpperArm;
    private GameObject rightHand;
    private GameObject rightEndEffector;

    public Transform goal;

    private void Awake()
    {
        leftWing = transform.Find("Left Wing").gameObject;
        leftUpperArm = leftWing.transform.GetChild(0).Find("Upper Arm").gameObject;
        leftForearm = leftUpperArm.transform.GetChild(0).Find("Forearm").gameObject;
        leftHand = leftForearm.transform.GetChild(0).Find("LeftHand").gameObject;
        leftEndEffector = leftHand.transform.Find("End Effector").gameObject;

        rightWing = transform.Find("Right Wing").gameObject;
        rightUpperArm = rightWing.transform.GetChild(0).Find("Upper Arm").gameObject;
        rightForearm = rightUpperArm.transform.GetChild(0).Find("Forearm").gameObject;
        rightHand = rightForearm.transform.GetChild(0).Find("RightHand").gameObject;
        rightEndEffector = rightHand.transform.Find("End Effector").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fabrik.Go(ref leftEndEffector, ref goal, ref leftHand, ref leftUpperArm);
        Fabrik.Go(ref rightEndEffector, ref goal, ref rightHand, ref rightUpperArm);
    }
}
