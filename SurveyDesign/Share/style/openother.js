$(document).ready(function() {
    $(".reg_txt").focus(function() { $(this).removeClass("reg_txt").addClass("reg_txt_focus"); });
    $(".reg_txt").blur(function() { $(this).removeClass("reg_txt_focus").addClass("reg_txt"); });



    /*====选择系统权限验证======*/
    $("input[name^='t_FSysList']").click(function() {
        if (checkBoxList_Nocheck("t_FSysList")) {
            $("#tip_CheckSys").attr("class", "error");
            $("#tip_CheckSys").text("至少要选择一个要开通的系统。");
        }
        else {
            $("#tip_CheckSys").attr("class", "success");
            $("#tip_CheckSys").text("");
        }
    });

    /*====选择系统权限验证======*/
    $("#t_FYY").focus(function() {
        $("#tip_FYY").attr("class", "tip");
        $("#tip_FYY").text("填写您的开通原由。");
    });
    $("#t_FYY").blur(function() {
        if ($(this).val().trim() == "") {
            $("#tip_FYY").attr("class", "error");
            $("#tip_FYY").text("请填写您的开通原由。");
        }
        else {
            $("#tip_FYY").attr("class", "success");
            $("#tip_FYY").text("");
        }
    });


    /*====验证码输入框验证======*/
    $("#YZM").focus(function() {
        $("#tip_YZM").attr("class", "tip");
        $("#tip_YZM").text("填写验证码。");
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

        /*====选择系统权限验证======*/
        if (checkBoxList_Nocheck("t_FSysList")) {
            $("#tip_CheckSys").attr("class", "error");
            $("#tip_CheckSys").text("至少要选择一个要开通的系统。");
            ok = false;
        }
        else {
            $("#tip_CheckSys").attr("class", "success");
            $("#tip_CheckSys").text("");
        }

        /*====选择系统权限验证======*/
        if ($("#t_FYY").val().trim() == "") {
            $("#tip_FYY").attr("class", "error");
            $("#tip_FYY").text("请填写您的开通原由。");
            ok = false;
        }
        else {
            $("#tip_FYY").attr("class", "success");
            $("#tip_FYY").text("");
        }



        /*====验证码输入框验证======*/
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
                    $("#tip_YZM").text("验证码验证错误。");
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



    //选项卡按钮
    $("a[id^=a]").click(function() {
        var n = $(this).attr("id").substring(1, 2);
        $("#hidd_n").val(n);
        $("a[id^=a]").each(function() {
            $(this).attr("class", "li" + $(this).attr("id").substring(1, 2));
        });
        $(this).attr("class", "li" + n + "_link");
        $(this).blur();

        $("div[id^=div]").hide();
        $("div[id=div" + n + "]").fadeIn(800);
    });

    show();
});


//登陆
function login(str, sysId) {
    $("#hidd_FID").val(str);
    $("#hidd_sysId").val(sysId);
    $("#btnLogin").click();
}


//选项卡
function show() {

    var n = $("#hidd_n").val();
    $("a[id^=a]").each(function() {
        $(this).attr("class", "li" + $(this).attr("id").substring(1, 2));
    });
    $("a[id=a" + n + "]").attr("class", "li" + n + "_link");

    if (n == "") {
        n = "1";
    }
    $("div[id^=div]").hide();
    $("div[id=div" + n + "]").show();
}