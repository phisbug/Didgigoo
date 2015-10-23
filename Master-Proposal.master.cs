using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

public partial class Master_Proposal : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] currentRole = Roles.GetRolesForUser();
        foreach (string s in currentRole)
        {
            curRole.Text = s;
        }
    }

    protected void btnSearchClicked(object sender, EventArgs e)
    {
        
    }

}
