using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour
{
	#region Private
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private float _waveRadius;
	[SerializeField]
	private float _waveForce;

	private bool _showCollider;
	private float _colliderTime;
	private Vector3 _colliderPosition;
	#endregion

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
					rigidbody.AddForceAtPosition(direction.normalized * _waveForce, hitInfo.point, ForceMode.Impulse);
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (_showCollider)
		{
			if (_colliderTime + 1 <= Time.time)
			{
				_showCollider = false;
			}

			Gizmos.color = new Color(0, 1, 0, 0.5f);
			Gizmos.DrawSphere(_colliderPosition, _waveRadius);
		}
	}
}
