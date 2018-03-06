(function() {
    angular
        .module("appManager.controllers")
        .controller("logController", ["$scope", "LogService", LogController]);

    function LogController($scope, LogService) {
        $scope.mensagem = "Ultimos logs";
        $scope.numero = LogService;
    };
})();