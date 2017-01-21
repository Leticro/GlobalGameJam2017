using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Private
    [SerializeField]
    private Vector3 jump = new Vector3(0, 200, 0);
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(jump, ForceMode.Impulse);
        }
    }
}

