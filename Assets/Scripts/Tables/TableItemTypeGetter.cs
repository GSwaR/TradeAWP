using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class TableItemTypeGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString =    @"SELECT [ItemTypeID]
                                ,[ItemTypeName]
                                FROM [TradeManagerConsole].[dbo].[ItemType]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> ItemTypeID = new List<string>();
        List<string> ItemTypeName = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                ItemTypeID.Add(reader[0].ToString());
                ItemTypeName.Add(reader[1].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + ItemTypeID.Count * 50);

        CreateAndSetColumn(ItemTypeID, 0);
        CreateAndSetColumn(ItemTypeName, 1);

    }
}
