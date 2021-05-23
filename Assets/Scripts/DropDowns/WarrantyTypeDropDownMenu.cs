using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrantyTypeDropDownMenu : DropDownMenu
{
    private new void OnEnable()
    {
        string queryString =    @"SELECT [WarrantyTypeID]
                                ,[WarrantyTypeName]
                                FROM [TradeManagerConsole].[dbo].[WarrantyType]";

        SetChoices(queryString, 1);
    }
}
