(function() {
    angular.module("appManager.services", []);
    angular.module("appManager.controllers", ["ui.bootstrap", "appManager.services"]);
    angular.module("appManager.settings", []);

    var App = angular.module("appManager", ["ngRoute", "appManager.controllers"]);
    App.config([
        "$routeProvider", function($routeProvider) {
            $routeProvider
                .when('/',
                    {
                        templateUrl: 'templates/home.html',
                        controller: 'homeController'
                    }
                );
        }
    ]);


    App.value("URLRoot", urlRoot);
    App.value("API", urlRoot + "/api");
})(window.urlRoot);