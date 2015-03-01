
$(document).ready(function() {
    $(".reg_txt").focus(function() { $(this).removeClass("reg_txt").addClass("reg_txt_focus"); });
    $(".reg_txt").blur(function() { $(this).removeClass("reg_txt_focus").addClass("reg_txt"); });

    /*===单位类型======*/
    $("#t_FSystemId").focus(function() {
        $("#tip_FSystemId").attr("class", "tip");
        $("#tip_FSystemId").text("选择您的单位类型");
    });
    $("#t_FSystemId").blur(function() {
        if ($(this).val() == "") {
            $("#tip_FSystemId").attr("class", "error");
            $("#tip_FSystemId").text("请选择单位类型。");
        }
        else {

            /*单位名称AJAX验证*/
            check_FCompany();
            /*组织机构代码AJAX验证*/
            check_FJuridcialCode();
            /*营业执照号AJAX验证*/
            check_FLicence();


            $("#tip_FSystemId").attr("class", "success");
            $("#tip_FSystemId").text("");
        }
    });


    /*====单位名称输入框验证======*/
    $("#t_FCompany").focus(function() {
        $("#tip_FCompany").attr("class", "tip");
        $("#tip_FCompany").text("填写单位名称");
    });
    $("#t_FCompany").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FCompany").attr("class", "error");
            $("#tip_FCompany").text("请填写单位名称。");
        }
        else {
            if ($(this).val().trim().length < 2) {
                $("#tip_FCompany").attr("class", "error");
                $("#tip_FCompany").text("单位名称字符太短。");
            } else
                check_FCompany(); //ajax验证
        }
    });

    /*====企业地址输入框验证======*/
    $("#t_FRegistAddress").focus(function() {
        $("#tip_FRegistAddress").attr("class", "tip");
        $("#tip_FRegistAddress").text("请填写单位地址");
    });
    $("#t_FRegistAddress").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FRegistAddress").attr("class", "error");
            $("#tip_FRegistAddress").text("请填写单位地址。");
        }
        else {
            $("#tip_FRegistAddress").attr("class", "success");
            $("#tip_FRegistAddress").text("");
        }
    });
    /*====企业地址输入框验证======*/
    $("#t_FEntTypeId").focus(function() {
        $("#tip_FEntTypeId").attr("class", "tip");
        $("#tip_FEntTypeId").text("请选择单位性质");
    });
    $("#t_FEntTypeId").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FEntTypeId").attr("class", "error");
            $("#tip_FEntTypeId").text("请选择单位性质。");
        }
        else {
            $("#tip_FEntTypeId").attr("class", "success");
            $("#tip_FEntTypeId").text("");
        }
    });
    /*====法人代表输入框验证======*/
    $("#t_FOTxt5").focus(function() {
        $("#tip_FOTxt5").attr("class", "tip");
        $("#tip_FOTxt5").text("请填写法人代表");
    });
    $("#t_FOTxt5").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FOTxt5").attr("class", "error");
            $("#tip_FOTxt5").text("请填写法人代表。");
        }
        else {
            $("#tip_FOTxt5").attr("class", "success");
            $("#tip_FOTxt5").text("");
        }
    });
    /*====法人代表手机号框验证======*/
    $("#t_FMobile").focus(function() {
        $("#tip_FMobile").attr("class", "tip");
        $("#tip_FMobile").text("请填写法人手机号");
    });
    $("#t_FMobile").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FMobile").attr("class", "error");
            $("#tip_FMobile").text("请填写法人手机号。");
        }
        else {
            $("#tip_FMobile").attr("class", "success");
            $("#tip_FMobile").text("");
        }
    });

    /*====个人身份证框验证======*/
    $("#t_FIdCard").focus(function() {
        $("#tip_FIdCard").attr("class", "tip");
        $("#tip_FIdCard").text("请填写身份证");
    });
    $("#t_FIdCard").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FIdCard").attr("class", "error");
            $("#tip_FIdCard").text("请填写身份证。");
        }
        else {
            var iLen = $(this).val().trim().length;
            if (iLen != 15 && iLen != 18) {
                $("#tip_FIdCard").attr("class", "error");
                $("#tip_FIdCard").text("身份证格式不正确。");
            }
            else {
                $("#tip_FIdCard").attr("class", "success");
                $("#tip_FIdCard").text("");
            }
        }
    });

    /*====个人电话框验证======*/
    $("#t_FCall").focus(function() {
        $("#tip_FCall").attr("class", "tip");
        $("#tip_FCall").text("请填写联系电话");
    });
    $("#t_FCall").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FCall").attr("class", "error");
            $("#tip_FCall").text("请填写联系电话。");
        }
        else {
            $("#tip_FCall").attr("class", "success");
            $("#tip_FCall").text("");
        }
    });

    /*====组织机构代码输入框验证======*/
    $("#t_FJuridcialCode").focus(function() {
        $("#tip_FJuridcialCode").attr("class", "tip");
        $("#tip_FJuridcialCode").text("填写组织机构代码，格式：XXXXXXXX-X");
    });
    $("#t_FJuridcialCode").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FJuridcialCode").attr("class", "error");
            $("#tip_FJuridcialCode").text("请填写组织机构代码。");
        }
        else {
            if (!check_JuridcialCode($(this).val())) {
                $("#tip_FJuridcialCode").attr("class", "error");
                $("#tip_FJuridcialCode").text("组织机构代码格试不正确，（格式：XXXXXXXX-X。）");
            }
            else {
                /*组织机构代码AJAX验证*/
                check_FJuridcialCode();
            }
        }
    });


    /*===填写营业执照号验证======*/
    $("#t_FLicence").focus(function() {
        $("#tip_FLicence").attr("class", "tip");
        $("#tip_FLicence").text("填写营业执照号。");
    });
    $("#t_FLicence").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FLicence").attr("class", "error");
            $("#tip_FLicence").text("请填写营业执照号。");
        }
        else {
            /*营业执照号AJAX验证*/
            check_FLicence();
        }
    });


    /*====用户名输入框验证======*/
    $("#t_FName").focus(function() {
        $("#tip_FName").attr("class", "tip");
        $("#tip_FName").text("6~18个字符，包括字母、数字、下划线;字母开头，字母和数字结尾，区分大小写");
    });
    $("#t_FName").blur(function() {
        var ret = chkUsername($(this).val());
        if (ret == 1) {
            $.ajax({
                type: "get",
                url: "regCheck.ashx",
                processData: false,
                data: "type=2&FName=" + $(this).val(),
                error: function() {
                    $("#tip_FName").attr("class", "error");
                    $("#tip_FName").text("验证失败。");
                },
                success: function(str) {
                    if (str == "1") {
                        $("#tip_FName").attr("class", "success");
                        $("#tip_FName").text("");
                    }
                    else {
                        $("#tip_FName").attr("class", "error");
                        $("#tip_FName").text("用户名已存。");
                    }
                }
            });

        }
        else if (ret == 0) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名不能为空。");
        }
        else if (ret == -1) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名不能以数字开头。");
        }
        else if (ret == -2) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("合法长度为6-18个字符。");
        }
        else if (ret == -3) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能包含_,英文字母,数字。");
        }
        else if (ret == -4) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能英文字母开头。");
        }
        else if (ret == -5) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能英文字母或数字结尾。");
        }
    });

    /*====密码输入框验证======*/
    $("#t_FPassWord").focus(function() {
        $("#tip_FPassWord").attr("class", "tip");
        $("#tip_FPassWord").text("最小长度:6。 最大长度:16。");
    })
    $("#t_FPassWord").blur(function() {
        if ($(this).val().length < 6) {
            $("#tip_FPassWord").attr("class", "error");
            $("#tip_FPassWord").text("密码设置错误。密码长度过小。");
        }
        else {
            $("#tip_FPassWord").attr("class", "success");
            $("#tip_FPassWord").text("");
        }
    });
    $("#t_FPassWord").keyup(function() {
        validatePwdStrong($(this).val());
        $("#tt_FPassWord").attr("value", "");
        $("#ttip_FPassWord").attr("class", "");
        $("#ttip_FPassWord").text("");
    });

    /*====确认密码输入框验证======*/
    $("#tt_FPassWord").focus(function() {
        $("#ttip_FPassWord").attr("class", "tip");
        $("#ttip_FPassWord").text("确认您的密码。");
    });
    $("#tt_FPassWord").blur(function() {
        if ($(this).val().trim() == "") {
            $("#ttip_FPassWord").attr("class", "error");
            $("#ttip_FPassWord").text("请填写确认密码。");
        }
        else if ($(this).val().trim() != $("#t_FPassWord").val().trim()) {
            $("#ttip_FPassWord").attr("class", "error");
            $("#ttip_FPassWord").text("两次密码输入不一至。");
        }
        else {
            $("#ttip_FPassWord").attr("class", "success");
            $("#ttip_FPassWord").text("");
        }
    });

    /*====联系人验证======*/
    $("#t_FLinkMan").focus(function() {
        $("#tip_FLinkMan").attr("class", "tip");
        $("#tip_FLinkMan").text("填写联系人");
    })
    $("#t_FLinkMan").blur(function() {
        if ($(this).val() == "") {
            $("#tip_FLinkMan").attr("class", "error");
            $("#tip_FLinkMan").text("请填写联系人");
        }
        else {
            $("#tip_FLinkMan").attr("class", "success");
            $("#tip_FLinkMan").text("");
        }
    });


    /*====联系电话验证======*/
    $("#t_FTel").focus(function() {
        $("#tip_FTel").attr("class", "tip");
        $("#tip_FTel").text("填写联系电话");
    })
    $("#t_FTel").blur(function() {
        if ($(this).val() == "") {
            $("#tip_FTel").attr("class", "error");
            $("#tip_FTel").text("请填写联系电话");
        }
        else {
            $("#tip_FTel").attr("class", "success");
            $("#tip_FTel").text("");
        }
    });


    /*====选择系统权限验证======*/
    $("input[name^='r_FSysList']").click(function() {
        if (checkBoxList_Nocheck("r_FSysList")) {
            $("#tip_CheckSys").attr("class", "error");
            $("#tip_CheckSys").text("至少要选择一个系统权限。");
        }
        else {
            $("#tip_CheckSys").attr("class", "success");
            $("#tip_CheckSys").text("");
        }
    });




    /*====验证码输入框验证======*/
    $("#YZM").focus(function() {
        $("#tip_YZM").attr("class", "tip");
        $("#tip_YZM").text("填写图片显示的验证码。");
    });
    $("#YZM").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_YZM").attr("class", "error");
            $("#tip_YZM").text("请填写验证码。");
        }
        else {
            $.ajax({
                type: "get",
                url: "../../ApproveWeb/main/vCodeTool.ashx",
                processData: false,
                data: "YZM=" + $("#YZM").attr("value"),
                error: function() {
                    $("#tip_YZM").attr("class", "error");
                    $("#tip_YZM").text("验证码填写错误。");
                },
                success: function(str) {
                    if (str == "1") {
                        $("#tip_YZM").attr("class", "success");
                        $("#tip_YZM").text("");
                    }
                    else {
                        $("#tip_YZM").attr("class", "error");
                        $("#tip_YZM").text("验证码填写错误。");
                    }
                }
            });
        }
    });


    /*====确认按钮======*/
    $("#btnREG").click(function() {
        var ok = true;

        if ($("#t_FSystemId").val() == "") {
            $("#tip_FSystemId").attr("class", "error");
            $("#tip_FSystemId").text("请选择单位类型。");
            ok = false;
        }
        else {
            $("#tip_FSystemId").attr("class", "success");
            $("#tip_FSystemId").text("");
        }


        if ($("#t_FCompany").val().trim() == "") {
            $("#tip_FCompany").attr("class", "error");
            $("#tip_FCompany").text("请填写单位名称。");
            ok = false;
        }
        else {
            /*单位名称AJAX验证*/
            ok = check_FCompany();
        }

        if ($("#t_FJuridcialCode").val().trim() == "") {
            $("#tip_FJuridcialCode").attr("class", "error");
            $("#tip_FJuridcialCode").text("请填写组织机构代码。");
        }
        else {
            /*组织机构代码AJAX验证*/
            ok = check_FJuridcialCode();
        }
        if ($("#t_FLicence").val().trim() == "") {
            $("#tip_FLicence").attr("class", "error");
            $("#tip_FLicence").text("请填写营业执照号。");
        }
        else {
            /*营业执照号AJAX验证*/
            ok = check_FLicence();
        }


        var ret = chkUsername($("#t_FName").val());
        if (ret == 1) {
            $.ajax({
                type: "get",
                url: "regCheck.ashx",
                async: false,
                processData: false,
                data: "type=2&FSystemId=" + $("#t_FSystemId").val() + "&FName=" + escape($("#t_FName").val()),
                error: function() {
                    $("#tip_FName").attr("class", "error");
                    $("#tip_FName").text("验证失败。");
                    ok = false;
                },
                success: function(str) {
                    if (str == "1") {
                        $("#tip_FName").attr("class", "success");
                        $("#tip_FName").text("");
                    }
                    else {
                        $("#tip_FName").attr("class", "error");
                        $("#tip_FName").text("用户名已存。");
                        ok = false;
                    }
                }
            });
        }
        else if (ret == 0) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("请填写用户名。");
            ok = false;
        }
        else if (ret == -1) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名不能以数字开头。");
            ok = false;
        }
        else if (ret == -2) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("合法长度为6-18个字符。");
            ok = false;
        }
        else if (ret == -3) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能包含_,英文字母,数字。");
            ok = false;
        }
        else if (ret == -4) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能英文字母开头。");
            ok = false;
        }
        else if (ret == -5) {
            $("#tip_FName").attr("class", "error");
            $("#tip_FName").text("用户名只能英文字母或数字结尾。");
            ok = false;
        }
        if ($("#t_FPassWord").val().length < 6) {
            $("#tip_FPassWord").attr("class", "error");
            $("#tip_FPassWord").text("密码设置错误。密码长度过小。");
            ok = false;
        }
        else {
            $("#tip_FPassWord").attr("class", "success");
            $("#tip_FPassWord").text("");
        }
        if ($("#tt_FPassWord").val().trim() == "") {
            $("#ttip_FPassWord").attr("class", "error");
            $("#ttip_FPassWord").text("请填写确认密码。");
            ok = false;
        }
        else if ($("#tt_FPassWord").val().trim() != $("#t_FPassWord").val().trim()) {
            $("#ttip_FPassWord").attr("class", "error");
            $("#ttip_FPassWord").text("两次密码输入不一至。");
            ok = false;
        }
        else {
            $("#ttip_FPassWord").attr("class", "success");
            $("#ttip_FPassWord").text("");
        }


        /*====联系人验证======*/
        if ($("#t_FLinkMan").val() == "") {
            $("#tip_FLinkMan").attr("class", "error");
            $("#tip_FLinkMan").text("请填写联系人");
            ok = false;
        }
        else {
            $("#tip_FLinkMan").attr("class", "success");
            $("#tip_FLinkMan").text("");
        }



        /*====联系电话验证======*/
        if ($("#t_FTel").val() == "") {
            $("#tip_FTel").attr("class", "error");
            $("#tip_FTel").text("请填写联系电话");
            ok = false;
        }
        else {
            $("#tip_FTel").attr("class", "success");
            $("#tip_FTel").text("");
        }




        /*====选择系统权限验证======*/
        if (checkBoxList_Nocheck("r_FSysList")) {
            $("#tip_CheckSys").attr("class", "error");
            $("#tip_CheckSys").text("至少要选择一个系统权限。");
            ok = false;
        }
        else {
            $("#tip_CheckSys").attr("class", "success");
            $("#tip_CheckSys").text("正确。");
        }


        if ($("#YZM").val().trim() == "") {
            $("#tip_YZM").attr("class", "error");
            $("#tip_YZM").text("请填写验证码。");
            ok = false;
        }
        else {
            $.ajax({
                type: "get",
                async: false,
                url: "../../ApproveWeb/main/vCodeTool.ashx",
                processData: false,
                data: "YZM=" + $("#YZM").attr("value"),
                error: function() {
                    $("#tip_YZM").attr("class", "error");
                    $("#tip_YZM").text("验证码填写错误。");
                    ok = false;
                },
                success: function(str) {
                    if (str == "1") {
                        $("#tip_YZM").attr("class", "success");
                        $("#tip_YZM").text("");
                    }
                    else {
                        $("#tip_YZM").attr("class", "error");
                        $("#tip_YZM").text("验证码填写错误。");
                        ok = false;
                    }
                }
            });
        }

        if (!ok) {
            alert("请将信息填写正确后再点击注册");
            return false;
        }
    });
});


