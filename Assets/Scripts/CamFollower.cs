using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Transform target;
    Vector3 distanceFromTarget;

	void Start ()
    {
        distanceFromTarget = transform.position - target.position;
    }
	
	void Update ()
    {
        transform.position = target.position + distanceFromTarget;
	}
}
