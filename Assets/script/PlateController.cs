using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField] private DoorController _activeObject;
    [SerializeField] private Color _activeColor;

    private Renderer _plateMaterial;

    private Animation _myAnimation;

    private uint _countPress = 0;

    private void Awake()
    {
        _plateMaterial = GetComponentInChildren<Renderer>();
        _myAnimation = GetComponent<Animation>();
    }

    public void Press()
    {
        if (_countPress == 0)
        {
            _plateMaterial.material.color = _activeColor;

            _myAnimation.Play("PressAnim");
            _activeObject.Open();
        }

        _countPress++;
    }

    public void DePress()
    {
        _countPress--;

        if (_countPress == 0)
        {
            _plateMaterial.material.color = Color.red;

            _myAnimation.Play("DePressAnim");
            _activeObject.Close();
        }
    }
}