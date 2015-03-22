<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpLockInfo.aspx.cs" Inherits="JSDW_project_EmpLockInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>锁定人员详情信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    
    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Repeater ID="dg_List" runat="server">
            <HeaderTemplate>
                <table width="98%" align="center" class="m_dg1">
                    <tr class="m_dg1_h">
                        <th>序号
                        </th>
                        <th>姓名
                        </th>
                        <th>项目名称
                        </th>
                        <th>工程所在地
                        </th>
                        <th>锁定日期
                        </th>
                        <th>工程状态
                        </th>
                        <th>合同开工日期
                        </th>
                        <th>合同竣工日期
                        </th>
                        <th>实际开工日期
                        </th>
                        <th>
                            实际竣工日期
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td>
                        <%# Container.ItemIndex + 1%> 
                    </td>
                    <td>
                        <%# Eval("FHumanName")%>
                    </td>
                    <td>

                        <%# Eval("ProjectName")%>
                    </td>
                    <td>
                        <%# Eval("AddressDept")%>
                    </td>
                    <td>

                        <%# Eval("SDRQ")%>
                    </td>
                    
                    <td>
                        <%#Eval("SDZT") %>
                    </td>
                    <td>
                        <%#Eval("HTKGRQ") %>
                    </td>
                     <td>
                        <%# Eval("HTJGRQ")%>
                    </td>
                    <td>
                        <%# Eval("SJKGRQ")%>
                    </td>
                    <td>
                        <%# Eval("SJJGRQ")%>
                    </td>
                   
                    
                </tr>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
