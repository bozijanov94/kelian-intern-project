app.directive("userTypeDirective",
    function () {
        return {
            templateUrl: "/app/directives/userType.html",
            restrict: "E",
            scope: {
                model: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.UserTypes = [];

                $scope.fetchData = function (search) {
                    $scope.UserTypes = [{ "Id": 1, "Name": "External" }, { "Id": 0, "Name": "Internal" }];
                    if (search) {
                        $scope.UserTypes = $scope.UserTypes.filter(function (item) {
                            if (item.Name.includes(search)) {
                                return item;
                            }
                        })
                    }
                }

                $scope.fetchData("");

                $scope.change = function (item) {
                    $scope.model = item;
                }
            }
        }
    });

