app.service("categoryService", function ($http, SERVER_URL) {

    this.getAll = function () {
        var result = $http.get(SERVER_URL + "categories/getAll");
        return result;
    }

})