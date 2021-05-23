using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;


public class ItemDropDownMenu : DropDownMenu
{
    private new void OnEnable()
    {
        SetChoices();
    }

    public override void SetChoices()
    {
        string queryString =    @"select i.ItemID, i.ItemName, b.BrandName
                                from (Brand as b inner join Item as i on b.BrandID = i.BrandID);";

        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string data = reader[1].ToString() + ", " + reader[2].ToString();
                dropdown.options.Add(new Dropdown.OptionData(data, null));
            }
        }
    }
}
