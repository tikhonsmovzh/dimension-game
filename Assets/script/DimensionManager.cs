using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    [SerializeField] private GameObject _dimension1, _dimension2;
    [SerializeField] private KeyCode _dimensionKey = KeyCode.Q;
    [SerializeField] private Animation _raumkonsoleAnimation;

    public static GameObject ActiveDimension;

    private bool _currentDimension = true;

    private void Start()
    {
        UpdateDimension();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_dimensionKey))
        {
            _currentDimension = !_currentDimension;

            UpdateDimension();

            _raumkonsoleAnimation.Play("RaumkonsoleClickAnim");
        }
    }

    private void UpdateDimension()
    {
        if(_currentDimension)
        {
            _dimension1.SetActive(true);
            _dimension2.SetActive(false);

            ActiveDimension = _dimension1;

            return;
        }

        _dimension1.SetActive(false);
        _dimension2.SetActive(true);

        ActiveDimension = _dimension2;
    }
}