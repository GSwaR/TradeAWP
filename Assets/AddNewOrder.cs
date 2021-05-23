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

    public Text Buyer;
    public Text OrderID;
    public Text ManagerID;
    public Text Item;
    public Text BuyTime;
    public Text Amount;
    public Text Price;

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (OrderID.text == "" || ManagerID.text == "" || Amount.text == "" || Price.text == "")
        {
            ConnectionManager.errorHandler.Handle("Input fields can't be empty!");
        }
        else
        {
            string ItemName_ = Item.text.Split(',')[0];

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

            string BuyerID;

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

            SqlDateTime dateTime = DateTime.Now;
            queryString =           @"insert into BuyOrder
                                    values
                                    ('" + OrderID.text + "', " +
                                    "'" + BuyerID +
                                    "', '" + ManagerID.text + "', " + "'" + ItemID + "', " +
                                    "convert(date, '" + dateTime + "')," + int.Parse(Amount.text)+ ", " + float.Parse(Price.text) + ");";
            Debug.LogError(OrderID.text + " " + BuyerID + " " + ManagerID.text + " " + ItemID + " " + dateTime + " " + Amount.text + " " + Price.text);
            
            try
            {
                command = new SqlCommand(queryString, ConnectionManager.Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ConnectionManager.errorHandler.Handle(e.Message);
            }
        }
    }

}
