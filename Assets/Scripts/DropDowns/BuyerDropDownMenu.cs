using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;


public class BuyerDropDownMenu : DropDownMenu
{
    private new void OnEnable()
    {
        SetChoices();
    }

    public override void SetChoices()
    {
        string queryString =    @"SELECT [BuyerID]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string data = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                dropdown.options.Add(new Dropdown.OptionData(data, null));
            }
        }
    }
}
