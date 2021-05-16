using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class TableItemTypeGetter : TableBrandGetter
{
    private void OnEnable()
    {
        OnClick();
    }

    public override void OnClick()
    {
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

        Vector3 column1 = Columns[0].transform.position;
        Vector3 column2 = Columns[1].transform.position;

        foreach (string s in ItemTypeID)
        {
            column1.y -= Columns[0].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[0], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = column1;
            columnObject.GetComponentInChildren<Text>().text = s;
        }

        foreach (string s in ItemTypeName)
        {
            column2.y -= Columns[1].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[1], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = column2;
            columnObject.GetComponentInChildren<Text>().text = s;
        }

    }
}
