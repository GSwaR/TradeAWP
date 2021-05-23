using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
public class DropDownMenu : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    public Dropdown dropdown;

    protected void OnEnable()
    {
        SetChoices();
    }

    public virtual void SetChoices()
    {

    }

    public virtual void SetChoices(string queryString, int column)
    {
        SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string data = reader[column].ToString();
                dropdown.options.Add(new Dropdown.OptionData(data, null));
            }
        }
    }
}
