using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInteractor : MonoBehaviour
{
    public delegate void InteractorAction();
    public event InteractorAction OnInteractorClicked;

    public void CallInteractorClickedAction()
    {
        OnInteractorClicked();
    }
}
