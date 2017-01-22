using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    #region Private
    [SerializeField]
    private float _zoomInCap;
    [SerializeField]
    private float _zoomOutCap;
    [SerializeField]
    private float _cameraSensitivity;

    private float _padScroll;

    private readonly float CAMERA_SCALAR = 0.1f;
    #endregion

    private void Update()
    {
        float scroll = mouseScroll();

        if (scroll > 0f && transform.position.y > _zoomInCap)
        {
            print(scroll);
            transform.position -= transform.position * _cameraSensitivity * CAMERA_SCALAR;
        }
        else if (scroll < 0f && transform.position.y < _zoomOutCap)
        {
            print(scroll);
            transform.position += transform.position * _cameraSensitivity * CAMERA_SCALAR;
        }
    }

        private float mouseScroll()
    {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            if (mouseScroll != 0)
                return mouseScroll;
            else
                return _padScroll;
    }

    //Get TrackPad Scroll
    void OnGUI()
    {
        if (Event.current.type == EventType.ScrollWheel)
        {
            print(Event.current.delta);
            _padScroll = Event.current.delta.y / 100;
        }
        else
            _padScroll = 0;
    }
}
