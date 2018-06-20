using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float viewRadius = 15f;

	[Range(0, 360)]
	public float viewAngle = 75f;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public float meshResolution = 0.20f;
	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	public int edgeResolveIterations;
	public float edgeDistanceThreshold;

	public List<Transform> visibleTargets = new List<Transform>();

	void Start() {
		viewMesh = new Mesh();
		viewMesh.name = "view mesh";
		viewMeshFilter.mesh = viewMesh;
		StartCoroutine("FindTargetsWithDelay", .2f);
	}

	void Update() {
		DrawFieldOfView();
	}

	// void LateUpdate() {
	// 	DrawFieldOfView();
	// }

	EdgeInfo FindEdge(ViewCastInfo min, ViewCastInfo max) {
		float minAngle = min.angle;
		float maxAngle = max.angle;

		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		// while would be better?
		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo viewCast = ViewCast(angle);

			bool edgeDistanceThresholdExceeded = Mathf.Abs(min.distance - viewCast.distance) > edgeDistanceThreshold;
			if (viewCast.hit == min.hit && !edgeDistanceThresholdExceeded) {
				minAngle = viewCast.angle;
				minPoint = viewCast.point;
			} else {
				maxAngle = viewCast.angle;
				maxPoint = viewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}

	void DrawFieldOfView() {
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();

		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i < stepCount; i++) {
			float currentAngle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			// Debug.DrawLine(transform.position, transform.position + DirFromAngle(currentAngle, true) * viewRadius, Color.red);

			ViewCastInfo viewCast = ViewCast(currentAngle);

			if (i > 0) {
				bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.distance - viewCast.distance) > edgeDistanceThreshold;
				if (oldViewCast.hit != viewCast.hit || (oldViewCast.hit && viewCast.hit && edgeDistanceThresholdExceeded)) {
					EdgeInfo edge = FindEdge(oldViewCast, viewCast);

					if (edge.pointA != Vector3.zero) {
						viewPoints.Add(edge.pointA);
					}

					if (edge.pointB != Vector3.zero) {
						viewPoints.Add(edge.pointB);
					}
				}
			}

			viewPoints.Add(viewCast.point);

			oldViewCast = viewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

			if (i < vertexCount - 2) {
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}

	ViewCastInfo ViewCast(float globalAngle) {
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask)) {
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}

		return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
	}

	public struct ViewCastInfo {
		public bool hit;
		public Vector3 point;
		public float distance;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle) {
			hit = _hit;
			point = _point;
			distance = _distance;
			angle = _angle;
		}
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

	public struct EdgeInfo {
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
			pointA = _pointA;
			pointB = _pointB;
		}
	}
}
