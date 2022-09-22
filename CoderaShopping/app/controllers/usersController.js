app.controller("usersCtrl",
    function ($scope, userService, $uibModal) {

        $scope.users = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.filter = {
            "Name": null,
            "Email": null,
            "UserType": null
        };
        $scope.orderAscend = true;
        $scope.orderBy = "Name";

        $scope.loadUsers = function () {
            var $result = userService.getAll($scope.currentPage, $scope.itemsPerPage, $scope.filter, $scope.orderAscend, $scope.orderBy);
            $result.then(function (result) {
                $scope.users = result.data.Items;
                $scope.totalItems = result.data.TotalItems;
            })
        }

        $scope.loadUsers();

        $scope.removeUser = function (id) {
            userService.deleteUser(id)
                .then(function (result) {
                    $scope.loadUsers();
                }, (function (error) {
                    $scope.errorMessage = error.data;
                }))
        }

        $scope.addUser = function () {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-user.html',
                controller: 'AddEditUserCtrl',
                resolve: {
                    userId: function () {
                        return null;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadUsers();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.updateUser = function (id) {
            var modalInstance = $uibModal.open({
                templateUrl: './app/templates/modalTemplates/add-edit-user.html',
                controller: 'AddEditUserCtrl',
                resolve: {
                    userId: function () {
                        return id;
                    }
                }
            });

            modalInstance.result
                .then(function (response) {
                    if (response) {
                        //refresh the data in the table
                        $scope.loadUsers();
                    }
                },
                    function rejection(error) {
                        return error;
                    });
        }

        $scope.orderChange = function (header) {
            if ($scope.orderBy == header) {
                $scope.orderAscend = !$scope.orderAscend;
            } else {
                $scope.orderAscend = true;
            }
            $scope.orderBy = header;
            $scope.loadUsers();
        }

        $scope.$watch("itemsPerPage", function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.loadUsers();
            }
        })

    });