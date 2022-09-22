app.directive("customerDirective",
    function (userService) {
        return {
            templateUrl: "/app/directives/customer.html",
            restrict: "E",
            scope: {
                model: "="
            },
            link: function ($scope, $element, $attributes) {
                $scope.Customers = [];

                $scope.fetchData = function (search) {
                    var $result = userService.getAllSearch(search);
                    $result.then(function (result) {
                        $scope.Customers = result.data;
                    })
                }

                $scope.fetchData("");

                $scope.change = function (item) {
                    $scope.model = item;
                }
            }
        }
    });

