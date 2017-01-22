using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Private
    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    private float _acceptableSlope;

    private bool _col;
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _col)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpPower, 0), ForceMode.Impulse);
            _col = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        foreach (var c in col.contacts)
        {
            if (c.point.y > 0.5f - _acceptableSlope/2 && c.point.y < 0.5f + _acceptableSlope/2)
            {
                _col = true;
            }
        }
    }
}

