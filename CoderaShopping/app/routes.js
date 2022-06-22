angular.module("codera.shopping").config(function($stateProvider, $urlRouterProvider) {
   
    //todo: define ur states here
    $stateProvider.state('category',
            {
                url: "/category",
                templateUrl: "/app/templates/categories.html",
                controller: "categoriesCtrl"
        });

    $stateProvider.state('products',
        {
            url: "/products",
            templateUrl: "/app/templates/products.html",
            controller: "productsCtrl"
        });

    $stateProvider.state('orders',
        {
            url: "/orders",
            templateUrl: "/app/templates/orders.html",
            controller: "ordersCtrl"
        });

    $stateProvider.state('users',
        {
            url: "/users",
            templateUrl: "/app/templates/users.html",
            controller: "usersCtrl"
        });

    $stateProvider.state('multiOrders',
        {
            url: "/multiOrders",
            templateUrl: "/app/templates/multiOrders.html",
            controller: "multiOrdersCtrl"
        });

    $stateProvider.state('viewMultiOrders',
        {
            url: "/viewMultiOrders",
            templateUrl: "/app/templates/viewMultiOrders.html",
            controller: "viewMultiOrdersCtrl"
        });

    $urlRouterProvider.otherwise("/");
});