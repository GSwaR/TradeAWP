using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TableBuyOrderGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString =    @"SELECT [OrderID]
                                ,[BuyerID]
                                ,[ManagerID]
                                ,[ItemID]
                                ,[BuyTime]
                                ,[Amount]
                                ,[TotalPrice]
                                FROM [TradeManagerConsole].[dbo].[BuyOrder]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> OrderID = new List<string>();
        List<string> BuyerID = new List<string>();
        List<string> ManagerID = new List<string>();
        List<string> ItemID = new List<string>();
        List<string> BuyTime = new List<string>();
        List<string> Amount = new List<string>();
        List<string> TotalPrice = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                OrderID.Add(reader[0].ToString());
                BuyerID.Add(reader[1].ToString());
                ManagerID.Add(reader[2].ToString());
                ItemID.Add(reader[3].ToString());
                BuyTime.Add(reader[4].ToString());
                Amount.Add(reader[5].ToString());
                TotalPrice.Add(reader[6].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + ItemID.Count * 50);

        CreateAndSetColumn(OrderID, 0);
        CreateAndSetColumn(BuyerID, 1);
        CreateAndSetColumn(ManagerID, 2);
        CreateAndSetColumn(ItemID, 3);
        CreateAndSetColumn(BuyTime, 4);
        CreateAndSetColumn(Amount, 5);
        CreateAndSetColumn(TotalPrice, 6);
    }
}
