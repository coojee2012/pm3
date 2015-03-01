using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;

public partial class Share_webflow_WebFlow : System.Web.UI.Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/xml";
//       Response.Write(@"<?xml version='1.0' encoding='GBK'?>
//<WebFlow>
//  <FlowConfig>
//    <BaseProperties flowId='fly' flowText='网上机票预定流程' />
//    <VMLProperties stepTextColor='green' stepStrokeColor='green' stepShadowColor='#b3b3b3' stepFocusedStrokeColor='yellow' isStepShadow='T' actionStrokeColor='green' actionTextColor='' actionFocusedStrokeColor='yellow' sStepTextColor='green' sStepStrokeColor='green' stepColor1='green' stepColor2='white' isStep3D='true' step3DDepth='20'/>
//    <FlowProperties flowMode='' startTime='' endTime='' ifMonitor='' runMode='' noteMode='' activeForm='' autoExe='' />
//  </FlowConfig>
//  <Steps>
//    <Step>
//      <BaseProperties id='check' text='会员身份确认' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='1112' y='412' textWeight='' strokeWeight='' isFocused='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='sendinfo' text='送票情况确认' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='1105' y='785' textWeight='' strokeWeight='' isFocused='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='post' text='确认并提交预定单' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='1099' y='1159' textWeight='' strokeWeight='' isFocused='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='login' text='登录ctrip.com' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='428px' y='510px' textWeight='' strokeWeight='' isFocused='' zIndex='40'/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='order' text='填写预定单' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='774px' y='1190px' textWeight='' strokeWeight='' isFocused='' zIndex='40'/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='query' text='查询航班信息' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='584px' y='854px' textWeight='' strokeWeight='' isFocused='' zIndex='40'/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='message' text='发短信或电话确认' stepType='NormalStep'/>
//      <VMLProperties width='200' height='200' x='1102' y='1515' textWeight='' strokeWeight='' isFocused='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='begin' text='开 始' stepType='BeginStep'/>
//      <VMLProperties width='170' height='180' x='144' y='166' textWeight='' strokeWeight='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//    <Step>
//      <BaseProperties id='end' text='结 束' stepType='EndStep'/>
//      <VMLProperties width='170' height='180' x='1600' y='1600' textWeight='' strokeWeight='' zIndex=''/>
//      <FlowProperties/>
//    </Step>
//  </Steps>
//  <Actions>
//    <Action>
//      <BaseProperties id='action0' text='开始到登录' actionType='PolyLine' from='begin' to='login'/>
//      <VMLProperties startArrow='' endArrow='classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action1' text='登录到查询' actionType='PolyLine' from='login' to='query'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='40'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action2' text='查询到填单' actionType='PolyLine' from='query' to='order'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='40'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action3' text='填单到会员' actionType='PolyLine' from='order' to='check'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action4' text='会员到送票确认' actionType='PolyLine' from='check' to='sendinfo'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action5' text='送票确认到提交' actionType='PolyLine' from='sendinfo' to='post'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action6' text='提交到短信确认' actionType='PolyLine' from='post' to='message'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//    <Action>
//      <BaseProperties id='action7' text='短信确认到结束' actionType='PolyLine' from='message' to='end'/>
//      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
//      <FlowProperties/>
//    </Action>
//  </Actions>
//</WebFlow>");
            
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FOrder,FLevel,FDefineDay, FProcessId,FTypeId,");
        sb.Append("case FIsSend when 1 then '是' when 2 then '否' end as FIsSend,");
        sb.Append("case FIsEnd when 1 then '是' when 2 then '否' end as FIsEnd,");
        sb.Append("(select top 1 fname from CF_App_Process where fid=FProcessId) FProcessName,");
        sb.Append("(select top 1 fname from cf_sys_role where fnumber=FRoleId) FRoleName ");
        sb.Append("From CF_App_SubFlow ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By forder,FCreateTime Desc");
        DataTable dt = rc.GetTable(sb.ToString());

        StringBuilder sbXML = new StringBuilder();
        sbXML.AppendLine("<?xml version='1.0' encoding='GBK'?>");
        sbXML.AppendLine("<WebFlow>");
        sbXML.AppendLine("<FlowConfig>");
        string FProcessName = rc.GetSignValue(" select top 1 fname from CF_App_Process where fid=@FProcessId", new SqlParameter("@FProcessId", Request.QueryString["fprocessid"]));
        sbXML.AppendLine("<BaseProperties flowId='" + FProcessName + "' flowText='" + FProcessName + "' />");
        sbXML.AppendLine("<VMLProperties stepTextColor='green' stepStrokeColor='green' stepShadowColor='#b3b3b3' stepFocusedStrokeColor='yellow' isStepShadow='T' actionStrokeColor='green' actionTextColor='' actionFocusedStrokeColor='yellow' sStepTextColor='green' sStepStrokeColor='green' stepColor1='green' stepColor2='white' isStep3D='true' step3DDepth='20'/>");
        sbXML.AppendLine("<FlowProperties flowMode='' startTime='' endTime='' ifMonitor='' runMode='' noteMode='' activeForm='' autoExe='' />");
        sbXML.AppendLine("</FlowConfig>");
        sbXML.AppendLine("<Steps>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sbXML.AppendLine("<Step>");
            sbXML.AppendLine("<BaseProperties id='step" + EConvert.ToString(dt.Rows[i]["FId"]).Replace("-","") + "' text='" + dt.Rows[i]["FName"] + "' stepType='NormalStep' />");
            sbXML.AppendLine("<VMLProperties width='200' height='200' x='111' y='"+(400+(i*300))+"' textWeight='' strokeWeight='' isFocused='' zIndex=''/>");
            sbXML.AppendLine("<FlowProperties/>");
            sbXML.AppendLine("</Step>");
        }
        sbXML.AppendLine(@"<Step>
      <BaseProperties id='begin' text='开 始' stepType='BeginStep'/>
      <VMLProperties width='170' height='180' x='144' y='166' textWeight='' strokeWeight='' zIndex=''/>
      <FlowProperties/>
    </Step>
    <Step>
      <BaseProperties id='end' text='结 束' stepType='EndStep'/>
      <VMLProperties width='170' height='180' x='144' y='1600' textWeight='' strokeWeight='' zIndex=''/>
      <FlowProperties/>
    </Step>");
        sbXML.AppendLine("</Steps>");
        sbXML.AppendLine("<Actions>");
        for (int i = 0; i <= dt.Rows.Count; i++)
        {
            //    
            //      <BaseProperties id='action7' text='短信确认到结束' actionType='PolyLine' from='message' to='end'/>
            //      <VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>
            //      <FlowProperties/>
            //    

            sbXML.AppendLine("<Action>");
            if (i == 0)
            {
                sbXML.AppendLine("<BaseProperties id='action" + i + "' text='' actionType='PolyLine' from='begin' to='step" + EConvert.ToString(dt.Rows[i]["FId"]).Replace("-", "") + "'/>");

            }
            else if (i == dt.Rows.Count)
            {
                sbXML.AppendLine("<BaseProperties id='action" + i + "' text='' actionType='PolyLine' from='step" + EConvert.ToString(dt.Rows[i-1]["FId"]).Replace("-", "") + "' to='end'/>");

            }
            else
            {
                sbXML.AppendLine("<BaseProperties id='action" + i + "' text='' actionType='PolyLine' from='step" + EConvert.ToString(dt.Rows[i - 1]["FId"]).Replace("-", "") + "' to='step" + EConvert.ToString(dt.Rows[i]["FId"]).Replace("-", "") + "'/>");

            }
            sbXML.AppendLine("<VMLProperties startArrow='' endArrow='Classic' strokeWeight='' isFocused='' zIndex='39'/>");
            sbXML.AppendLine("<FlowProperties/>");
            sbXML.AppendLine("</Action>");
        }

        sbXML.AppendLine("</Actions>");
        sbXML.AppendLine("</WebFlow>");

        Response.Write(sbXML.ToString());
      
        
        Response.End();
    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
      
        if (Request["fprocessid"] != null && Request["fprocessid"] != "")
        {
            sb.Append(" and fprocessid='");
            sb.Append(Request["fprocessid"] + "' ");
        }
        return sb.ToString();
    }
}
