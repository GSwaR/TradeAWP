using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System.Data.SqlTypes;

public class AddNewOrder : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    public Button Button;

    public Text BuyerID;
    public Text OrderID;
    public Text ManagerID;
    public Text ItemID;
    public Text BuyTime;
    public Text Amount;
    public Text Price;

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (BuyerID.text == "" || OrderID.text == "" || ManagerID.text == "" || ItemID.text == "" || Amount.text == "" || Price.text == "")
        {
            ConnectionManager.errorHandler.Handle("Input fields can't be empty!");
        }
        else
        {
            SqlDateTime dateTime = DateTime.Now;
            string queryString = @"insert into BuyOrder
                                    values
                                    ('" + OrderID.text + "', " +
                                    "'" + BuyerID.text +
                                    "', '" + ManagerID.text + "', " + "'" + ItemID.text + "', " +
                                    "convert(date, '" + dateTime + "')," + int.Parse(Amount.text)+ ", " + float.Parse(Price.text) + ");";
            try
            {
                SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ConnectionManager.errorHandler.Handle(e.Message);
            }
        }
    }

}
