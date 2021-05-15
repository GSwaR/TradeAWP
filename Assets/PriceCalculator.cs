using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class PriceCalculator : MonoBehaviour
{
    public InputField ItemID;
    public InputField Amount;
    public InputField InputField;
    public ConnectionManager ConnectionManager;
    public Text Price;

    private void Start()
    {
        InputField.onValueChanged.AddListener(Calculate);
    }

    private void Calculate(string arg)
    {
        if (ItemID.text != "")
        {
            string queryString = @"SELECT ItemID
                                    ,Price
                                    FROM [TradeManagerConsole].[dbo].Item
                                    WHERE ItemID = '"+ ItemID.text +"'";
            SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

            List<float> data = new List<float>();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(float.Parse(reader[1].ToString()));
                    }

                    float[] arr = data.ToArray();

                    if (data.Count > 0)
                    {
                        Price.text = (float.Parse(Amount.text) * arr[0]) + "";
                    }
                }
            }
            catch (Exception e)
            {
                ConnectionManager.errorHandler.Handle(e.Message);
            }
        }
    }

}
