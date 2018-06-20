using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

	public Camera cam;
	public UnityEngine.AI.NavMeshAgent agent;
	public NavPoint currentPoint;

	// Use this for initialization
	void Start () {
		agent.SetDestination(currentPoint.transform.position);
	}

	// Update is called once per frame
	void Update () {
		float dist = agent.remainingDistance;
		// if (dist!=Mathf.infinite && agent.pathStatus==NavMeshPathStatus.completed && agent.remainingDistance==0)
		if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
		{
			if (currentPoint.next)
			{
				currentPoint = currentPoint.next;
				agent.SetDestination(currentPoint.transform.position);
			}
		}
		// if (Input.GetMouseButtonDown(0)) {
		// 	Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		// 	RaycastHit hit;
		//
		// 	if (Physics.Raycast(ray, out hit))
		// 	{
		// 		agent.SetDestination(hit.point);
		// 	}
		// }
	}
}
