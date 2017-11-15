function thongketheotg() {
    var froms = $('#fday').val();
    var tos = $('#tday').val();
    var html = "<img src=\"/ThongKe/ThongKeDTTheoTG?froms=" + froms + "&tos=" + tos + "\">";
    $('.Ajax-Table').html(html);
}
function thongketheott() {
    var froms = $('#fdaytt').val();
    var tos = $('#tdaytt').val();
    var html = "<img src=\"/ThongKe/ThongKeTiTrong?froms=" + froms + "&tos=" + tos + "\">";
    $('.Ajax-Table-tt').html(html);
}