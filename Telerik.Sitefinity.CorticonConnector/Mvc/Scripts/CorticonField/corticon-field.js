jQuery(document).ready(function ($) {
    if (typeof FormRulesSettings !== "undefined") {
        FormRulesSettings.addFieldSelector("corticon-field-container", "[data-sf-role='corticon-field-input']");
    }

    var parseFormValue = function (form, fieldName, value) {
        // Get value in correct format. Example: parse numbers as float type instead of string.
        var fieldType = $(form).find('[name="' + fieldName + '"]').attr("type");
        switch (fieldType) {
            case "number": return parseFloat(value);
            default: return value;
        }
    }

    var initializeFields = function (inputs) {
        inputs.on('change', function (e) {
            if (typeof $.fn.processFormRules == 'function')
                $(e.target).processFormRules();
        }); 

        var loadingCorticonValues = false;
        var corticonRequest = null;
        var delayTimeout = null;
        $(inputs).closest('form').on('keyup change paste', 'input, select, textarea', function (formEvent) {
            var isCorticonField = false;
            $(inputs).each(function () {
                isCorticonField = isCorticonField || this === formEvent.target;
            });

            if (!isCorticonField) {
                // Get formId and form field values
                var form = $(inputs).closest('form');
                var formFields = form.serializeArray();
                let fields = {}
                for (var field of formFields) {
                    var fieldIsInsideSFForm = !!$(inputs).closest('[role="form"]').find('[name="' + field.name + '"]')[0];
                    if (fieldIsInsideSFForm && field.value !== "") {
                        fields[field.name] = parseFormValue(form, field.name, field.value);
                    }
                }

                var formData = {
                    formId: $(inputs).closest('[data-sf-role="form-container"]').find('input[data-sf-role="form-id"]').val(),
                    fieldsJSON: JSON.stringify(fields)
                }

                // Execute Corticon rules with delay to avoid sending request on each key press   
                if (delayTimeout) {
                    clearTimeout(delayTimeout);
                }

                if (corticonRequest) {
                    corticonRequest.abort();
                }

                loadingCorticonValues = true;
                var timeoutTime = formEvent.type === "keyup" ? 300 : 0;
                delayTimeout = setTimeout(() => {
                    corticonRequest = $.ajax({
                        url: "/api/default/Default.CorticonResult()",
                        data: JSON.stringify(formData),
                        contentType: "application/json",
                        type: 'POST',
                        success: function (response) {
                            try {
                                var result = JSON.parse(response.value);
                                $(inputs).each(function () {
                                    if (this && result[this.name] && this.value !== result[this.name]) {
                                        if (typeof result[this.name] !== "object") {
                                            $(this).val(result[this.name]);
                                        } else {
                                            $(this).val(JSON.stringify(result[this.name]));
                                        }

                                        $(this).trigger('change');
                                    }
                                });                                
                            }
                            catch {
                                // Do nothing
                            }
                            finally {
                                loadingCorticonValues = false;
                            }
                        },
                        error: function () {
                            loadingCorticonValues = false;
                        }
                    });
                }, timeoutTime);
            }
        });

        var waitForCorticonExecution = function (form) {
            if (form) {
                if (!loadingCorticonValues) {
                    form.submit();
                } else {
                    setTimeout(() => waitForCorticonExecution(form), 100);
                }
            }
        }

        $(inputs).closest('form').on("submit", function (e) {
            if (loadingCorticonValues) {
                e.preventDefault();
                waitForCorticonExecution(this);
            }
        });
    };

    var containers = $('[data-sf-role="corticon-field-container"]');
    var inputs = containers.find('[data-sf-role="corticon-field-input"]');
    initializeFields(inputs);
});