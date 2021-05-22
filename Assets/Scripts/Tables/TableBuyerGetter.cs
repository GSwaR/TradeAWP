using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class TableBuyerGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString =    @"SELECT [BuyerID]
                                ,[Phone]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> BuyerID = new List<string>();
        List<string> Phone = new List<string>();
        List<string> BuyerName = new List<string>();
        List<string> BuyerMiddleName = new List<string>();
        List<string> BuyerLastName = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                BuyerID.Add(reader[0].ToString());
                Phone.Add(reader[1].ToString());
                BuyerName.Add(reader[2].ToString());
                BuyerMiddleName.Add(reader[3].ToString());
                BuyerLastName.Add(reader[4].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + BuyerID.Count * 50);

        CreateAndSetColumn(BuyerID, 0);
        CreateAndSetColumn(Phone, 1);
        CreateAndSetColumn(BuyerName, 2);
        CreateAndSetColumn(BuyerMiddleName, 3);
        CreateAndSetColumn(BuyerLastName, 4);

    }
}
