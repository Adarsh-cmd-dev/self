<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestStudent.aspx.cs" Inherits="Self.TestStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Registration</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container p-lg-5 p-md-5">
            <div>
                <asp:Label ID="lblmsg" runat="server" ></asp:Label>
            </div>
            <h2 class="text-center">TestStudent</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name!!" CssClass="form-control" required></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtDOB">Date of Birth</label>
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDOB" runat="server" ErrorMessage="DOB is required!!" CssClass="form-control" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                 <div class="col-md-6">
                    <label for="txtRoll">Roll No.</label>
                    <asp:TextBox ID="txtRoll" runat="server" placeholder="Enter Roll!!" CssClass="form-control" required></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="ddlState">State</label>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">                       
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlState" InitialValue="Select State" runat="server" ErrorMessage="State is required!!" CssClass="form-control" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5 ">
                 <div class="col-md-3 col-md-offset-2 mb-3 ">
                     <asp:Button ID="btnAdd" runat="server" Text="Submit" CssClass="btn btn-primary btn-block" OnClick="btnAdd_Click" />
                 </div>
             </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5 ">
                 <div class="col-md-12">
                     <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Id" PageSize="4" EmptyDataText="no record display!!" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
                         <Columns>
                             <asp:BoundField DataField="Sr.No." HeaderText="Sr.No.">
                             <ItemStyle HorizontalAlign="Center" />
                             </asp:BoundField>

                             <asp:TemplateField HeaderText="Name">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtNameEdit" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>' Width="100px"></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="txtNameEdit" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="Date of Birth">
                                  <ItemTemplate>
                                     <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="Roll No.">
                                  <EditItemTemplate>
                                     <asp:TextBox ID="txtRollEdit" runat="server" CssClass="form-control" Text='<%# Eval("RollNo") %>' Width="100px"></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("RollNo") %>'></asp:Label>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="State">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStateEdit" runat="server" CssClass="form-control" Width="120px"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>

                             <asp:CommandField HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="True" CausesValidation="false">
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
