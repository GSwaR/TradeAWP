using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class PriceCalculator : MonoBehaviour
{
    public Text ItemName;
    public InputField Amount;
    public InputField InputField;
    public ConnectionManager ConnectionManager;
    public Dropdown Dropdown;
    public Text Price;

    private void Start()
    {
        InputField.onValueChanged.AddListener(Calculate);
        Dropdown.onValueChanged.AddListener(Calc);
    }


    private void Calc(int args)
    {
        if (Amount.text == "")
        {

        }
        else {
            string ItemName_ = ItemName.text.Split(',')[0];
            foreach (string item in ItemName.text.Split(','))
            {
                Debug.LogError(item);
            }
            string queryString = @"SELECT ItemName
                                    ,Price
                                    FROM [TradeManagerConsole].[dbo].Item
                                    WHERE ItemName = '" + ItemName_ + "';";
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

    private void Calculate(string arg)
    {
            string ItemName_ = ItemName.text.Split(',')[0];
            foreach (string item in ItemName.text.Split(','))
            {
                Debug.LogError(item);
            }
            string queryString = @"SELECT ItemName
                                    ,Price
                                    FROM [TradeManagerConsole].[dbo].Item
                                    WHERE ItemName = '" + ItemName_ + "';";
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
