using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class UserNameLoader : MonoBehaviour
{
    public Text Username;
    public Text LoginUsername;
    public ConnectionManager ConnectionManager;

    private void Start()
    {
        Username.text = LoginUsername.text;
        if (ConnectionManager.Membership == "owner")
        {
            Username.text += ", Administrator";
        }
        else if (ConnectionManager.Membership == "writer")
        {
            Username.text += ", Manager";
        }
        else
        {
            Username.text += ", Consultant"; 
        }
    }
}
