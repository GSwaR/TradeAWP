using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TableItemGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString =    @"SELECT [ItemID]
                                ,[ItemName]
                                ,[Price]
                                ,[PriceDate]
                                ,[BrandID]
                                FROM [TradeManagerConsole].[dbo].[Item]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> ItemID = new List<string>();
        List<string> ItemName = new List<string>();
        List<string> Price = new List<string>();
        List<string> PriceDate = new List<string>();
        List<string> BrandID = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                ItemID.Add(reader[0].ToString());
                ItemName.Add(reader[1].ToString());
                Price.Add(reader[2].ToString());
                PriceDate.Add(reader[3].ToString());
                BrandID.Add(reader[4].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + ItemID.Count * 50);

        CreateAndSetColumn(ItemID, 0);
        CreateAndSetColumn(ItemName, 1);
        CreateAndSetColumn(Price, 2);
        CreateAndSetColumn(PriceDate, 3);
        CreateAndSetColumn(BrandID, 4);

    }
}
