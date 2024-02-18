<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpRegistration.aspx.cs" Inherits="Self.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EmpRegistration</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h2 class="text-center">Employee Registration</h2>

             <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="txtName">Name</label>
                     <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name" required></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name should be in Characters!!" 
                         ForeColor="Red" ValidationExpression="^[a-zA-Z\s]+$" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtName">
                     </asp:RegularExpressionValidator>
                </div>

                <div class="col-md-6">
                    <label for="txtDOB">Date of Birth</label>
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" required></asp:TextBox>
                </div>

            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="ddlGender">Gender</label>
                     <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                         <asp:ListItem Value="0">Select Gender</asp:ListItem>
                         <asp:ListItem>Male</asp:ListItem>
                         <asp:ListItem>Female</asp:ListItem>
                         <asp:ListItem>Other</asp:ListItem>
                     </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Gender is Required!" 
                         ForeColor="Red" ControlToValidate="ddlGender" Display="Dynamic" SetFocusOnError="true" InitialValue="Select Gender">
                     </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtMobile">Contact No.</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TextMode="Number" placeholder="10 Digits Mobile No." required></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Mobile No.!!" 
                         ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                    </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="txtEmail">Email</label>
                     <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" TextMode="Email" required></asp:TextBox>
                     
                </div>
                <div class="col-md-6">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="txtState">State</label>
                     <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="State is Required !!" 
                         ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red" InitialValue="Select State" 
                         SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="ddlDistrict">District</label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" ></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="District is Required !!" 
                         ControlToValidate="ddlDistrict" Display="Dynamic" ForeColor="Red" InitialValue="Select District" 
                         SetFocusOnError="True"></asp:RequiredFieldValidator>              
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-12">
                    <label for="txtAddress">Address</label>
                     <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" required></asp:TextBox>                    
                </div>                
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c9" Text="Submit" OnClick="btnAdd_Click"  />
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display!" 
                        AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="EmployeeId"
                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="4" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sr. No" HeaderText="Sr. No" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:TemplateField HeaderText="Name">
                                <EditItemTemplate>
                                   <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control" Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mobile">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMobileEdit" runat="server" Text='<%# Eval("Mobile") %>' CssClass="form-control" Width="100px "></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Email"> 
                                 <EditItemTemplate>
                                    <asp:TextBox ID="txtEmailEdit" runat="server" Text='<%# Eval("Email") %>' CssClass="form-control" Width="100px "></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Password">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPasswordEdit" runat="server" Text='<%# Eval("Password") %>' CssClass="form-control" Width="100px "></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="State">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStateGv" runat="server" DataSourceID="SqlDataSource1" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Eval("StateId") %>' CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStateGv_SelectedIndexChanged">
                                        <asp:ListItem>Select State</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationCS %>" SelectCommand="SELECT * FROM [State]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="District">
                                <EditItemTemplate>
                                           <asp:DropDownList ID="ddlDistrictGv" runat="server" CssClass="form-control">
                                    </asp:DropDownList>         
                                    </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrictName" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Eval("Address") %>' CssClass="form-control" Width="130px" TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:CommandField HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="true" CausesValidation="false">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#5558c9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
