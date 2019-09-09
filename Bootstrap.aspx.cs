using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class Bootstrap : System.Web.UI.Page
{
    //Initializing Variables
    int SalesReasonID = 0;
    string saleName = "";
    string reasonType = "";
    string foundReason = "";
    string foundName = "";

    //Creating database connection
    System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Checking to see if connection string is valid
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        try
        {
            sc.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            //    sc.ConnectionString = @"Server=LEASPC\MSSQLSERVER01; Database=AdventureWorks2014; Integrated Security = True; Trusted_Connection = Yes";
        }
        catch
        {
            MessageBox.Show("Error");
        }

        if (IsPostBack)
        {
            SalesDropDown_SelectedIndexChanged(sender, e);
        }
    }


    //Populating the textboxes when an option from the dropdown is selected
    protected void SalesDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        sc.Open();
        String queryGetName = "SELECT Name FROM [AdventureWorks2014].[Sales].[SalesReason] WHERE SalesReasonID = @SalesReasonID";
        SqlCommand cmdGetName = new SqlCommand(queryGetName, sc);
        cmdGetName.Parameters.AddWithValue("@SalesReasonID", HttpUtility.HtmlEncode(SalesDropDown.Text));
        String name = HttpUtility.HtmlEncode((String)cmdGetName.ExecuteScalar());
        NameTextbox.Text = name;
        sc.Close();

        sc.Open();
        String queryGetReasonType = "SELECT ReasonType FROM [AdventureWorks2014].[Sales].[SalesReason] WHERE SalesReasonID = @SalesReasonID";
        SqlCommand cmdGetReason = new SqlCommand(queryGetReasonType, sc);
        cmdGetReason.Parameters.AddWithValue("@SalesReasonID", HttpUtility.HtmlEncode(SalesDropDown.Text));
        String reason = HttpUtility.HtmlEncode((String)cmdGetReason.ExecuteScalar());
        SaleReasonTextbox.Text = reason;
        sc.Close();
    }

    //Commit Button Actions
    protected void CommitButton_Click(object sender, EventArgs e)
    {
        saleName = NameTextbox.Text;
        reasonType = SaleReasonTextbox.Text;

        //Validation 
        if (NameTextbox.Text == "" | NameTextbox.Text == " " | NameTextbox.Text == null)
        {
            MessageBox.Show("Please enter sale name!");
            return;
        }

        if (SaleReasonTextbox.Text == "" | SaleReasonTextbox.Text == " " | NameTextbox.Text == null)
        {
            MessageBox.Show("Please enter a sale reason!");
            return;
        }

        //Check to see if a certain name exists in the DB
        SqlCommand command;
        sc.Open();
        command = new SqlCommand("SELECT COUNT(*) FROM [AdventureWorks2014].[Sales].[SalesReason] WHERE [Name] = @Name", sc);
        command.Parameters.AddWithValue("@Name", HttpUtility.HtmlEncode(NameTextbox.Text));
        int ReasonExist = (int)command.ExecuteScalar();
        sc.Close();

        if (ReasonExist > 0)
        {
            MessageBox.Show("This reason already exists");
        }
        else
        {
            SalesReason salesReason = new SalesReason(SalesReasonID, saleName, reasonType);
            //Open database connection
            sc.Open();
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            insert.Connection = sc;

            insert.CommandText = "INSERT INTO SALES.SALESREASON VALUES (@Name,@ReasonType,@ModifiedDate); SELECT SCOPE_IDENTITY()";
            insert.Parameters.Add(new SqlParameter("@Name", salesReason.getSaleName()));
            insert.Parameters.Add(new SqlParameter("@ReasonType", salesReason.getReasonType()));
            insert.Parameters.Add(new SqlParameter("@ModifiedDate", salesReason.getModifiedDate()));

            insert.ExecuteNonQuery();
            //Closes Connection
            sc.Close();
            UpdatedLabel.Text = "Modified Date: " + salesReason.getModifiedDate();
        }
    }

    //saves activity edits
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Today;
        string salereasonID = SalesDropDown.SelectedValue.ToString();
        sc.Open();
        System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
        insert.Connection = sc;

        //Validation 
        if (NameTextbox.Text == "" | NameTextbox.Text == " " | NameTextbox.Text == null)
        {
            MessageBox.Show("Please enter sale name!");
            return;
        }

        if (SaleReasonTextbox.Text == "" | SaleReasonTextbox.Text == " " | NameTextbox.Text == null)
        {
            MessageBox.Show("Please enter a sale reason!");
            return;
        }

        insert.CommandText = "UPDATE Sales.SalesReason SET Name= @Name,ReasonType=@ReasonType, ModifiedDate=@ModifiedDate WHERE SalesReasonID=@SaleReasonID ;";
        insert.Parameters.Add(new SqlParameter("@SaleReasonID", salereasonID));
        insert.Parameters.Add(new SqlParameter("@ModifiedDate", date));
        insert.Parameters.Add(new SqlParameter("@Name", NameTextbox.Text));
        insert.Parameters.Add(new SqlParameter("@ReasonType", SaleReasonTextbox.Text));


        insert.ExecuteNonQuery();
        //Closes co

        sc.Close();
    }


}