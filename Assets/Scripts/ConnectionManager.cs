using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine;
using System.Net.Sockets;

public class ConnectionManager : MonoBehaviour
{
    public SqlConnection Connection { get; private set; }

    public string Membership;

    public string InitialCatalog;
    public string DataSource;

    public LoginErrorHandler errorHandler;
    public State WorkFlowState;

    public StateManager StateManager;

    public void SetConnection(string user, string password, string dataSource)
    {
        if (dataSource.Length > 0)
        {
            DataSource = dataSource;
        }

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.UserID = user;
        builder.Password = password;
        builder.InitialCatalog = InitialCatalog;
        builder.DataSource = DataSource;

        try
        {
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
            SetMembership(user);

            StateManager.SetCurrentState(WorkFlowState);
        }
        catch(Exception e)
        {
            errorHandler.Handle(e.Message);
        }
    }

    public void CloseConnection()
    {
        Connection.Close();
        Connection = null;
    }

    private void SetMembership(string user)
    {
        string queryString = @"SELECT DP1.name AS DatabaseRoleName,   
                                    isnull(DP2.name, 'No members') AS DatabaseUserName
                                    FROM sys.database_role_members AS DRM
                                    RIGHT OUTER JOIN sys.database_principals AS DP1
                                    ON DRM.role_principal_id = DP1.principal_id
                                    LEFT OUTER JOIN sys.database_principals AS DP2
                                    ON DRM.member_principal_id = DP2.principal_id
                                    WHERE DP1.type = 'R' and DP2.name = " + "'" + user + "'" +
                                    " ORDER BY DP1.name; ";

        SqlCommand command = new SqlCommand(queryString, Connection);

        List<string> data = new List<string>(); 
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                data.Add(reader[0].ToString());
            }

            if (data.Contains("db_owner"))
            {
                Membership = "owner";
            }
            else if (data.Contains("db_datawriter"))
            {
                Membership = "writer";
            }
            else
            {
                Membership = "reader";
            }
        }
    }
}
