(function () {

    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {

        // ------------------------------------------------------------------------
        // scope variables and set up
        // ------------------------------------------------------------------------

        $scope.$watch(
            'properties.Model.InputType.PropertyValue',
            function (newInputType, oldInputType) {
                if (newInputType !== oldInputType) {
                    $scope.fieldInputType = getInputType(newInputType);
                    $scope.properties.Model.MetaField.DefaultValue.PropertyValue = null;
                }
            },
            true
        );

        var getFieldName = function (title) {
            // Convert string to Pascal Case string with only letters and numbers
            return title.replace(/\w+/g, function (txt) {
                return txt.charAt(0).toUpperCase() + txt.substr(1);
            }).replace(/[^A-Za-z0-9]/g, '');
        }

        $scope.fieldNameIsSet = false;
        $scope.$watch(
            'properties.Model.MetaField.Title.PropertyValue',
            function (newValue, oldValue) {
                if (!$scope.fieldNameIsSet && newValue !== oldValue) {
                    $scope.properties.Model.MetaField.FieldName.PropertyValue = getFieldName(newValue);
                }
            },
            true
        );

        $scope.$watch(
            'defaultValue.value',
            function (newDefaultValue, oldDefaultValue) {
                if (newDefaultValue !== oldDefaultValue && $scope.properties) {
                    $scope.properties.Model.MetaField.DefaultValue.PropertyValue = angular.element("#predefinedValue").val();
                }
            },
            true
        );

        $scope.feedback.showLoadingIndicator = true;
        $scope.defaultValue = { value: "" };

        var getInputType = function (textType) {
            if (textType == 'DateTimeLocal')
                return 'datetime-local';
            if (textType == 'Hidden')
                return 'text';
            else
                return textType.toLowerCase();
        };

        var onGetPropertiesSuccess = function (data) {
            if (data) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);
                if ($scope.properties.Model.MetaField.FieldName) {
                    $scope.fieldNameIsSet = !$scope.properties.Model.MetaField.FieldName.IsEditable;
                }

                $scope.fieldInputType = getInputType($scope.properties.Model.InputType.PropertyValue);

                var inputType = $scope.properties.Model.InputType.PropertyValue.toLowerCase();
                var defaultValue = inputType === "text" || inputType === "url" || inputType === "password" ? null : "";
                $scope.defaultValue = { value: defaultValue };
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();