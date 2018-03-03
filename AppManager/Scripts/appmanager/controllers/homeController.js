(function () {
    angular
        .module("appManager.controllers")
        .controller("homeController", ["$scope", "$uibModal", "Site" , HomeController]);

    function HomeController($scope, $uibModal, Site) {

        Site.listarSites().then(function(s) {
            $scope.sites = s;
        });
    }
})();