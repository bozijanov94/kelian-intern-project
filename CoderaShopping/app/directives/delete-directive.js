app.directive("deleteDirective",
    function (categoryService, $uibModal) {
        return {
            templateUrl: "/app/directives/delete.html",
            restrict: "E",
            scope: {
                id: "=",
                onDelete: "&",
                entityType: "@"
            },
            link: function ($scope, $element, $attributes) {
                $scope.openModal = function () {
                    var modalInstance = $uibModal.open({
                        templateUrl: 'delete-entity.html',
                        controller: function ($scope, $uibModalInstance, entityType) {
                            $scope.entityType = entityType;
                            $scope.confirm = function () {
                                $uibModalInstance.close(true);
                            }

                            $scope.cancel = function () {
                                $uibModalInstance.dismiss();
                            }
                        },
                        resolve: {
                            entityType: function () {
                                return $scope.entityType;
                            }
                        }
                    });

                    modalInstance.result
                        .then(function (response) {
                            if (response) {
                                $scope.onDelete();
                            }
                        },
                            function rejection(error) {
                                return error;
                            });
                }
            }
        }
    });

