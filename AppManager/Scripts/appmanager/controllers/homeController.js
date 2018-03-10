(function () {
    angular
        .module("appManager.controllers")
        .controller("homeController", ["$scope", "$uibModal", "IISWebSite" , "$location","$window", "URLRoot", HomeController]);

    function HomeController($scope, $uibModal, Site, $location, $window, URLRoot) {

        Site.listarSites().then(function(foundSites) {
            if (!foundSites.length) {
                $window.location.href = URLRoot + "/Parse";
            }
        });
    }
})();