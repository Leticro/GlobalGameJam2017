using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;
	private bool _isBumpping;
	[SerializeField]
	private float _bumpAmount;

	private void OnCollisionEnter(Collision info)
	{
		if (!_isBumpping)
		{
			_isBumpping = true;
			_animator.SetBool("bumpped", true);

			var reflect = info.relativeVelocity - 2 * info.contacts[0].normal * (Vector3.Dot(info.contacts[0].normal, info.relativeVelocity));

			info.rigidbody.AddForce(reflect.normalized * _bumpAmount, ForceMode.Impulse);
		}
	}

	private void OnCollisionExit(Collision info)
	{
		_isBumpping = false;
	}
}
