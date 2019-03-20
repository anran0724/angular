var CurrentPage = function () {

    jQuery.validator.addMethod("customUsername", function (value, element) {
        if (value === $('input[name="EmailAddress"]').val()) {
            return true;
        }

        return !$.validator.methods.email.apply(this, arguments);
    }, abp.localization.localize("RegisterFormUserNameInvalidMessage"));

    var _passwordComplexityHelper = new app.PasswordComplexityHelper();

    var handleRegister = function () {

        $('.register-form').validate({
            rules: {
                PasswordRepeat: {
                    equalTo: "#RegisterPassword"
                },
                UserName: {
                    required: true,
                    customUsername: true
                }
            },

            submitHandler: function (form) {
                form.submit();
            }
        });

        $('.register-form input').keypress(function (e) {
            if (e.which === 13) {
                if ($('.register-form').valid()) {
                    $('.register-form').submit();
                }
                return false;
            } 
        });

        _passwordComplexityHelper.setPasswordComplexityRules($('input[name=Password], input[name=PasswordRepeat]'), window.passwordComplexitySetting);
    }
    
    return {
        init: function () {
            //handleRegister();
            var upfilePath = $('.register-form').attr("action");
            var pictureName = "";
            var pictureId = "";
            var pictureImg = "";
            $("input[name='ExpirationDate']").datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });

            $('.register-form input[name=SignPicture]').change(function () {
                pictureName = "SignPicture";
                pictureId = "SignPictureId";
                pictureImg = "SignPictureImg";
                $('.register-form').attr("action", upfilePath);
                $('.register-form').submit();              
            });
            $('.register-form input[name=CertificateBackPicture]').change(function () {
                pictureName = "CertificateBackPicture";
                pictureId = "CertificateBackPictureId";
                pictureImg = "CertificateBackPictureImg";
                $('.register-form').attr("action", upfilePath);
                $('.register-form').submit();                
            });
            $('.register-form input[name=CertificateFrontPicture]').change(function () {
                pictureName = "CertificateFrontPicture";
                pictureId = "CertificateFrontPictureId";
                pictureImg = "CertificateFrontPictureImg";
                $('.register-form').attr("action", upfilePath);
                $('.register-form').submit();             
            });


            $('.register-form').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {
                    var $fileInput = $('.register-form input[name=' + pictureName+']');
                    var files = $fileInput.get()[0].files;

                    if (!files.length) {
                        return false;
                    }

                    var file = files[0];

                    //File type check
                    var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                    if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                        abp.message.warn(app.localize('ContractPictureId_Warn_FileType'));
                        return false;
                    }

                    //File size check
                    if (file.size > 5242880) //5MB
                    {
                        abp.message.warn(app.localize('ContractPictureId_Warn_SizeLimit', app.maxProfilPictureBytesUserFriendlyValue));
                        return false;
                    }

                    var mimeType = _.filter(formData, { name: pictureName})[0].value.type;

                    formData.push({ name: 'FileType', value: mimeType });
                    formData.push({ name: 'FileName', value: pictureName });
                    formData.push({ name: 'FileToken', value: app.guid() });

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        var $contractPictureIdResize = $('#' + pictureImg+'');

                        var profileFilePath = abp.appPath + 'File/DownloadTempFile?fileToken=' + response.result.fileToken + '&fileName=' + response.result.fileName + '&fileType=' + response.result.fileType + '&v=' + new Date().valueOf();
                        $("input[name='" + pictureId+"']").val(response.result.fileToken);

                        $contractPictureIdResize.show();
                        $contractPictureIdResize.attr('src', profileFilePath);
                         
                        $contractPictureIdResize.attr('originalWidth', response.result.width);
                        $contractPictureIdResize.attr('originalHeight', response.result.height);

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });

            $("#register-submit-btn").click(function () {
                if ($('.register-form').valid()) {
                    //if ($("input[name='SignPictureId']").val() == "") {
                    //    abp.message.error("请上传签名照片");
                    //    return;
                    //}
                    //if ($("input[name='CertificateBackPictureId']").val() == "") {
                    //    abp.message.error("请上传特种设备正面照片");
                    //    return;
                    //}
                    //if ($("input[name='CertificateFrontPictureId']").val() == "") {
                    //    abp.message.error("请上传特种设备背面照片");
                    //    return;
                    //}
                    $.post("/Account/Register", $(".register-form").serializeArray(), function (returnMsg) {
                        if (returnMsg.result.success) {
                            abp.message.info("注册成功");
                            window.setTimeout(function () {
                                window.location.href = "/Account/Login";
                            }, 1000 * 2);        
                        }
                        else {
                            abp.message.error(returnMsg.result.message);
                        }
                    });
                }
            });           
        }
    };

}();