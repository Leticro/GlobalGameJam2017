using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour
{
	#region Private
	private Camera _camera;
	[SerializeField]
	private float _waveRadius;
	[SerializeField]
	private float _waveForce;

#if DEBUG
	private bool _showCollider;
	private float _colliderTime;
	private Vector3 _colliderPosition;
#endif
	#endregion

	private void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray = _camera.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000, 1 << LayerMask.NameToLayer("Floor")))
			{
				_showCollider = true;
				_colliderPosition = hitInfo.point;
				_colliderTime = Time.time;

				var objects = Physics.OverlapSphere(hitInfo.point, _waveRadius, 1 << LayerMask.NameToLayer("Player"));
				foreach (var obj in objects)
				{
					var direction = obj.transform.position - hitInfo.point;
					var rigidbody = obj.GetComponent<Rigidbody>();
					var force = Mathf.Lerp(_waveForce, 0, Vector3.Distance(hitInfo.point, obj.transform.position) / _waveRadius);
					rigidbody.AddForceAtPosition(direction.normalized * force, hitInfo.point, ForceMode.Impulse);
				}
			}
		}
	}

#if DEBUG
	private void OnDrawGizmos()
	{
		if (_showCollider)
		{
			if (_colliderTime + 1 <= Time.time)
			{
				_showCollider = false;
			}

			Gizmos.color = new Color(0, 1, 0, 0.25f);
			Gizmos.DrawSphere(_colliderPosition, _waveRadius);
		}
	}
#endif
}
