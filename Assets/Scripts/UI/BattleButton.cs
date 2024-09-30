using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public delegate void ButtonAction();
    public event ButtonAction OnButtonPressed;

    public void CallButtonPressedAction()
    {
        OnButtonPressed();
    }
}
