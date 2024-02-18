<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="Self.State" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State</title>
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
            <h2 class="text-center">New State</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtClass">State Name</label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="Enter State Name" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c9" Text="Add State" OnClick="btnAdd_Click" />
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="StateId"
                        AutoGenerateColumns="False" EmptyDataText="No Record to Display !!" OnPageIndexChanging="GridView1_PageIndexChanging" 
                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="true" PageSize="4">
                        <Columns>
                            <asp:BoundField DataField="Sr. No" HeaderText="Sr. No" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="State">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStateEdit" runat="server" Text='<%# Eval("StateName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" />
                        </Columns>
                        <HeaderStyle BackColor="#5558c9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
