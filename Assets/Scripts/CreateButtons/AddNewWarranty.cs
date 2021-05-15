using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlTypes;

public class AddNewWarranty : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    public Button Button;

    public Text BuyerID;
    public Text WarrantyID;
    public Text WarrantyTypeID;
    public Text ItemID;
    public Text BuyTime;
    public Text Term;

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (BuyerID.text == "" || WarrantyID.text == "" || WarrantyTypeID.text == "" || ItemID.text == "" || Term.text == "")
        {
            ConnectionManager.errorHandler.Handle("Input fields can't be empty!");
        }
        else
        {
            SqlDateTime dateTime = DateTime.Now;
            try
            {
                string queryString = @"insert into Warranty
                                    values
                                    ('" + WarrantyID.text + "', " +
                                    "'" + BuyerID.text +
                                    "', '" + WarrantyTypeID.text + "', '" +
                                    ItemID.text + "', convert(date, '"+ dateTime + "')," + int.Parse(Term.text) + ");";
        
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
