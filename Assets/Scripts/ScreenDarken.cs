using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDarken : MonoBehaviour
{
    public delegate void DarkScreenAction();
    public event DarkScreenAction OnScreenClicked;

    public void CallScreenClickedAction()
    {
        OnScreenClicked();
    }
}
