//I submit to the JMU Honor Code--Nicole Williamson & Leandra Rios
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DataMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Exits the application
    protected void Exit_Click(object sender, EventArgs e)
    {
        Environment.Exit(-1);
    }
}
