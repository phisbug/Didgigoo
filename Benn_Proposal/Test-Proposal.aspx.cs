using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Data.Linq;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;


public partial class Test_Proposal : System.Web.UI.Page
{
    Sessions appSession;

    DataClassesProposalDataContext dbClass = new DataClassesProposalDataContext();
    string currentPanelShown = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (dbClass == null)
        {
            throw new Exception("Error! Cannot connect to database!");
        }

        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("OlivLogin.aspx");
        }


        /////////// INIT METHODS

        //set up user and fill out information
        appSession = new Sessions(Session);

        //get user name
        appSession.UsersUsername = User.Identity.Name.ToString();

        //get user id GUID version
        string currentUserID = Membership.GetUser().ProviderUserKey.ToString();

        //get role
        appSession.UsersRole = System.Web.Security.Roles.GetRolesForUser(appSession.UsersUsername).FirstOrDefault();

        ////////////////////
        //get agent company
        ///////////////////

        //convert currentUserID to guid object
        Guid guiduserId = Guid.Empty;
        if (currentUserID != "")
        {
            guiduserId = new Guid(currentUserID);
        }

        //MAKE SURE THE LINQ CHECKS THE GUID is SAME IN QUERY
        var userCompany = from a in dbClass.aspnet_User2s
                          join b in dbClass.Benn_Proposal_Agents on a.UserId equals b.Agent_GUID
                          join c in dbClass.Benn_Proposal_Companies on b.Company_Id equals c.Proposal_Company_Id
                          where b.Agent_GUID == guiduserId
                          select new { c.Company_Name };

        if (userCompany.Any())
        {
            appSession.CurrentlySelectedCompany = userCompany.Single().ToString();
        }
        else
        {
            //not an agent prolly a system manager Enter God Mode
            // or other
            switch (appSession.UsersRole)
            {
                case "System_Manager":
                    appSession.CurrentlySelectedCompany = appSession.UsersRole + " = god mode can add proposal to any company";
                    break;

                case "Proposal_Agent":
                    appSession.CurrentlySelectedCompany = appSession.UsersRole + " = THIS USER HAS NO COMPANY YET: Proposal agent can add a proposal to ITS OWN company";
                    break;

                case "Company_Admin":
                    appSession.CurrentlySelectedCompany = appSession.UsersRole + " = THIS USER HAS NO COMPANY YET: Company Admin can add proposal to ITS OWN company";
                    break;

                default:
                    {
                        appSession.CurrentlySelectedCompany = appSession.UsersRole + " the type of user/role is not suppose to be able to add a proposal.";
                        //Response.Redirect("~/OlivLogin.aspx");
                        break;
                    }
            }
        }





        //ON PAGE LOAD NOT POSTBACK
        if (!IsPostBack)
        {


            //set up pages based on user type
            SetUp_UserType(appSession.UsersRole);

            //initialize the pages
            proposal_Init(appSession.UsersRole, currentUserID);






            Literal1.Text = appSession.CurrentlySelectedCompany + "<br/>" + "{Role Type: " + appSession.UsersRole + "}";


            //functions to populate sidebar
            populateSidebarCompanyDDL(DDLProposalCompany, dbClass);
            populateSidebarAgentsDDL(DDLProposalAgent, dbClass);
            populateSidebarProposalTypeDDL(DDLProposalType, dbClass);
            populateSidebarProposalStatusCheckbox(CheckboxStatuses, dbClass);
            populateSidebarProposalNextActionCheckbox(CheckBoxNextAction, dbClass);
            // close sidebar functions

            //string cs = ConfigurationManager.ConnectionStrings["didgigoConnectionString3"].ConnectionString;
        }
        else
        {
            //TODO on after postback


            //set up pages based on user type
            SetUp_UserType(appSession.UsersRole);

        }
    }



    ///Cust FUNCTIONS 
    ////****************************************** /

    //INIT METHOD USE ON START UP
    private void proposal_Init(string userRole, string currentUserId)
    {
        // check the type or role of the current logged in user
        // will change this later as switch statemtns if number of types of users increase


        switch (userRole)
        {

            case "Proposal_Agent":
                {
                    MainAddProposal();
                    CompanyRadioBtnList.SelectedValue = "AddProp";
                    break;
                }

            case "System_Manager":
                {
                    MainSearchProposal();
                    CompanyRadioBtnList.SelectedValue = "SearchProp";
                    break;
                }

            case "Company_Admin":
                {
                    MainSearchProposal();
                    CompanyRadioBtnList.SelectedValue = "SearchProp";
                    break;
                }

            default:
                {
                    MainOTHERTEMPLATE();
                    break;
                }

        }

    }




    //determine type of user or role of user hide controls accordingly
    private void SetUp_UserType(string a_userType)
    {
        switch (a_userType)
        {
            //UserType_SystemManager
            case "System_Manager":
                CompanyRadioBtnList.Items[0].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[1].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[2].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[3].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[4].Attributes.CssStyle.Add("display", "inline");
                break;

            //Proposal Agent
            case "Proposal_Agent":
                CompanyRadioBtnList.Items[0].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[1].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[2].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[3].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[4].Attributes.CssStyle.Add("display", "inline");
                break;

            //UserType_CompanyAdmin
            case "Company_Admin":
                CompanyRadioBtnList.Items[0].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[1].Attributes.CssStyle.Add("display", "inline");
                CompanyRadioBtnList.Items[2].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[3].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[4].Attributes.CssStyle.Add("display", "none");
                break;


            default:
                CompanyRadioBtnList.Items[0].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[1].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[2].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[3].Attributes.CssStyle.Add("display", "none");
                CompanyRadioBtnList.Items[4].Attributes.CssStyle.Add("display", "none");
                break;


        }
    }




    //setup panels
    private void ShowPanel(string a_panelName)
    {

        //int visiblePanel = 0; 1- add company, 2 - add agent 3 - search company 4 - search agent 5 - company setting
        switch (a_panelName)
        {
            case "prop-add":

                PropAddPanel.Visible = true;

                PropSearchPanel.Visible = false;

                PropSettingPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropTripPanel.Visible = false;

                currentPanelShown = "AddProposal";
                break;

            case "prop-search":

                PropAddPanel.Visible = false;

                PropSearchPanel.Visible = true;

                PropSettingPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropTripPanel.Visible = false;

                currentPanelShown = "SearchProposal";

                break;

            case "prop-setting":

                PropAddPanel.Visible = false;

                PropSearchPanel.Visible = false;

                PropSettingPanel.Visible = true;

                PropPrintPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropTripPanel.Visible = false;

                currentPanelShown = "SettingProposal";

                break;

            case "print-templates":

                PropAddPanel.Visible = false;

                PropSearchPanel.Visible = false;

                PropSettingPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropPrintPanel.Visible = true;

                PropTripPanel.Visible = false;

                currentPanelShown = "PrintTemplates";
                break;


            case "trip-templates":

                PropAddPanel.Visible = false;

                PropSearchPanel.Visible = false;

                PropSettingPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropPrintPanel.Visible = false;

                PropTripPanel.Visible = true;

                currentPanelShown = "TripTemplates";
                break;

            case "none":
                {
                    CompanyRadioBtnList.Visible = false;

                    PanelContainer.Visible = false;

                    break;
                }

            default: break;
        }

    }



    /****TOP RADIO BUTTONS determines which panels to show ***/
    private void MainAddProposal()
    {
        Titlelbl.Text = "Add Proposal";
        ShowPanel("prop-add");
        sidebarAction("hide");

        //multiviews
        MultiViewProposalAdd.ActiveViewIndex = 0;

    }


    private void MainSearchProposal()
    {
        Titlelbl.Text = "Search";
        ShowPanel("prop-search");
        sidebarAction("show");

        GridViewSearchProposal.DataSource = dbClass.Benn_Proposals;
        GridViewSearchProposal.DataBind();


    }

    private void MainProposalSetting()
    {
        Titlelbl.Text = "Settings";
        ShowPanel("prop-setting");
        sidebarAction("hide");

    }

    private void MainPrintTemplates()
    {
        Titlelbl.Text = "Print Templates";

        sidebarAction("hide");

        ShowPanel("print-templates");
    }

    private void MainTripTemplates()
    {
        Titlelbl.Text = "Trip Templates";
        ShowPanel("trip-templates");
        sidebarAction("hide");

    }

    private void MainOTHERTEMPLATE()
    {
        Titlelbl.Text = "UNAUTHORIZED";
        ShowPanel("none");
        sidebarAction("hide");
    }



    //Dropdown of Sidebar Company
    private void populateSidebarCompanyDDL(DropDownList ddl, DataClassesProposalDataContext db)
    {
        string dataTextField = "Company_Name";
        string dataTextValue = "Proposal_Company_Id";

        var proposalCompanies = from a in db.Benn_Proposal_Companies select a;

        if (proposalCompanies.Count() != 0)
        {
            ddl.DataSource = proposalCompanies.ToList();

            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataTextValue;

            ddl.DataBind();

            //add header
            ListItem header = new ListItem();
            header.Text = "--All Companies--";
            header.Value = "--All Companies--";
            ddl.Items.Insert(0, header);
            ddl.SelectedValue = "--All Companies--";
        }


    }

    //Dropdown of Sidebar Agents
    private void populateSidebarAgentsDDL(DropDownList ddl, DataClassesProposalDataContext db)
    {
        var proposalAgent = from a in db.Benn_Proposal_Agents select new { agentName = a.First_Name + " " + a.Last_Name, agentID = a.Agent_Id };

        string dataTextField = "agentName";
        string dataTextValue = "agentID";

        if (proposalAgent.Count() != 0)
        {
            ddl.DataSource = proposalAgent.ToList();

            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataTextValue;

            ddl.DataBind();

            //add header
            ListItem header = new ListItem();
            header.Text = "--All Agents--";
            header.Value = "--All Agents--";
            ddl.Items.Insert(0, header);
            ddl.SelectedValue = "--All Agents--";
        }


    }


    //Dropdown of Sidebar Proposal Type
    private void populateSidebarProposalTypeDDL(DropDownList ddl, DataClassesProposalDataContext db)
    {
        string dataTextField = "Type_Description";
        string dataTextValue = "Proposal_Type_Id";

        var proposalTypes = from a in db.Benn_Proposal_Types select a;

        if (proposalTypes.Count() != 0)
        {
            ddl.DataSource = proposalTypes.ToList();

            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataTextValue;

            ddl.DataBind();

            //add header
            ListItem header = new ListItem();
            header.Text = "--All Types--";
            header.Value = "--All Types--";
            ddl.Items.Insert(0, header);
            ddl.SelectedValue = "--All Types--";
        }


    }


    //Checkbox of Sidebar Proposal Status
    private void populateSidebarProposalStatusCheckbox(CheckBoxList cb, DataClassesProposalDataContext db)
    {


        var proposalStatus = from a in db.Benn_Proposal_Status select a;



        if (proposalStatus.Count() != 0)
        {

            //do something 
            foreach (Benn_Proposal_Status s in proposalStatus)
            {
                //CheckBox item = (CheckBox)Page.FindControl("chk" + activity.Activity_Id.ToString());
                ListItem item = new ListItem();
                item.Value = s.Proposal_Status_Id.ToString();//set item value to the status ID
                item.Text = s.Status_Description.ToString();//set item text to the status

                cb.Items.Add(item);//add our item to our checkbox list

            }
        }


    }


    //Checkbox of Sidebar Proposal Next Action
    private void populateSidebarProposalNextActionCheckbox(CheckBoxList cb, DataClassesProposalDataContext db)
    {


        var proposalNextAction = from a in db.Benn_Proposal_Next_Actions select a;



        if (proposalNextAction.Count() != 0)
        {

            //do something 
            foreach (Benn_Proposal_Next_Action s in proposalNextAction)
            {
                //CheckBox item = (CheckBox)Page.FindControl("chk" + activity.Activity_Id.ToString());
                ListItem item = new ListItem();
                item.Value = s.Proposal_Next_Action_Id.ToString();//set item value to the status ID
                item.Text = s.Next_Action_Description.ToString();//set item text to the status

                cb.Items.Add(item);//add our item to our checkbox list

            }
        }


    }



    //clear Filters
    private void clearFilters()
    {
        //TODO
    }


    //get proposal filtesr values in the sidebar
    private List<string> getProposalFilterValues()
    {
        //total
        List<string> total = new List<string>();


        //keyword textbox value
        string keyword = TextboxKeywords.Text;
        total.Add(keyword);

        //date added
        string dateAddedFrom = TextboxDateAddedFrom.Text;
        string dateAddedTo = TextboxDateAddedTo.Text;

        total.Add(dateAddedFrom);
        total.Add(dateAddedTo);

        //date update
        string dateUpdatedFrom = TextboxDateUpdatedFrom.Text;
        string dateUpdatedTo = TextboxDateUpdatedTo.Text;

        total.Add(dateUpdatedFrom);
        total.Add(dateUpdatedTo);

        //Proposal Company ddl
        string proposalCompany = DDLProposalCompany.SelectedItem.Text;
        total.Add(proposalCompany);

        // proposal agent
        string proposalAgent = DDLProposalAgent.SelectedItem.Text;
        total.Add(proposalAgent);

        // proposal type
        string proposalType = DDLProposalType.SelectedItem.Text;
        total.Add(proposalType);


        //status checkbox
        foreach (ListItem li in CheckboxStatuses.Items)
        {
            if (li.Selected)
            {
                total.Add(li.Text);
            }
        }


        //next action checkbox
        foreach (ListItem li in CheckBoxNextAction.Items)
        {
            if (li.Selected)
            {
                total.Add(li.Text);
            }
        }

        //proposal start date
        string startDateFrom = TextboxProposalStartDateFrom.Text;
        string startDateTo = TextboxProposalStartDateTo.Text;
        total.Add(startDateFrom);
        total.Add(startDateTo);


        //proposal end date
        string endDateFrom = TextboxProposalEndDateFrom.Text;
        string endDateTo = TextboxProposalEndDateTo.Text;
        total.Add(endDateFrom);
        total.Add(endDateTo);


        //budget
        string budgetMin = budgetAmountMin.Text;
        string budgetMax = budgetAmountMax.Text;
        total.Add(budgetMin);
        total.Add(budgetMax);

        return total;
    }



    // button for clear filter 
    protected void Button2_Click(object sender, EventArgs e)
    {
        List<string> myString = getProposalFilterValues();

    }


    //sideBar function will hide the sidebar based on visible panel
    private void sidebarAction(string action)
    {
        switch (action)
        {

            case "hide":
                this.Master.FindControl("sidebarID").Visible = false;
                HtmlGenericControl h = new HtmlGenericControl();
                h = (HtmlGenericControl)this.Master.FindControl("contentID");
                h.Attributes["class"] += " nosideBar";
                break;


            case "show":
                this.Master.FindControl("sidebarID").Visible = true;
                h = (HtmlGenericControl)this.Master.FindControl("contentID");
                h.Attributes["class"] = "content";
                break;

            default:
                this.Master.FindControl("sidebarID").Visible = true;
                h = (HtmlGenericControl)this.Master.FindControl("contentID");
                h.Attributes["class"] = "content";
                break;
        }
    }

    //radio button changed on select choose panels to show
    protected void CompanyRadioBtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = CompanyRadioBtnList.SelectedValue;

        switch (selectedValue)
        {
            case "AddProp": MainAddProposal(); break;
            case "SearchProp": MainSearchProposal(); break;
            case "PropSetting": MainProposalSetting(); break;
            case "PrintTemp": MainPrintTemplates(); break;
            case "TripTemp": MainTripTemplates(); break;
        }
    }

    protected void btnStepOneCreate_Click(object sender, EventArgs e)
    {
        MultiViewProposalAdd.ActiveViewIndex = 1;
    }

    protected void btnStepTwoBack_Click(object sender, EventArgs e)
    {
        MultiViewProposalAdd.ActiveViewIndex = 0;
    }
}
