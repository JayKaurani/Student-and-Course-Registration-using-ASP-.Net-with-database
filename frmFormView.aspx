<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFormView.aspx.cs" Inherits="Database_Master.frmFormView" MasterPageFile="~/Database_Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:FormView ID="frmViewCourse" runat="server" DataKeyNames="CourseId" RenderOuterTable="false" 
            AllowPaging="true" OnPageIndexChanging="frmViewCourse_PageIndexChanging"
            PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-Position="TopAndBottom"
            PagerSettings-NextPageText="N"
            PagerSettings-LastPageText="Last" PagerSettings-PreviousPageText="Prev"
            PagerSettings-FirstPageText="First" PageIndex="1"
            OnModeChanging="frmViewCourse_ModeChanging"
            OnItemUpdating="frmViewCourse_ItemUpdating"
            OnItemDeleting="frmViewCourse_ItemDeleting"
            OnItemCommand="frmViewCourse_ItemCommand"
            OnItemInserting="frmViewCourse_ItemInserting">
            <ItemTemplate>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="col-sm-2">
                                <asp:Image ID="imgTitle" runat="server" CssClass="crd-img-top ms-sm-3 img-thumbnail mt-sm-2" ImageUrl='<%# Eval("Logo") %>' />
                            </div>
                            <div class="card-body">
                                <p class="card-text"><%# Eval("Name") %></p>
                                <p class="card-text"><%# Eval("description") %></p>
                            </div>
                            <div class="card-footer">
                                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="New" CssClass="btn btn-info" Width="25%" />
                                <asp:Button id="btnEdit" Text="Edit" runat="server" CommandName="Edit" CssClass="btn btn-success" Width="25%" />
                                <asp:Button id="btnDelete" Text="Delete" runat="server" CommandName="Delete" CssClass="btn btn-danger" Width="25%" OnClientClick="return confirm('Are u sure want to delete?')" />
                            </div>
                        </div>

                    </div>
                </div>
            </ItemTemplate>
            <InsertItemTemplate>
                <div class="row">
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' placeholder="Course Name" />
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" runat="server" Text="Name Required" CssClass="text-danger form-label" ErrorMessage="Name Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control mt-sm-2" Text='<%# Bind("Description") %>' placeholder="Course Name" TextMode="MultiLine" />
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="rfvDescription" ControlToValidate="txtDescription" runat="server" Text="*" CssClass="text-danger form-label mt-sm-2" ErrorMessage="Description Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="row-cols-6">
                        <asp:Image ID="imgTitle" runat="server" CssClass="img-thumbnail mt-sm-2" ImageUrl='<%# Bind("Logo") %>' />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-10">
                        <asp:FileUpload runat="server" CssClass="form-control mt-sm-2" ID="fuLogo" />
                    </div>
                </div>
                <div class="row-cols-4 mt-sm-2">
                    <asp:Button ID="btnInsert" Text="Save" runat="server" CommandName="Insert" CssClass="btn btn-success m-sm-1" Width="20%" />
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CommandName="Insert" CssClass="btn btn-success m-sm-1" Width="20%" />
                </div>
            </InsertItemTemplate>
            <EditItemTemplate>
                <div class="row">
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' placeholder="Course Name" />
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" runat="server" Text="*" CssClass="text-danger form-label" ErrorMessage="Name Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control mt-sm-2" Text='<%# Bind("Description") %>' placeholder="Course Name" TextMode="MultiLine" />
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="rfvDescription" ControlToValidate="txtDescription" runat="server" Text="*" CssClass="text-danger form-label mt-sm-2" ErrorMessage="Description rEquired" />
                    </div>
                </div>
                <div class="row">
                    <div class="row-cols-6">
                        <asp:Image ID="imgTitle" runat="server" CssClass="img-thumbnail mt-sm-2" ImageUrl='<%# Bind("Logo") %>' />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-10">
                        <asp:FileUpload runat="server" CssClass="form-control mt-sm-2" ID="fuLogo" />
                    </div>
                </div>
                <div class="row-cols-4 mt-sm-2">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-success m-sm-1" Width="20%" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Update" CssClass="btn btn-success m-sm-1" Width="20%" />
                </div>
            </EditItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>


