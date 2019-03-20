var CurrentPage = function () {

    function setPayment() {
        var $periodType = $('input[name=PaymentPeriodType]:checked');
        $('input[name=DayCount]').val($periodType.data('day-count') ? $periodType.data('day-count') : 0);
    }

    var _passwordComplexityHelper = new app.PasswordComplexityHelper();

    var uploadedFileToken = null;

    var handleRegister = function() {

        $('input[name=PaymentPeriodType]').change(function() {
            setPayment();
        });

        $('input[name=PaymentPeriodType]:first').prop('checked', true);

        setPayment();

        $.validator.addMethod(
            "regex",
            function(value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
            app.localize('TenantName_Regex_Description'));

        $.validator.addMethod(
            "isMobile",
            function (value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
            app.localize('Mobile_Regex_Description'));

        $('.register-form').validate({
            errorElement: 'div',
            errorClass: 'form-control-feedback',
            focusInvalid: false, // do not focus the last invalid input
            ignore: ':hidden',
            rules: {
                AdminPasswordRepeat: {
                    equalTo: "#AdminPassword"
                },
                TenancyName: {
                    required: true,
                    regex: '^[a-zA-Z][a-zA-Z0-9_-]{1,}$'
                },
                Mobile: {
                    isMobile: '^(13[0-9]{9})|(18[0-9]{9})|(14[0-9]{9})|(17[0-9]{9})|(15[0-9]{9})$'
                }
            },
            messages: {
                AdminPasswordRepeat: {
                    equalTo: app.localize('PasswordRepeatError')
                }
            },
            invalidHandler: function(event, validator) {

            },
            highlight: function(element) {
                $(element).closest('.form-group').addClass('has-danger');
            },
            success: function(label) {
                label.closest('.form-group').removeClass('has-danger');
                label.remove();
            },
            errorPlacement: function(error, element) {
                if (element.closest('.input-icon').length === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },
            submitHandler: function(form) {
                form.submit();
            }
        });

        $('.register-form input').keypress(function(e) {
            if (e.which === 13) {
                if ($('.register-form').valid() && checkImageUpload('AptitudePhotoId') && checkImageUpload('BusinessLicenseId')) {

                    if ($('input[name=IsMember]').val() === 'on') {
                        $('input[name=IsMember]').val('true');
                    } else {
                        $('input[name=IsMember]').val('false');
                    }

                    $('.register-form').submit();
                }
                return false;
            }
        });

        _passwordComplexityHelper.setPasswordComplexityRules(
            $("input[name=AdminPassword],input[name=AdminPasswordRepeat]"),
            window.passwordComplexitySetting);

        $('#register-submit-btn').click(function() {

            if (checkImageUpload('AptitudePhotoId') && checkImageUpload('BusinessLicenseId')) {

                if ($('input[name=IsMember]').val() === 'on') {
                    $('input[name=IsMember]').val('true');
                } else {
                    $('input[name=IsMember]').val('false');
                }

                $('.register-form').submit();
            }


        });

        function checkImageUpload(imageId) {

            var element = $('#' + imageId + 'Resize');

            if ($('[name=' + imageId + 'FileToken]').val() === imageId) {

                if (element.prev().hasClass('has-danger')) {
                    return false;
                }

                element.prev().addClass('has-danger')
                    .append('<div id="' + imageId +'-error" class="form-control-feedback">请上传照片</div>');
                return false;

            } else {
                if (element.prev().hasClass('has-danger')) {
                    element.prev().removeClass('has-danger').find('.form-control-feedback').remove();
                }

                return true;
            }
        }
    };

    var handleUploadPicture = function(formName, inputName) {

        $('#' + formName + ' input[name=' + inputName + ']').change(function() {
            $('#' + formName + '').submit();
        });

        $('#' + formName + '').ajaxForm({
            beforeSubmit: function(formData, jqForm, options) {
                var $fileInput = $('#' + formName + ' input[name=' + inputName + ']');
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
                    abp.message.warn(app.localize('ContractPictureId_Warn_SizeLimit',
                        app.maxProfilPictureBytesUserFriendlyValue));
                    return false;
                }

                var mimeType = _.filter(formData, { name: '' + inputName + '' })[0].value.type;

                formData.push({ name: 'FileType', value: mimeType });
                formData.push({ name: 'FileName', value: '' + inputName + '' });
                formData.push({ name: 'FileToken', value: app.guid() });

                return true;
            },
            success: function(response) {
                if (response.success) {
                    var $contractPictureIdResize = $('#' + inputName + 'Resize');

                    var profileFilePath = abp.appPath +
                        'File/DownloadTempFile?fileToken=' +
                        response.result.fileToken +
                        '&fileName=' +
                        response.result.fileName +
                        '&fileType=' +
                        response.result.fileType +
                        '&v=' +
                        new Date().valueOf();
                    uploadedFileToken = response.result.fileToken;

                    $contractPictureIdResize.show();
                    $contractPictureIdResize.attr('src', profileFilePath);
                    $contractPictureIdResize.attr('originalWidth', response.result.width);
                    $contractPictureIdResize.attr('originalHeight', response.result.height);

                    $('[name=' + inputName + 'FileToken]').val(uploadedFileToken);

                    if ($('#' + inputName + 'Resize').prev().hasClass('has-danger')) {
                        $('#' + inputName + 'Resize').prev().removeClass('has-danger').find('.form-control-feedback').remove();
                    }

                } else {
                    abp.message.error(response.error.message);
                }
            }
        });

        $('#' + inputName + 'Resize').hide();

    };

    function init() {
        handleRegister();
        handleUploadPicture('businessLicenseForm', 'BusinessLicenseId');
        handleUploadPicture('aptitudePhotoForm' ,'AptitudePhotoId');
    };

    return {
        init: init
    };
}();