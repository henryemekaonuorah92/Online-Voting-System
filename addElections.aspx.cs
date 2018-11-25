using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addElections : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string str = ConfigurationManager.ConnectionStrings["votingdatabase"].ToString();
        con = new SqlConnection(str);
        con.Open();

        string one = startdate.Value;
        string two = starttime.Value;
        DateTime dt1 = Convert.ToDateTime(one + " " + two);

        one = enddate.Value;
        two = endtime.Value;
        DateTime dt2 = Convert.ToDateTime(one + " " + two);      

        c = 0;
        int localGovernment = 0;
        foreach (ListItem item in CheckBoxList2.Items)
        {
            if (item.Selected)
            {
                localGovernment = item.Selected.Value;
            }
            c++;
        }
        c = 0;
        int state = 0;
        foreach (ListItem item in CheckBoxList3.Items)
        {
            if (item.Selected)
            {
                state = item.Selected.Value;
            }
            c++;
        }

        string command = "insert into elections values('"+title.Value+"','"+dt1.ToString()+"','"+dt2.ToString()+"','"+description.Value+"',"+state+","+localGovernment+")";
        SqlCommand cmd = new SqlCommand(command, con);
        try {
            cmd.ExecuteNonQuery();
        }
        catch(SqlException exre)
        {
            Label1.Text = "Error. Maybe Election is already added?";
        }
        con.Close();
    }
}