function LicencePic() {
    $("#tip_FLicencePic").attr("class", "success");
    $("#tip_FLicencePic").text("上传成功");
    $("#div_FLicencePic").fadeIn("slow");
}

/*单位名称AJAX验证*/
function check_FCompany() {
    var v = true;
    $.ajax({
        type: "get",
        url: "regCheck.ashx",
        processData: false,
        data: "type=1&FCompany=" + escape($("#t_FCompany").val()) + "&FSystemId=" + escape($("#t_FSystemId").val()),
        error: function() {
            $("#tip_FCompany").attr("class", "error");
            $("#tip_FCompany").text("验证失败。");
            v = false;
        },
        success: function(str) {
            if (str == "1") {
                $("#tip_FCompany").attr("class", "success");
                $("#tip_FCompany").text("");
            }
            else if (str == "2") {
                $("#tip_FCompany").attr("class", "error");
                $("#tip_FCompany").text("单位名称已存在。");
                v = false;
            }
            else if (str == "3") {
                $("#tip_FCompany").attr("class", "error");
                $("#tip_FCompany").text("该单位已注册过，并未审核完。");
                v = false;
            }
            else if (str == "4") {//未填写
            }
            else {
                $("#tip_FCompany").attr("class", "error");
                $("#tip_FCompany").text("验证失败。");
                v = false;
            }
        }
    });
    return v;
}

