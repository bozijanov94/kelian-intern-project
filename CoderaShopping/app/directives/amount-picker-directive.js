app.directive("amountPickerDirective",
    function () {
        return {
            templateUrl: "/app/directives/amount-picker.html",
            restrict: "E",
            scope: {
                model: "=",
                load: "&"
            },
            link: function ($scope, $element, $attributes) {
                $scope.pickerOnChange = function (newValue) {
                    $scope.model = newValue;
                }
            }
        }
    });

