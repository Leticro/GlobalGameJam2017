using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject player;

    #region Private
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _zoomOutCap;
    [SerializeField]
    private float _zoomInCap;

    private Vector3 _camDistance = new Vector3(-8, 20, -15);
    private Vector3 _camSense = new Vector3(-8, 20, -15)/10;
    #endregion  

    void Update()
    {
        mouseScroll(); // enable mouse scrolling
        _camera.transform.position = player.transform.position + _camDistance; // camera follow player
    }

    void mouseScroll()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f && _camera.transform.position.y > _zoomInCap)
        {
            _camDistance -= _camSense;
        }
        else if (scroll < 0f && _camera.transform.position.y < _zoomOutCap)
        {
            _camDistance += _camSense;
        }
    }
}