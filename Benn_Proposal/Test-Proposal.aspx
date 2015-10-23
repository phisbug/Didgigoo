<%@ Page Title="" Language="C#" MasterPageFile="~/Master-Proposal.master" AutoEventWireup="true"
    CodeFile="Test-Proposal.aspx.cs" Inherits="Test_Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageFunction" runat="Server">
    <asp:RadioButtonList ID="CompanyRadioBtnList" runat="server" RepeatDirection="Horizontal"
        OnSelectedIndexChanged="CompanyRadioBtnList_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Text="Add Proposal" Value="AddProp"></asp:ListItem>
        <asp:ListItem Text="Search Proposal" Value="SearchProp"></asp:ListItem>
        <asp:ListItem Text="Proposal Setting" Value="PropSetting"></asp:ListItem>
        <asp:ListItem Text="Print Templates" Value="PrintTemp"></asp:ListItem>
        <asp:ListItem Text="Trip Templates" Value="TripTemp"></asp:ListItem>
    </asp:RadioButtonList>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenuTitle" runat="Server">
    <h1>
        <asp:Label ID="Titlelbl" runat="server" Text="Proposal"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenuButtons" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSidebar" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </AjaxControlToolkit:ToolkitScriptManager>
    <div class="filters-buttons-container">
        <asp:Button ID="Button1" runat="server" Text="Clear Filters" CssClass="btn-gray" />
        <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn-green" OnClick="Button2_Click" />
    </div>
    <div class="filters-accordion">
        <section class="filterWrap">
        <h3>
            Keywords<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>
                    Enter Keywords</p>
                <asp:TextBox ID="TextboxKeywords" runat="server"></asp:TextBox>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Date Added<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        From:</div>
                    <div class="item">
                        <asp:ImageButton ID="Image1NEW" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxDateAddedFrom" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        To:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxDateAddedTo" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender1NEW" runat="server" PopupButtonID="Image1NEW"
                    TargetControlID="TextboxDateAddedFrom">
                </AjaxControlToolkit:CalendarExtender>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                    TargetControlID="TextboxDateAddedTo">
                </AjaxControlToolkit:CalendarExtender>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Date Updated<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        From:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxDateUpdatedFrom" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        To:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxDateUpdatedTo" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                    TargetControlID="TextboxDateUpdatedFrom">
                </AjaxControlToolkit:CalendarExtender>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton3"
                    TargetControlID="TextboxDateUpdatedTo">
                </AjaxControlToolkit:CalendarExtender>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Company<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>
                    Select Company</p>
                <asp:DropDownList ID="DDLProposalCompany" runat="server">
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Agent<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>
                    Select Agent</p>
                <asp:DropDownList ID="DDLProposalAgent" runat="server">
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Type<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>
                    Select Type</p>
                <asp:DropDownList ID="DDLProposalType" runat="server">
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Status<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>Select Statuses</p>
                <asp:CheckBoxList ID="CheckboxStatuses" runat="server">

                </asp:CheckBoxList>
            </div>
        </div>
        </section>
        <section>
        <h3>
            Proposal Next Action<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <p>Select Next Action</p>
                <asp:CheckBoxList ID="CheckBoxNextAction" runat="server">
                </asp:CheckBoxList>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Start Date<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        From:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxProposalStartDateFrom" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        To:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxProposalStartDateTo" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton4"
                    TargetControlID="TextboxProposalStartDateFrom">
                </AjaxControlToolkit:CalendarExtender>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="ImageButton5"
                    TargetControlID="TextboxProposalStartDateTo">
                </AjaxControlToolkit:CalendarExtender>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal End Date<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        From:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton6" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxProposalEndDateFrom" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <div class="sidebar-datePickers">
                    <div class="item">
                        To:</div>
                    <div class="item">
                        <asp:ImageButton ID="ImageButton7" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/calendar-icon.png"
                            CausesValidation="false" Width="20" /></div>
                    <div class="item">
                        <asp:TextBox ID="TextboxProposalEndDateTo" runat="server" ReadOnly="true"></asp:TextBox></div>
                </div>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="ImageButton6"
                    TargetControlID="TextboxProposalEndDateFrom">
                </AjaxControlToolkit:CalendarExtender>
                <AjaxControlToolkit:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="ImageButton7"
                    TargetControlID="TextboxProposalEndDateTo">
                </AjaxControlToolkit:CalendarExtender>
            </div>
        </div>
        </section>
        <section class="filterWrap">
        <h3>
            Proposal Estimated Budget<i class="fa fa-caret-down"></i></h3>
        <div>
            <div>
                <div class="budget-slider-container">
                    <div id="budget-slider">
                    </div>
                    <p class="min">
                        <label for="budgetAmountMin">
                            Min</label>
                        <asp:TextBox ID="budgetAmountMin" runat="server" ReadOnly="true"></asp:TextBox>
                    </p>
                    <p class="max">
                        <label for="budgetAmountMax">
                            Max</label>
                        <asp:TextBox ID="budgetAmountMax" runat="server" ReadOnly="true"></asp:TextBox>
                    </p>
                </div>
            </div>
        </div>
        </section>
    </div>
    <!-- ./ close filters accordion div -->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:LoginView ID="proposalLoginView" runat="server">
        <LoggedInTemplate>
            <h3>
                User Name :
                <asp:LoginName ID="LoginName1" runat="server"></asp:LoginName>
            </h3>
        </LoggedInTemplate>
        <AnonymousTemplate>
            Not Logged In
        </AnonymousTemplate>
    </asp:LoginView>
    <!-- get user info -->
    <div>
        <asp:Literal ID="Literal1" runat="server">
            <div style="background:red;">
                <p>Agent Role - </p>
                <p>Agent Company - </p>
            </div>
        </asp:Literal>
    </div>
    <hr />
    
    <%--Panels--%>
    <div id="PanelContainer" runat="server">
    
        <!-- SEARCH PROPOSAL PANEL -->
        <asp:Panel ID="PropSearchPanel" runat="server">
            
            <asp:GridView ID="GridViewSearchProposal" runat="server">
            </asp:GridView>
            
        </asp:Panel>
        
        
        <!-- ADD PROPOSAL PANEL -->
        <asp:Panel ID="PropAddPanel" runat="server">
            <h1>
                Step 1 : Proposal Basics</h1>
            <hr />
        </asp:Panel>
        <!-- PROPOSAL SETTINGS PANEL -->
        <asp:Panel ID="PropSettingPanel" runat="server">
            <p>
                Prop Setting</p>
        </asp:Panel>
        <!-- PRINT TEMPLATES PANEL -->
        <asp:Panel ID="PropPrintPanel" runat="server">
            <p>
                Print</p>
        </asp:Panel>
        <!-- PROPOSAL TRIP TEMPLATES PANEL -->
        <asp:Panel ID="PropTripPanel" runat="server">
            <p>
                Trip</p>
        </asp:Panel>
    </div>
    <!-- ./ close div panel container -->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphCustomScripts" runat="Server">

    <script>
        $(function()
        {
            //jQuery code here


            $('.filters-accordion input:text').keypress(function(event)
            {
                if (event.keyCode == 13)
                {
                    return false;
                }

            });

            var maxV = 9999;
            var minV = 0;

            $("#budget-slider").slider({
                range: true,
                min: minV,
                max: maxV,
                values: [75, 9999],
                slide: function(event, ui)
                {
                    $("#<%= budgetAmountMin.ClientID %>").val(ui.values[0]);
                    $("#<%= budgetAmountMax.ClientID %>").val(ui.values[1]);
                }
            });

            $("#<%= budgetAmountMin.ClientID %>").on("keyup", function(e)
            {
                if (this.value > maxV)
                {
                    this.value = maxV;
                }

                if (this.value < minV)
                {
                    this.value = minV;
                }


                $("#budget-slider").slider("values", 0, this.value);
            });

            $("#<%= budgetAmountMax.ClientID %>").on("keyup", function(e)
            {
                if (this.value > maxV)
                {
                    this.value = maxV;
                }

                if (this.value < minV)
                {
                    this.value = minV;
                }

                $("#budget-slider").slider("values", 1, this.value);
            });


            $("#<%= budgetAmountMin.ClientID %>").val($("#budget-slider").slider("values", 0));
            $("#<%= budgetAmountMax.ClientID %>").val($("#budget-slider").slider("values", 1));

        });
    </script>

</asp:Content>
