app.directive("statusDirective",
    function () {
        return {
            templateUrl: "/app/directives/status.html",
            restrict: "E",
            scope: {
                model: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.Statuses = [];

                $scope.fetchData = function (search) {
                    $scope.Statuses = [{ "Id": 1, "Name": "Active" }, { "Id": 0, "Name": "Inactive" }];
                    if (search) {
                        $scope.Statuses = $scope.Statuses.filter(function (item) {
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

