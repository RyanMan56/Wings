using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fabrik
{
    // Distance allowance between goal and end effector
    private static float EPS = 0.5f;

    public static void Go(ref GameObject endEffector, ref Transform goal, ref GameObject finalLimb, ref GameObject rootLimb) // rootLimb is the limb not the joint
    {
        Debug.Log("Pre-Running!");
        while (Mathf.Abs(Vector3.Distance(endEffector.transform.position, goal.position)) > EPS)
        {
            Debug.Log("Running!");
            //FinalToRoot(ref goal, ref finalLimb);
            RootToFinal(ref rootLimb);
        }
    }

    private static void FinalToRoot(ref Transform goal, ref GameObject finalLimb)
    {
        Vector3 currentGoal = goal.position;
        GameObject currentLimb = finalLimb;
        while (currentLimb != null)
        {
            var diff = currentGoal - currentLimb.transform.parent.position;
            //currentLimb.transform.rotation = Vector3.Angle(Vector3.up, diff));
            
            //currentLimb.transform.rotation = Quaternion.LookRotation(currentGoal); // Not sure if this is calculated right, try diff
            currentLimb.transform.parent.rotation = Quaternion.LookRotation(currentGoal); // Not sure if this is calculated right, try diff

            // setting outboard position
            currentLimb.transform.parent.position = goal.position;


            Transform inboard = currentLimb.transform.parent;
            currentGoal = inboard.position;
            currentLimb = inboard.parent.gameObject;
            if (currentLimb.name == "Right Wing" || currentLimb.name == "Left Wing")
            {
                currentLimb = null;
            }
        }
    }

    private static void RootToFinal(ref GameObject rootLimb)
    {
        Transform currentInboardPosition = rootLimb.transform.parent;
        GameObject currentLimb = rootLimb;
        while (currentLimb != null)
        {
            currentLimb.transform.parent.position = currentInboardPosition.position;

            var outboard = currentLimb.transform.Find("Joint"); // Outboard position
            if (outboard == null)
            {
                outboard = currentLimb.transform.Find("End Effector");
            }
            currentInboardPosition = outboard;
            if (outboard.childCount == 0)
            {
                currentLimb = null;
            }
            else
            {
                currentLimb = outboard.GetChild(0).gameObject;
            }
        }
    }
}
