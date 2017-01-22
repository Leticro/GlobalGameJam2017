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
    [SerializeField]
    private AudioSource _jumpAudio;

    private bool _col;
    #endregion

    private void Awake()
    {
        _jumpAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _col || Input.GetKeyDown(KeyCode.Mouse1) && _col)
        {
            if (_jumpAudio)
            {
                _jumpAudio.Play();
            }
            GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpPower, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        foreach (var c in col.contacts)
        {
            if (c.normal.y >= 1 - _acceptableSlope)
            {
                _col = true;
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        _col = false;
    }
}

