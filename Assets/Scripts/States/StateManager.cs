using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State CurrentState;
    public State LoginState;
    public State WorkflowState;
    public State MenuState;
    public State CreateOrderState;
    public State CreateWarrantyState;
    public State CreateBuyerState;
    public State ValidBuyerState;
    public State ValidWarrantyState;
    public State ValidOrderState;
    public State ReportAllBuyerOrdersState;
    public State ReportAllBuyerWarrantiesState;
    public State TableBuyerState;
    public State TableBuyOrderState;
    public State TableBrandState;
    public State TableItemState;
    public State TableItemTypeState;
    public State TableManagerState;
    public State TableWarrantyState;
    public State TableWarrantyTypeState;

    public void Awake()
    {
        CurrentState = LoginState;
        LoginState.EnableState();
    }

    public void SetCurrentState(State state)
    {
        CurrentState.DisableState();
        CurrentState = state;
        CurrentState.EnableState();
    }
}
