﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;

	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargets = new List<Transform>();

	void Start() {
		StartCoroutine("FindTargetsWithDelay", .2f);
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
				float distanceToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask)) {
					// Can See!!
					visibleTargets.Add(target);
				}
			}
		}
	}

	public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) {
			angleDegrees += transform.eulerAngles.y;
		}

		return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
	}
}
