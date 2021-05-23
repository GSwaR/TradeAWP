using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlTypes;


public class GetDataButtonOrders : TableBrandGetter
{
    public Text Buyer;
    public Button Button;

    private void Start()
    {
        Button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        transforms = new Vector3[Columns.Length];

    }

    public override void OnClick()
    {
        ColumnObject[] objs = Content.GetComponentsInChildren<ColumnObject>();
        foreach (ColumnObject obj in objs)
        {
            obj.OnDisable();
        }
        SetTransforms();

        string Name = Buyer.text.Split(' ')[0];
        string LastName = Buyer.text.Split(' ')[2];
        string MiddleName = Buyer.text.Split(' ')[1];

        string queryString = @"SELECT [BuyerID]
                                ,[BuyerName]
                                ,[BuyerMiddleName]
                                ,[BuyerLastName]
                                FROM [TradeManagerConsole].[dbo].[Buyer]
                                WHERE BuyerName = '" + Name
                    + "' and BuyerMiddleName = '" + MiddleName
                    + "' and BuyerLastName = '" + LastName + "';";
        string BuyerID;
        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
        using (SqlDataReader reader = command.ExecuteReader())
        {
            reader.Read();
            BuyerID = reader[0].ToString();
        }

        queryString =   @"select bo.OrderID, 
                        bo.BuyTime, i.ItemName, bo.Amount, i.Price*bo.Amount
                        from (Buyer as b inner join BuyOrder as bo on b.BuyerID = bo.BuyerID) 
                        inner join Item as i on i.ItemID = bo.ItemID
                        where b.BuyerID = '" + BuyerID + "';";

        command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> OrderID = new List<string>();
        List<string> BuyTime = new List<string>();
        List<string> ItemName = new List<string>();
        List<string> Amount = new List<string>();
        List<string> Price = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                OrderID.Add(reader[0].ToString());
                BuyTime.Add(reader[1].ToString());
                ItemName.Add(reader[2].ToString());
                Amount.Add(reader[3].ToString());
                Price.Add(float.Parse(reader[3].ToString()) * float.Parse(reader[4].ToString()) + "");
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + OrderID.Count * 50);

        CreateAndSetColumn(OrderID, 0);
        CreateAndSetColumn(BuyTime, 1);
        CreateAndSetColumn(ItemName, 2);
        CreateAndSetColumn(Amount, 3);
        CreateAndSetColumn(Price, 4);

    }
}