/*组织机构代码AJAX验证*/
function check_FJuridcialCode() {
    var v = true;
    $.ajax({
        type: "get",
        async: false,
        url: "regCheck.ashx",
        processData: false,
        data: "type=3&FJuridcialCode=" + escape($("#t_FJuridcialCode").val()) + "&FSystemId=" + escape($("#t_FSystemId").val()),
        error: function() {
            $("#tip_FJuridcialCode").attr("class", "error");
            $("#tip_FJuridcialCode").text("验证失败。");
            v = false;
        },
        success: function(str) {
            if (str == "1") {
                $("#tip_FJuridcialCode").attr("class", "success");
                $("#tip_FJuridcialCode").text("");
            }
            else if (str == "2") {
                $("#tip_FJuridcialCode").attr("class", "error");
                $("#tip_FJuridcialCode").text("组织机构代码已存在。");
                v = false;
            }
            else if (str == "3") {
                $("#tip_FJuridcialCode").attr("class", "error");
                $("#tip_FJuridcialCode").text("该组织机构代码注册过，并未审核完。");
                v = false;
            }
            else if (str == "4") {//未填写
            }
            else {
                $("#tip_FJuridcialCode").attr("class", "error");
                $("#tip_FJuridcialCode").text("验证失败。");
                v = false;
            }
        }
    });
    return v;
}

