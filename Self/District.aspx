<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="District.aspx.cs" Inherits="Self.District" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>District</title>
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
            <h2 class="text-center">New District</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="txtState">State</label>
                     <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="State is Required !!" 
                         ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red" InitialValue="Select State" 
                         SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtSubject">District</label>
                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" placeholder="Enter District" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c9" Text="Add District" OnClick="btnAdd_Click"  />
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-7">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display!" 
                        AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="DistrictId"
                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="4">
                        <Columns>
                            <asp:BoundField DataField="Sr. No" HeaderText="Sr. No" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="State">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Eval("StateId") %>' CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationCS %>" SelectCommand="SELECT * FROM [State]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDistrictEdit" runat="server" Text='<%# Eval("DistrictName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrictName" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Operation" ShowEditButton="True" CausesValidation="false">
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
