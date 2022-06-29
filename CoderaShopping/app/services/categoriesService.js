app.service("categoryService", function ($http, SERVER_URL) {

    this.getAll = function () {
        return $http.get(SERVER_URL + "categories/getAll");
    }

    this.createCategory = function (model) {
        return $http({
            method: "post",
            url: SERVER_URL + "categories/create",
            data: model
        });
    }

})