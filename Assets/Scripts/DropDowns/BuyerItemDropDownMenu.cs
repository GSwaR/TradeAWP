using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlTypes;

public class BuyerItemDropDownMenu : DropDownMenu
{
    public Text Buyer;
    public string BuyerID;
    public Dropdown Dropdown;
    public Text label;

    private new void OnEnable()
    {
        dropdown.value = -1;
        //SetChoices();
    }

    public void Start()
    {
        Dropdown.onValueChanged.AddListener(SetChoices);
    }

    public override void SetChoices()
    {
        if (Buyer.text.Length == 0)
        {

        }
        else
        {
            string Name = Buyer.text.Split(' ')[0];
            string LastName = Buyer.text.Split(' ')[2];
            string MiddleName = Buyer.text.Split(' ')[1];

            string queryString = @"SELECT [BuyerID]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]
                                WHERE BuyerName = '" + Name
                        + "' and BuyerMiddleName = '" + MiddleName
                        + "' and BuyerLastName = '" + LastName + "';";

            SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                BuyerID = reader[0].ToString();
            }

            queryString = @"select b.BuyerID, 
                                bo.OrderID, i.ItemName, bo.Amount, bo.BuyTime, bo.TotalPrice
                                from (Buyer as b inner join BuyOrder as bo on b.BuyerID = bo.BuyerID) 
                                inner join Item as i on i.ItemID = bo.ItemID
                                where b.BuyerID = '" + BuyerID + "';";

            command = new SqlCommand(queryString, ConnectionManager.Connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string data = reader[2].ToString() + ", " + reader[1].ToString();
                    dropdown.options.Add(new Dropdown.OptionData(data, null));
                }
            }
            dropdown.value = -1;
        }
    }
    public void SetChoices(int args)
    {

        string Name = Buyer.text.Split(' ')[0];
        string LastName = Buyer.text.Split(' ')[2];
        string MiddleName = Buyer.text.Split(' ')[1];

        string queryString = @"SELECT [BuyerID]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]
                                WHERE BuyerName = '" + Name
                    + "' and BuyerMiddleName = '" + MiddleName
                    + "' and BuyerLastName = '" + LastName + "';";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            reader.Read();
            BuyerID = reader[0].ToString();
        }

        queryString = @"select b.BuyerID, 
                                bo.OrderID, i.ItemName, bo.Amount, bo.BuyTime, bo.TotalPrice
                                from (Buyer as b inner join BuyOrder as bo on b.BuyerID = bo.BuyerID) 
                                inner join Item as i on i.ItemID = bo.ItemID
                                where b.BuyerID = '" + BuyerID + "';";

        command = new SqlCommand(queryString, ConnectionManager.Connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string data = reader[2].ToString() + ", " + reader[1].ToString();
                dropdown.options = new List<Dropdown.OptionData>();
                dropdown.options.Add(new Dropdown.OptionData(data, null));
            }
        }
        dropdown.value = -1;
    }
}
