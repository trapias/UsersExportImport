<%@ Control Language="C#" AutoEventWireup="false" Inherits="forDNN.Modules.UsersExportImport.Settings" CodeBehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnSettings dnnClear" id="dnnSettings">

    <fieldset>

        <div class="dnnFormItem">

            <dnn:label id="lblSeparator" runat="server" text="Separator" helptext="Enter a value" controlname="txtSeparator" />

            <asp:textbox id="txtSeparator" runat="server" maxlength="1" />

        </div>

   </fieldset>

</div>


