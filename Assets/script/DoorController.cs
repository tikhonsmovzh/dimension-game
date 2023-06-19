using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animation _myAnimation;

    public bool isOpenable = false;

    private void Awake()
    {
        _myAnimation = GetComponent<Animation>();
    }

    public void Open()
    {
        _myAnimation.Play("OpenAnim");
    }

    public void Close() 
    {
        _myAnimation.Play("CloseAnim");
    }
}
