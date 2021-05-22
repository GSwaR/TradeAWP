using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TableWarrantyGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString = @"SELECT [WarrantyID]
                                ,[BuyerID]
                                ,[WarrantyTypeID]
                                ,[ItemID]
                                ,[BuyTime]
                                ,[Term]
                                FROM [TradeManagerConsole].[dbo].[Warranty]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> WarrantyID = new List<string>();
        List<string> BuyerID = new List<string>();
        List<string> WarrantyTypeID = new List<string>();
        List<string> ItemID = new List<string>();
        List<string> BuyTime = new List<string>();
        List<string> Term = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                WarrantyID.Add(reader[0].ToString());
                BuyerID.Add(reader[1].ToString());
                WarrantyTypeID.Add(reader[2].ToString());
                ItemID.Add(reader[3].ToString());
                BuyTime.Add(reader[4].ToString());
                Term.Add(reader[5].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + WarrantyID.Count * 50);

        CreateAndSetColumn(WarrantyID, 0);
        CreateAndSetColumn(BuyerID, 1);
        CreateAndSetColumn(WarrantyTypeID, 2);
        CreateAndSetColumn(ItemID, 3);
        CreateAndSetColumn(BuyTime, 4);
        CreateAndSetColumn(Term, 5);

    }
}
