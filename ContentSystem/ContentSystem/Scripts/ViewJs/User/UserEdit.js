//$(function () {
//    $('#btnSave').click(function () {
//        var id = $('#id'),
//            NickName = $('#NickName'),
//            PhoneNo = $('#PhoneNo'),
//            BaseImageId = $('#imgInfoId');
//        //数据校验
//        if (NickName.val() == '' || NickName.val() == null) {
//            NickName.focus();
//            return false;
//        }
//        var dataArr = {
//            UserId: id.val(),
//            NickName: NickName.val(),
//            BaseImageId: BaseImageId.val(),
//            PhoneNo: PhoneNo.val()
//        };
//        window.parent.showModal();
//        //提交
//        $.post('/User/Edit', dataArr, function (data) {
//            if (data.Status == 200) {
//                swal("提示", "操作成功");
//                setTimeout(function () {
//                    window.location.href = '/User/List';
//                }, 1500);
//            }
//            if (data.Status == 202) {
//                swal("提示", "操作失败");
//            }
//            if (data.Status == 203) {
//                swal("提示", "数据重复");
//            }
//        }).complete(function () { window.parent.hideModal(); });
//    });
//});