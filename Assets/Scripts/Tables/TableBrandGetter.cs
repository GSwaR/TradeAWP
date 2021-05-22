using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class TableBrandGetter : MonoBehaviour
{
    public ConnectionManager ConnectionManager;

    public GameObject[] Columns;
    public GameObject[] ColumnObjects;
    public GameObject Content;
    protected Vector3[] transforms;

    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];
        OnClick();
    }

    protected void SetTransforms()
    {
        for (int i = 0; i < Columns.Length; i++)
        {
            transforms[i] = Columns[i].transform.position;
        }
    }

    public virtual void OnClick()
    {
        SetTransforms();

        string queryString = @"SELECT [BrandID]
                                ,[BrandName]
                                FROM [TradeManagerConsole].[dbo].[Brand]";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> BrandID = new List<string>();
        List<string> BrandName = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                BrandID.Add(reader[0].ToString());
                BrandName.Add(reader[1].ToString());
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + BrandID.Count*50);

        CreateAndSetColumn(BrandID, 0);
        CreateAndSetColumn(BrandName, 1);
    }

    protected void CreateAndSetColumn(List<string> list, int i)
    {
        foreach (string s in list)
        {
            transforms[i].y -= Columns[i].GetComponent<RectTransform>().transform.localScale.y * 1.5f;
            GameObject columnObject = Instantiate(ColumnObjects[i], Content.transform);
            RectTransform rectTransform = columnObject.GetComponent<RectTransform>();
            rectTransform.transform.position = transforms[i];
            columnObject.GetComponentInChildren<Text>().text = s;
        }
    }
}
