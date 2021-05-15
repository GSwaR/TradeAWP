using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class AddNewBuyer : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    public Button Button;

    public Text BuyerID;
    public Text Phone;
    public Text Name;
    public Text LastName;
    public Text MiddleName;

    private void Awake()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (BuyerID.text == "" || Phone.text == "" || Name.text == "" || LastName.text == "" || MiddleName.text == "")
        {
            ConnectionManager.errorHandler.Handle("Input fields can't be empty!");
        }
        else
        {
            string queryString =  @"insert into Buyer
                                    values
                                    ('"+ BuyerID.text +"', " +
                                    "'"+ Phone.text +
                                    "', '"+ Name.text +"', '"+
                                    LastName.text +"', '"+ MiddleName.text +"');";
            try
            {
                SqlCommand command = new SqlCommand(queryString, ConnectionManager.Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ConnectionManager.errorHandler.Handle(e.Message);
            }
        }
    }
}
