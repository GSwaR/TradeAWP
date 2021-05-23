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

    public Text Buyer;
    public Text WarrantyID;
    public Text WarrantyTypeID;
    public Text ItemName;
    public Text BuyTime;
    public Text Term;
    public string BuyerID;

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (WarrantyID.text == "" || Term.text == "")
        {
            ConnectionManager.errorHandler.Handle("Input fields can't be empty!");
        }
        else
        {
            string ItemName_ = ItemName.text.Split(',')[0];

            string queryString = @"SELECT
                                [ItemName]
                                ,[ItemID]
                                FROM [TradeManagerConsole].[dbo].[Item]
                                WHERE ItemName = '" + ItemName_ + "';";
            SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
            string ItemID;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                ItemID = reader[1].ToString();
            }

            string Name = Buyer.text.Split(' ')[0];
            string LastName = Buyer.text.Split(' ')[2];
            string MiddleName = Buyer.text.Split(' ')[1];

            queryString = @"SELECT [BuyerID]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]
                                WHERE BuyerName = '" + Name 
                                + "' and BuyerMiddleName = '" + MiddleName 
                                + "' and BuyerLastName = '" + LastName + "';";

            command = new SqlCommand(queryString, ConnectionManager.Connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                BuyerID = reader[0].ToString();
            }

            queryString = @"SELECT [WarrantyTypeID]
                          ,[WarrantyTypeName]
                          FROM [TradeManagerConsole].[dbo].[WarrantyType]
                          WHERE WarrantyTypeName = '" + WarrantyTypeID.text +"';";
            string WarrantyType;
            command = new SqlCommand(queryString, ConnectionManager.Connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                WarrantyType = reader[0].ToString();
            }

            SqlDateTime dateTime = DateTime.Now;
            try
            {
                queryString = @"insert into Warranty
                                    values
                                    ('" + WarrantyID.text + "', " +
                                    "'" + BuyerID +
                                    "', '" + WarrantyType + "', '" +
                                    ItemID + "', convert(date, '" + dateTime + "'), " + int.Parse(Term.text) + ");";

                command = new SqlCommand(queryString, ConnectionManager.Connection);
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                ConnectionManager.errorHandler.Handle(e.Message);
            }

        }
    }
}
