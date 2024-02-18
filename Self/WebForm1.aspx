<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Self.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test</title>

     <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMSG" runat="server" ></asp:Label>
            </div>
            <h2 class="text-center">Test Registration</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name!" required></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name should be in character" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtName" CssClass="form-control" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtDOB">Date of Birth</label>
                    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control" required></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Gender is required!!" ControlToValidate="ddlGender" Display="Dynamic" ForeColor="Red" InitialValue="Select Gender" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtMobile">Mobile</label>
                    <asp:TextBox ID="txtMobile" runat="server" placeholder="10 digits mobile no!" CssClass="form-control" TextMode="Number" required></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Mobile Number!" ControlToValidate="txtMobile" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{10}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">               
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email!" TextMode="Email" required></asp:TextBox>  
                     </div>
                 <div class="col-md-6">               
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password!" required TextMode="Password"></asp:TextBox>  
                     </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-12">
                    <label for="txtAddress">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address!" TextMode="MultiLine" requird ></asp:TextBox>
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558c9" Text="Submit" OnClick="btnAdd_Click"   />
                </div>
            </div>

             <div class="row mb-3 mr-lg-5 ml-lg-5">
                 <div class="col-md-12">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table tabl-hover table-bordered" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Id" EmptyDataText="no record to display" PageSize="4" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                     <Columns>
                         <asp:BoundField DataField="Sr. No." HeaderText="Sr. No." ReadOnly="True"  >
                         <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                           
                         <asp:TemplateField HeaderText="Name">
                             <EditItemTemplate>
                                 <asp:TextBox ID="txtNameEdit" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>' Width="100px"></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>                 
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Mobile">
                             <EditItemTemplate>
                                 <asp:TextBox ID="txtMobileEdit" runat="server" CssClass="form-control" Text='<%# Eval("Mobile") %>' Width="100px"  ></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>

                           <asp:TemplateField HeaderText="Email">                              
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

                         <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Eval("Address") %>' CssClass="form-control" Width="100px" TextMode="MultiLine"></asp:TextBox>
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
