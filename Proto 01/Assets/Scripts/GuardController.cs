using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

	public Camera cam;
	public UnityEngine.AI.NavMeshAgent agent;
	public NavPoint currentPoint;
	public FieldOfView fov;
	public PlayerController hunted;

	// Use this for initialization
	void Start () {
		agent.SetDestination(currentPoint.transform.position);
	}

	// Update is called once per frame
	void Update () {
		foreach (var target in fov.visibleTargets) {
			var player = target.gameObject.GetComponent<PlayerController>();

			if (player != null) {
				if (player.inNaughtyZone) {
					hunted = player;
					agent.SetDestination(hunted.transform.position);
				}
			}
		}

		if (hunted == null) {
			float dist = agent.remainingDistance;
			if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
			{
				if (currentPoint.next)
				{
					currentPoint = currentPoint.next;
					agent.SetDestination(currentPoint.transform.position);
				}
			}
		} else {
			float dist = agent.remainingDistance;
			if (dist < .4f) {
				hunted.gameObject.SetActive(false);
			}
		}
	}

	void OnMouseDown() {
		if (Input.GetMouseButton(0)) {
			Debug.Log("GUARD");
			// drawCone = true;
		}
	}
}
