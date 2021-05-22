using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class TableWarrantyTypeGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString = @"SELECT [WarrantyTypeID]
                                ,[WarrantyTypeName]
                                FROM [TradeManagerConsole].[dbo].[WarrantyType]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> WarrantyTypeID = new List<string>();
        List<string> WarrantyTypeName = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                WarrantyTypeID.Add(reader[0].ToString());
                WarrantyTypeName.Add(reader[1].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + WarrantyTypeID.Count * 50);

        CreateAndSetColumn(WarrantyTypeID, 0);
        CreateAndSetColumn(WarrantyTypeName, 1);

    }
}
