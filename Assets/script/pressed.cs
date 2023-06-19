using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pressed : MonoBehaviour
{
    private bool _active = false;

    private void OnCollisionExit(Collision collision)
    {
        PlateController controller;

        if (controller = collision.gameObject.GetComponent<PlateController>())
        {
            if (!_active)
                return;

            _active = false;

            controller.DePress();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlateController controller;

        if (controller = collision.gameObject.GetComponent<PlateController>())
        {
            if (_active)
                return;

            _active = true;

            controller.Press();
        }
    }
}