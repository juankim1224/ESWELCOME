<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WEB.schedule.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        var init = function () {

            function fnDeleteStaff(radioId) {

                var deleteRadio = $('#' + radioId).val().toString();

                var hddStaffList = $("input[id$='hdd_ARR_STAFF']").val();
                var staff = hddStaffList.split(',');


                for (let i = 0; i <= staff.length; i++) {

                    if (staff[i] === deleteRadio) {
                        var index = staff.indexOf(staff[i]);
                        var result = staff.splice(index, 1);
                        $("input[id$='hdd_ARR_STAFF']").val(staff);
                    }
                }
                $('#s' + radioId).remove();
            }

        }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
</asp:Content>
