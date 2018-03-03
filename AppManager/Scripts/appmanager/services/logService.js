(function () {

    angular
        .module("appManager.services")
        .factory("MyLog",
        ["Hub", "$rootScope", "$log", "$timeout", function (Hub, $rootScope, $log, $timeout) {

            var Employees = this;
            Employees.connected = [];
            Employees.loading = true;

            var hub = new Hub("log", {
                listeners: {
                    "newConnection": function (id) {
                        $log.log("Conected " + id);
                        $rootScope.$apply();
                    },
                    "doAlgo": function (n) {
                        debugger;
                        Employees.connected.push(n);
                        $timeout(function() {
                            window.scrollTo(0,document.body.scrollHeight);
                        }, 0, false);
                        $rootScope.$apply();
                    }
                }
            });

            return Employees;
        }]);
})();