/*营业执照号AJAX验证*/
function check_FLicence() {
    var v = true;
    $.ajax({
        type: "get",
        url: "regCheck.ashx",
        processData: false,
        data: "type=4&FLicence=" + escape($("#t_FLicence").val()) + "&FSystemId=" + escape($("#t_FSystemId").val()),
        error: function() {
            $("#tip_FLicence").attr("class", "error");
            $("#tip_FLicence").text("营业执照号填写错误。");
            v = false;
        },
        success: function(str) {
            if (str == "1") {
                $("#tip_FLicence").attr("class", "success");
                $("#tip_FLicence").text("");
            }
            else if (str == "2") {
                $("#tip_FLicence").attr("class", "error");
                $("#tip_FLicence").text("营业执照号已存在。");
                v = false;
            }
            else if (str == "3") {
                $("#tip_FLicence").attr("class", "error");
                $("#tip_FLicence").text("该营业执照号已注册过，并未审核完。");
                v = false;
            }
            else if (str == "4") {//未填写
            }
            else {
                $("#tip_FLicence").attr("class", "error");
                $("#tip_FLicence").text("验证失败。");
                v = false;
            }
        }
    });
    return v;
}

/*================================用户名验证 ===========Begin=======================================*/
function chkUsername(username) {
    if (username == "") {
        return 0;
    }
    else if (/^\d.*$/.test(username)) {
        //用户名不能以数字开头
        return -1;
    }
    else if (fLen(username) < 6 || fLen(username) > 18) {
        //合法长度为6-18个字符
        return -2;
    }
    else if (!/^\w+$/.test(username)) {
        //用户名只能包含_,英文字母，数字
        return -3;
    }
    else if (!/^([a-z]|[A-Z])[0-9a-zA-Z_]+$/.test(username)) {
        //用户名只能英文字母开头
        return -4;
    }
    else if (!(/[0-9a-zA-Z]+$/.test(username))) {
        //用户名只能英文字母或数字结尾
        return -5;
    }
    return 1;
}

