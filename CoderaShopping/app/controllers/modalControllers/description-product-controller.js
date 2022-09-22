app.controller("DescriptionProductCtrl",
    function ($scope, $uibModalInstance, desc) {
        $scope.desc = desc;

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    });