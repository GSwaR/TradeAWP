using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TableManagerGetter : TableBrandGetter
{
    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    public override void OnClick()
    {
        SetTransforms();
        string queryString =    @"SELECT [ManagerID]
                                ,[Phone]
                                ,[ManagerName]
                                ,[ManagerMiddleName]
                                ,[ManagerLastName]
                                FROM [TradeManagerConsole].[dbo].[Manager]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> ManagerID = new List<string>();
        List<string> Phone = new List<string>();
        List<string> Manager = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                ManagerID.Add(reader[0].ToString());
                Phone.Add(reader[1].ToString());
                Manager.Add(reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + ManagerID.Count * 50);

        CreateAndSetColumn(ManagerID, 0);
        CreateAndSetColumn(Phone, 1);
        CreateAndSetColumn(Manager, 2);

    }
}
