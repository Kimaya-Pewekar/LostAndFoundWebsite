<%@ Page Title="" Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="LostItemsForum.aspx.cs" Inherits="LostAndFound.LostItemsForum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
       <asp:Button ID="FilterButton" CssClass="btn" runat="server" style="margin-top:10px;" Text="Sort by Category" OnClick="FilterButton_Click" />
       <asp:DropDownList CssClass="dropdown" ID="FilterCategoryDropDown" runat="server">
           <asp:ListItem Value="All" Text="All Categories" />
           <asp:ListItem Value="Electronics" Text="Electronics" />
           <asp:ListItem Value="Keys" Text="Keys" />
           <asp:ListItem Value="Wallet" Text="Wallet" />
           <asp:ListItem Value="Phone" Text="Phone" />
           <asp:ListItem Value="ID" Text="ID" />
           <asp:ListItem Value="Jewelry" Text="Jewelry" />
           <asp:ListItem Value="Others" Text="Others" />
        </asp:DropDownList>
    </div>
    <asp:Repeater ID="LostItemsRepeater" runat="server">
        <ItemTemplate>
            <div class="container">
                <h3><%# Eval("ItemName") %> - <span class="item-type"><%# Eval("ItemType") %></span></h3>
                <asp:Image ID="ItemImage" runat="server" ImageUrl='<%# Eval("ImageUrl") != null ? Eval("ImageUrl") : "no-image.png" %>' Width="150px" Height="150px" />
                <p><strong>Reported by:</strong> <%# Eval("Username") %></p>
                <p><strong>Date Lost:</strong> <%# Eval("DateLost","{0:dd-MM-yyyy}") %></p>
                <p><strong>Last Location:</strong> <%# Eval("LastLocation") %></p>
                <p><strong>Description:</strong> <%# Eval("Description") %></p>
                <asp:Button ID="DeleteButton" Cssclass="btn" runat="server" Text="Delete" CommandArgument='<%# Eval("ReportId") %>' OnClick="DeleteButton_Click" Visible='<%# Eval("IsDeletable") %>' />
                <br />
                <h4>Comments</h4>
                <asp:Repeater ID="CommentsRepeater"  runat ="server" DataSource='<%# Eval("Comments") %>'>
                    <ItemTemplate>
                        <div class="comment">
                            <p><strong><asp:HyperLink runat="server"  NavigateUrl='<%# "UserProfile.aspx?username=" + Eval("Username") %>'><%# Eval("Username") %>:</asp:HyperLink></strong> <%# Eval("CommentText") %></p>
                            <p><em><%# Eval("CommentDate") %></em></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Rows="2" Width="95%" placeholder="Write something..." style="margin-bottom:10px"></asp:TextBox>
                <asp:Button ID="AddCommentButton" CssClass="btn" runat="server" Text="Add Comment" CommandArgument='<%# Eval("ReportId") %>' OnClick="AddCommentButton_Click" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