//计算字符数，一个中文2个字符
function fLen(Obj) {
    var nCNLenth = 0;
    var nLenth = Obj.length;
    for (var i = 0; i < nLenth; i++) {
        if (Obj.charCodeAt(i) > 255) {
            nCNLenth += 2;
        } else {
            nCNLenth++;
        }
    }
    return nCNLenth;
}
/*================================密码强度 ===========Begin=======================================*/
function validatePwdStrong(value) {
    var pwd = {
        color: ['#E6EAED', '#AC0035', '#FFCC33', '#639BCC', '#246626'],
        text: ['太短', '弱', '一般', '很好', '极佳']
    };
    function colorInit() {
        $("#pwdStrong_1").css("backgroundColor", pwd.color[0]);
        $("#pwdStrong_2").css("backgroundColor", pwd.color[0]);
        $("#pwdStrong_3").css("backgroundColor", pwd.color[0]);
        $("#pwdStrong_4").css("backgroundColor", pwd.color[0]);
    }
    $("#pwdDIV").show();
    if (checkStrong(value) == 1) {
        colorInit();
        $("#pwdStrong_1").css("backgroundColor", pwd.color[1]);
        $("#pwdStrong_text").text(pwd.text[1]);
        $("#pwdStrong_text").css("color", pwd.color[1]);
    }
    else if (checkStrong(value) == 2) {
        colorInit();
        $("#pwdStrong_1").css("backgroundColor", pwd.color[2]);
        $("#pwdStrong_2").css("backgroundColor", pwd.color[2]);
        $("#pwdStrong_text").text(pwd.text[2]);
        $("#pwdStrong_text").css("color", pwd.color[2]);
    }
    else if (checkStrong(value) == 3) {
        colorInit();
        $("#pwdStrong_1").css("backgroundColor", pwd.color[3]);
        $("#pwdStrong_2").css("backgroundColor", pwd.color[3]);
        $("#pwdStrong_3").css("backgroundColor", pwd.color[3]);
        $("#pwdStrong_text").text(pwd.text[3]);
        $("#pwdStrong_text").css("color", pwd.color[3]);
    }
    else if (checkStrong(value) == 4) {
        $("#pwdStrong_1").css("backgroundColor", pwd.color[4]);
        $("#pwdStrong_2").css("backgroundColor", pwd.color[4]);
        $("#pwdStrong_3").css("backgroundColor", pwd.color[4]);
        $("#pwdStrong_4").css("backgroundColor", pwd.color[4]);
        $("#pwdStrong_text").text(pwd.text[4]);
        $("#pwdStrong_text").css("color", pwd.color[4]);
    }
}

//组织机构代码格试验证
function check_JuridcialCode(str) {
    if (str) {
        var patrn = /^[A-Za-z0-9]{1}[0-9]{7}-[A-Za-z0-9]{1}$/;
        if (!patrn.exec(str)) {
            return false
        }
    }
    return true;
}

function CharMode(iN) {
    if (iN >= 48 && iN <= 57) //数字 
        return 1;
    if (iN >= 65 && iN <= 90) //大写字母 
        return 2;
    if (iN >= 97 && iN <= 122) //小写 
        return 4;
    else
        return 8; //特殊字符 
}

//bitTotal函数 
//计算出当前密码当中一共有多少种模式 
function bitTotal(num) {
    modes = 0;
    for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
    }
    return modes;
}

//checkStrong函数 
//返回密码的强度级别 
function checkStrong(sPW) {
    Modes = 0;
    for (i = 0; i < sPW.length; i++) {
        //测试每一个字符的类别并统计一共有多少种模式. 
        Modes |= CharMode(sPW.charCodeAt(i));
    }
    return bitTotal(Modes);
}