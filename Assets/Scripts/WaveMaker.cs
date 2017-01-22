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
	[SerializeField]
	private GameObject _waveEffectPrefab;

	private bool _hasStarted;

#if DEBUG
	private bool _showCollider;
	private float _colliderTime;
	private Vector3 _colliderPosition;
#endif
	#endregion

	public void StartGame()
	{
		_hasStarted = true;
	}

	public void EndGame()
	{
		_hasStarted = false;
	}

	private void Awake()
	{
		_camera = GetComponent<Camera>();
		_hasStarted = false;
	}

	private void Update()
	{
		if (!_hasStarted)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			var ray = _camera.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000, 1 << LayerMask.NameToLayer("Floor")))
			{
				_showCollider = true;
				_colliderPosition = hitInfo.point;
				_colliderTime = Time.time;

				Instantiate(_waveEffectPrefab, hitInfo.point + Vector3.up * 0.05f, Quaternion.identity);

				var objects = Physics.OverlapSphere(hitInfo.point, _waveRadius, 1 << LayerMask.NameToLayer("Player"));
				foreach (var obj in objects)
				{
					var direction = obj.transform.position - _colliderPosition;
					direction.y = 0;
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
