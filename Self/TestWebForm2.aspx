<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWebForm2.aspx.cs" Inherits="Self.TestWebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-lg-5 p-md-5">
            <div>
                <asp:Label ID="lblmsg" runat="server" ></asp:Label>
            </div>
            <h2 class="text-center">TestWebForm2</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" CssClass="form-control" required></asp:TextBox>
                </div>
                 <div class="col-md-6">
                    <label for="txtMobile">Mobile</label>
                    <asp:TextBox ID="txtMobile" runat="server" placeholder="Enter Mobile" CssClass="form-control" TextMode="Number" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                     <label for="ddlState">State</label>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="State is required !!" CssClass="form-control" ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red" InitialValue="Select State" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                     <label for="ddlDistrict">District</label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="District is required !!" CssClass="form-control" ControlToValidate="ddlDistrict" Display="Dynamic" ForeColor="Red" InitialValue="Select District" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 ">
                <div class="col-md-6">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c9" Text="Submit" OnClick="btnAdd_Click" />
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display!" 
                        AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="Id"
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
