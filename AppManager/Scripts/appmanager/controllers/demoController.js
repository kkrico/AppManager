(function() {
    angular
        .module("appManager.controllers")
        .controller("demoController", ["$scope", "MyLog", DemoController]);

    function DemoController($scope, MyLog) {
        $scope.mensagem = "Mensagem da Demo controller";
        debugger;
        $scope.numero = MyLog;
    };
})();