<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="User.ascx.cs" Inherits="Mobile.Controls.Membership.User" %>

<div id="form_command">
    <asp:UpdatePanel runat="server" ID="upnFormCommand">
        <ContentTemplate>
            <asp:Button Text="Add new User" runat="server" ID="btnShowUserForm" OnClick="btnShowUserForm_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div class="onecolumn">
	<div class="header">
		<span><%= Mobile.Core.Web.ResxManager.GetResx("UserList") %></span>
	</div>
	<br class="clear"/>
	<div class="content">
        <div class="form_search">
            <asp:UpdatePanel runat="server" ID="upnCommand" UpdateMode="Conditional">
                <ContentTemplate>
                    <input id="user_keyword" />
                    <select id="user_delete_status">
                        <option value="false">Đang sử dụng</option>
                        <option value="true">Đã xoá</option>
                    </select>
                    <asp:DropDownList runat="server" ID="ddlCompanyListSearch" AutoPostBack="false" DataTextField="CompanyName" DataValueField="CompanyID">
                    </asp:DropDownList>                    
                    <button id="user_list_search">Search</button>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="UserList">
            <div id="user_list">
                <div id="user_list_data">
                    <asp:UpdatePanel runat="server" ID="upnUserList" UpdateMode="Conditional">
                        <ContentTemplate>                        
                            <asp:ListView ID="lvUsers" runat="server" ItemPlaceholderID="itemPlaceholder" >    
                                <LayoutTemplate>        
                                    <table border=0 style="width: 100%;" class="grid_data" cellpadding="0" cellspacing="0">
                                        <tr align=left> 
                                            <th>User ID </th>
                                            <th>Email</th>
                                            <th>Fullname</th>
                                            <th>Locked</th>
                                            <th></th>
                                        </tr>                      
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
                                      </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                      <tr> 
                                        <td><%# Eval("UserID")%></td> 
                                        <td><%# Eval("Email")%></td>
                                        <td><%# Eval("FullName")%></td>
                                        <td><%# Eval("IsLocked")%></td>
                                        <td style="width:40px"> 
                                            <asp:LinkButton ID="SelectCategoryButton" runat="server" Text="Select" CommandName="Select"/> 
                                        </td>
                                      </tr>
                                </ItemTemplate>    
                                <EmptyDataTemplate >
                                    <i><%= Mobile.Core.Web.ResxManager.GetResx("DataEmpty")%></i>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>    
</div>

<asp:UpdatePanel ID="upnUserForm" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlUserForm" CssClass="confirm-dialog" Width="40%"
            Style="display: none">
            <div class="Popup_Title">
                <table border="0" cellpadding="0" cellspacing="0" class="Popup_Title" style="width: 100%">
                    <tr>
                        <td class="Popup_Title">
                            <strong><asp:Label Text="" ID="lblUserFormTitle" runat="server" /></strong>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table cellpadding="0" cellspacing="1" class="View_Table" style="width: 100%">
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblEmail" Text="Email" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtEmail" Width="200" />
                            <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail"
                                runat="server" ErrorMessage="(*)" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblFullname" Text="FullName" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtFullName" Width="200" />
                            <asp:RequiredFieldValidator ID="rfvFullName" ControlToValidate="txtFullName"
                                runat="server" ErrorMessage="(*)" ValidationGroup="UserForm"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblPhoneNumber" Text="PhoneNumber" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtPhoneNumber" Width="200" />                    
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblMobi" Text="Mobi" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtMobi" Width="200" />                    
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblAddress" Text="Address" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtAddress" Width="200" />                    
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblDescription" Text="Description" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:TextBox runat="server" ID="txtDescription" Width="200" />                    
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="left">
                            <asp:Label ID="lblCompany" Text="Company" runat="server" />:
                        </td>
                        <td style="width: 32%" align="left">
                            <asp:DropDownList runat="server" ID="ddlCompanyListUserForm" DataTextField="CompanyName" DataValueField="CompanyID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="clear: both">
                </div>
            </div>
            <div>
                <table cellpadding="0" cellspacing="0" class="View_Table" style="width: 100%">
                    <tr>
                        <td style="width: 18%">
                        </td>
                        <td style="width: 32%" align="right">                            
                            <asp:Button ID="btnSaveUserForm" runat="server" Visible="true" CssClass="button_ctrl"
                                Text="Thêm mới" OnClick="btnSaveUserForm_OnClick" ValidationGroup="UserForm" />
                            <asp:Button ID="btnClosePopup" runat="server" Text="Đóng cửa sổ" CssClass="button_ctrl"
                                Width="" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="close" OnClientClick="$find('ShowProductByCustomer').hide(); return false;" />            
        </asp:Panel>
        <div id="divPopup" runat="server">
        </div>
        <ajax:modalpopupextender ID="mpeUserForm" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="divPopup" PopupControlID="pnlUserForm" CancelControlID="btnClosePopup"
            ></ajax:modalpopupextender>
            
    </ContentTemplate>
</asp:UpdatePanel>



