angular.module("codera.shopping").config(function($stateProvider, $urlRouterProvider) {
   
    //todo: define ur states here
    $stateProvider.state('categories',
            {
                url: "/categories",
                templateUrl: "/app/templates/categories.html",
                controller: "categoriesCtrl"
        });

    $stateProvider.state('products',
        {
            url: "/products",
            templateUrl: "/app/templates/products.html",
            controller: "productsCtrl"
        });

    $stateProvider.state('users',
        {
            url: "/users",
            templateUrl: "/app/templates/users.html",
            controller: "usersCtrl"
        });   

    $stateProvider.state('orders',
        {
            url: "/orders",
            templateUrl: "/app/templates/orders.html",
            controller: "ordersCtrl"
        });

    $urlRouterProvider.otherwise("/categories");
})
.run(function run($state) {
    $state.go('categories');
});