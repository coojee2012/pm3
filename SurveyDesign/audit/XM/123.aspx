﻿<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link href="http://124.161.35.166/doc/css/layout.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://124.161.35.166/doc/script/jquery-1.5.min.js"></script>
<script type="text/javascript" src="http://124.161.35.166/doc/script/jquery.cookie.js"></script>
<script type="text/javascript" src="login.js"></script>
<script type="text/javascript" src="http://124.161.35.166/doc/script/common.js"></script>
</head>
<body onLoad="InitLogin()">
<div id="mainContent">
  <table style="width:100%;height:100%">
    <tr style="height:100%">
	  <td align="center" valign="middle">
        <table style="width:716px;height:344px;background-image:url(http://124.161.35.166/doc/images/login/login_14.png)">
		  <tr style="height:40px">
		    <td colspan=2 align="right" valign="center">
		      <table>
			    <tr>
				  <td style="padding-right:10px">
				    <select id="LanguageSelect" name="LanguageSelect" onChange="ChangeLanguage('http://124.161.35.166/doc/xml/login.xml',this.value)" style="width:120px; height:20px; display:"></select>
				  </td>
				</tr>
		      </table>
			</td>
		  </tr>
		  <tr style="height:260px">
		    <td style="width:80px;">&nbsp;</td>
		    <td align="center">
		      <table>
                  <tr  style="height:30px">
                    <td style="width:60px;">
			          <label id="lausername" style="color:#FFFFFF"></label>
			        </td>
                    <td  align="center" style="width:200px;">
			          <input name="UserName" id='UserName' type="text" style="font-size:12px;width:195px;height:20px" maxlength="16" value="admin" onkeydown="CheckKeyDown(event)">
			        </td>
                  </tr>
                  <tr style="height:30px">
                    <td style="width:60px;">
			          <label id="lapassword" style="color:#FFFFFF"></label>
			        </td>
                    <td align="center" style="width:200px">
			          <input name="Password1" id="Password1" type="Password" style="font-size:12px;width:195px;height:20px; " maxlength="16" value="363060">
			        </td>
                  </tr>
				  <tr>
				  <td style="width:60px;">&nbsp;</td>
                  <td class="loginbtn" align="center" valign="middle" onClick="DoLogin()" id="LoginBtn" onmousemove="this.className='selectloginbtn'" onmouseout="this.className = 'loginbtn'"><label id="lalogin" style="font-size:12px;color:#000"></label>
			      </td>
				</tr>
			      <tr>
		      </table>
			</td>
		  </tr>	
		  <tr>
		    <td style="width:80px;">&nbsp;</td>
		    <td align="center" valign="top">
			</td>
		  </tr>		  	  
		</table>
		<br>
		<label id="laCopyRight" style="font-size:12px;color:#727272">©Hikvision Digital Technology Co., Ltd. All Rights Reserved.</label>
	  </td>
    </tr>
  </table>
</div> 
</body>
</html>
   