using System.Collections;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TableManagerGetter : TableBrandGetter
{
    private void OnEnable()
    {
        OnClick();
    }

    public override void OnClick()
    {
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

        Vector3 column1 = Columns[0].transform.position;
        Vector3 column2 = Columns[1].transform.position;
        Vector3 column3 = Columns[2].transform.position;

        foreach (string s in ManagerID)
        {
            column1.y -= Columns[0].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[0], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = column1;
            columnObject.GetComponentInChildren<Text>().text = s;
        }

        foreach (string s in Phone)
        {
            column2.y -= Columns[1].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[1], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = column2;
            columnObject.GetComponentInChildren<Text>().text = s;
        }

        foreach (string s in Manager)
        {
            column3.y -= Columns[2].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[2], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = column3;
            columnObject.GetComponentInChildren<Text>().text = s;
        }
    }
}
