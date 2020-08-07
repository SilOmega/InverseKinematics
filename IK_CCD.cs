using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inverse Kinematics implementation for body animation using Cyclic Coordinates Descent

public class IK_CCD : MonoBehaviour
{

    public Transform Target;  // Target point to follow
    public Transform[] joints;  // List of body's joints
    private Matrix coordinates;
    public Transform endEffector;
    public Vector3 deltaE;

    
    void Start()
    {
        joints = transform.GetComponentsInChildren<Transform>();
        coordinates = new Matrix(3, joints.Length);
        endEffector = joints[joints.Length - 1];
        deltaE = Target.position - endEffector.position;    
    }

    // Update is called once per frame
    void Update()
    {
        InverseKinematics_CCD();
    }

    public void InverseKinematics_CCD()
    {
        for (int i = 0; i < joints.Length-1; i++)
        {
            Vector3 goal = Target.position - joints[i].position;
            Vector3 deltaJ = endEffector.position - joints[i].position;
            Quaternion rotation = Quaternion.FromToRotation(deltaJ, goal);
            joints[i].rotation = joints[i].rotation * rotation;
            // joints[i].rotation = Quaternion.Slerp(joints[i].rotation, rotation, weight);
            deltaE = Target.position - endEffector.position;
            if (Mathf.Abs(deltaE.x) < 0.01f && Mathf.Abs(deltaE.y) < 0.01f && Mathf.Abs(deltaE.z) < 0.01f)
                break;
         
            
        }
        
        
    }

}
