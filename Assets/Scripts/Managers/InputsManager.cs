using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    public static Action OnRightClick;
    public static Action OnLeftClick;

    
    public static Action OnActionKeyPressed;
    public static Action OnJumpKeyPressed;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            BroadcastJumpEvent();

        if (Input.GetKeyDown(KeyCode.E))
            BroadcastActionEvent();
    }


    private void BroadcastJumpEvent()
    {
        if (OnJumpKeyPressed != null)
            OnJumpKeyPressed();
    }

    private void BroadcastActionEvent()
    {
        if (OnActionKeyPressed != null)
            OnActionKeyPressed();
    }

}
