using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;


public class GetDataButton : TableBrandGetter
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

        queryString =   @"select w.WarrantyID, 
                        w.BuyTime, i.ItemName, w.Term
                        from (Buyer as b inner join Warranty as w on b.BuyerID = w.BuyerID) 
                        inner join Item as i on i.ItemID = w.ItemID
                        where b.BuyerID = '" + BuyerID + "';";

        command = new SqlCommand(queryString, ConnectionManager.Connection);

        List<string> WarrantyID = new List<string>();
        List<string> BuyTime = new List<string>();
        List<string> Item = new List<string>();
        List<string> Term = new List<string>();
        List<string> IsValid = new List<string>();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                WarrantyID.Add(reader[0].ToString());
                BuyTime.Add(reader[1].ToString());
                Item.Add(reader[2].ToString());
                Term.Add(reader[3].ToString());
                DateTime date = reader.GetDateTime(1);
                if (date.Month + int.Parse(reader[3].ToString()) >= 12)
                {
                    date = new DateTime(date.Year + 1, date.Month + int.Parse(reader[3].ToString()) - 12, date.Day);
                }
                else
                {
                    date = new DateTime(date.Year, date.Month + int.Parse(reader[3].ToString()), date.Day);
                }
                DateTime thisDate = DateTime.Now;
                if (date > thisDate)
                {
                    IsValid.Add("Valid");
                }
                else
                {
                    IsValid.Add("Invallid");
                }
            }
        }

        RectTransform contentSize = Content.GetComponent<RectTransform>();
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSize.sizeDelta.y + WarrantyID.Count * 50);

        CreateAndSetColumn(WarrantyID, 0);
        CreateAndSetColumn(BuyTime, 1);
        CreateAndSetColumn(Item, 2);
        CreateAndSetColumn(Term, 3);
        CreateAndSetColumn(IsValid, 4);

    }
}
