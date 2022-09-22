app.directive("defaultDirective",
    function () {
        return {
            templateUrl: "/app/directives/default.html",
            restrict: "E",
            scope: {
                model: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.Defaults = [];

                $scope.fetchData = function (search) {
                    $scope.Defaults = [{ "Id": 1, "Name": "Yes" }, { "Id": 0, "Name": "No" }];
                    if (search) {
                        $scope.Defaults = $scope.Defaults.filter(function (item) {
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

