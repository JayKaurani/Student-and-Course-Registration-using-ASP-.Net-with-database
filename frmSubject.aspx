<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSubject.aspx.cs" Inherits="Database_Master.frmSubject" MasterPageFile="~/Database_Master.Master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <h1>DataList</h1>
        <asp:DataList ID="dlSubject" runat="server" DataKeyField="SubjectId" RepeatColumns="2" RepeatDirection="Horizontal"
            OnEditCommand="dlSubject_EditCommand"
            OnDeleteCommand="dlSubject_DeleteCommand"
            OnCancelCommand="dlSubject_CancelCommand"
            OnUpdateCommand="dlSubject_UpdateCommand"
            CssClass="bg-light border border-1 rounded-1">
            <HeaderTemplate>
                <div class="text-center fs-2 fw-bolder text-secondary">Subjects</div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="card m-sm-1">
                    <div class="col-sm-2">
                        <asp:Image ID="imgTitle" runat="server" CssClass="card-img-top ms-sm-3 img-thumbnail mt-sm-3" ImageUrl='<%# Eval("Logo") %>' />
                    </div>
                    <div class="card-body">
                        <p class="card-text">Name:<%#Eval("Subject") %></p>
                        <p class="card-text">Sem:<%#Eval("Sem") %></p>
                        <p class="card-text">Course:<%#Eval("Course") %></p>
                        <asp:HiddenField ID="hdfCourseId" runat="server" Value='<%#Eval("CourseId") %>' />
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" CssClass="btn btn-success" Width="25%" />
                        <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" CssClass="btn btn-danger" Width="25%" OnClientClick="return confirm('Are u sure want to delete?')" />
                    </div>
                </div>
            </ItemTemplate>
            <EditItemTemplate>
                <div class="ps-sm-2 border border-1 rounded-1">
                    <div class="row-cols-2">
                        <asp:Label ID="lblName" AssociatedControlID="txtName" runat="server" CssClass="form-label fw-bold" Text="Subject" />
                    </div>
                    <div class="row">
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Bind("Subject") %>' placeholder="Subject Name" />
                        </div>
                        <div class="col-sm-4">
                            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" runat="server" Text="*" CssClass="text-danger form-label" ErrorMessage="Name Required" />
                        </div>
                    </div>
                    <div class="row-cols-2 mt-sm-2">
                        <asp:Label ID="lblSem" AssociatedControlID="txtSem" runat="server" CssClass="form-label fw-bold" Text="Sem" />
                    </div>
                    <div class="row mt-sm-1">
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtSem" TextMode="Number" min="1" max="10" runat="server" Text='<%# Bind("Sem") %>' CssClass="form-control" />
                        </div>
                        <div class="col-sm-4">
                            <asp:RequiredFieldValidator ID="rfvSem" ControlToValidate="txtSem" runat="server" Text="*" CssClass="text-danger form-label mt-sm-2" ErrorMessage="Semester Required" />
                            <asp:RangeValidator ID="rngSem" ControlToValidate="txtSem" runat="server" Display="Dynamic" ErrorMessage="Value should be 1 to 10" MinimumValue="1" MaximumValue="10" Type="Integer" />
                        </div>
                    </div>
                    <div class="row-cols-2 mt-sm-1">
                        <asp:Label ID="lblCourse" AssociatedControlID="ddlCourse" runat="server" CssClass="form-label fw-bold" Text="Course" />
                    </div>
                    <div class="row mt-sm-1">
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-select" />
                        </div>
                    </div>
                    <div class="row mt-sm-2">
                        <div class="row-cols-6">
                            <asp:Image ID="imgTitle" runat="server" CssClass="img-thumbnail mt-sm-2" ImageUrl='<%# Bind("Logo") %>'/>
                        </div>
                    </div>
                    <div class="row mt-sm-1">
                        <div class="col-sm-10">
                            <asp:FileUpload runat="server" ID="fuLogo" CssClass="form-control mt-sm-2" />
                        </div>
                    </div>
                    <div class="row-cols-1 m-sm-1">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" CssClass="btn btn-success m-sm-1" Width="20%"/>
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" CssClass="btn btn-danger m-sm-1" Width="20%"/>
                    </div>
                </div>
            </EditItemTemplate>
        </asp:DataList>
    </div>
    <div class="row mt-sm-2 ms-sm-1 border border-1 rounded-1 search fw-bolder me-sm-1">
        <div class="col-sm-2 p-sm-2">
            <asp:Label ID="lblCourse" AssociatedControlID="ddlCourseSearch" runat="server" Text="Course" />
        </div>
        <div class="col-sm-5 p-sm-2 text-secondary">
            <asp:DropDownList ID="ddlCourseSearch" runat="server" CssClass="form-select" />
        </div>
        <div class="col-sm-3 p-sm-2">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-secondary" />
        </div>
    </div>
    <div class="row-cols-6 ms-sm-1">
        <asp:LinkButton ID="lnkBtnInsert" Text="New" PostBackUrl="~/frmSubjectEditInsert.aspx?SubjectId=0" runat="server" CssClass="btn btn-success mt-sm-2 mb-sm-2" />
    </div>
    <h1>Repeater</h1>
    <asp:Repeater id="rptSubject" runat="server" OnItemCommand="rptSubject_ItemCommand">
        <ItemTemplate>
            <div class="row mt-sm-1 mb-sm-1">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="col-sm-2">
                            <asp:Image ID="imgTitle" runat="server" CssClass="card-img-top ms-sm-3 img-thumbnail mt-sm-2" ImageUrl='<%# Eval("Logo") %>' />
                        </div>
                        <div class="card-body">
                            <p class="card-text">Name:<%#Eval("Subject") %></p>
                            <p class="card-text">Sem:<%#Eval("Sem") %></p>
                            <p class="card-text">Course:<%#Eval("Course") %></p>
                        </div>
                        <div class="card-footer">
                            <asp:LinkButton ID="lnkBtnEdit" Text="Edit" PostBackUrl='<%# String.Concat("~/frmSubjectEditInsert.aspx?Subject=",Eval("SubjectId")) %>' runat="server" CssClass="btn btn-info" Width="15%" />
                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" CssClass="btn btn-danger" CommandArgument='<%# Eval("SubjectId") %>' Width="15%" OnClientClick="return confirm('Are u sure want to delete?')" />
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>