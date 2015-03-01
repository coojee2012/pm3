
//获取()里的数值,减去1并替换
function replaceCountByKH(str) {
    var strs = str.split("(");
    var oldCount = Number(strs[1].substr(0, 1));
    if (oldCount > 0) {
        var newCount = oldCount;
        newCount--;
        str = strs[0] + "(" + newCount.toString() + ")";

    }
    return str;
}