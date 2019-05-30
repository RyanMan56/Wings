using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfPressure : MonoBehaviour
{
    public float cp;
    public float cg;
    private Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, cg, 0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // find deviation of nose from direction of flight
        var dif = Vector3.Angle(rb.transform.up, Vector3.zero);

        // calculate the size of the center of pressure force based on angle (maximum of -0.125 at 90 degrees) 
        var cpScale = ((Mathf.Abs(dif - 90) - 90) / 720);

        //apply force to the center of pressure
        rb.AddForceAtPosition(rb.velocity * cpScale, transform.TransformPoint(0, cp, 0));
    }
}
