'use strict';

var tempUserId = 0;
var tempUserLogin = "";

var tempCategory = "";
var tempSubcategory = "";
var tempSearchField = "";

var isManager = false;
var isAdmin = false;

var app = angular.module('myApp');
app.controller('signIn', function ($scope, $http, $route) {
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    if (tempUserLogin != "") $scope.logged = true;

    $scope.check = function () {
        $scope.manager = isManager;
        $scope.admin = isAdmin;
        if (tempUserLogin != "") $scope.logged = true;

        $http.get("http://localhost:58135/api/user/" + $scope.login + "/" + $scope.password).then(
            function (response) {
                tempUserLogin = response.data.Login;
                tempUserId = response.data.UserId;
                if (response.data.Status == "User") {
                    isManager = false; isAdmin = false;
                }
                if (response.data.Status == "Manager") {
                    isManager = true; isAdmin = false;
                }
                if (response.data.Status == "Admin") {
                    isManager = true; isAdmin = true;
                }
                $scope.login = "";
                $scope.password = "";
                $route.reload(); 
            },
            function (response) {
                $scope.showWarningMessage = true;
                if (!response.data.Message.includes('No HTTP resource was found') &&
                    !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                else $scope.warningMessage = "Correct your entered data!"
            }
        );
    };
    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        $route.reload();
    };
});

app.controller('register', function ($scope, $http, $route) {
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    if (tempUserLogin != "") $scope.logged = true;
    $scope.registerin = function () {
        var data = {
            "Name": $scope.name,
            "Surname": $scope.surname,
            "Patronymic": $scope.patronymic,
            "Login": $scope.login,
            "Password": $scope.password,
            "PhoneNumber": $scope.phone,
            "Status": "User"
        };
        $http.post('http://localhost:58135/api/user/newUser', data)
            .then(
            function (response) {
                alert("Registered");
                $scope.name = "";
                $scope.surname = "";
                $scope.patronymic = "";
                $scope.phone = "";
                $scope.login = "";
                $scope.password = "";
            },
            function (response) {
                $scope.showWarningMessage = true;
                if (!response.data.Message.includes('No HTTP resource was found') &&
                    !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                else $scope.warningMessage = "Correct your entered data!"
            });
    };
    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        $route.reload();
    };
});

var name = "";
var specification = "";
var price = "";
var locationn = "";
app.controller('realEstateAdd', function ($scope, $http, $route) {
    $scope.name = name;
    $scope.specification = specification;
    $scope.price = price;
    $scope.location = locationn;

    $http.get("http://localhost:58135/api/category").then(function (response) {
        $scope.categories = response.data;
    });
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    if (tempUserLogin != "") $scope.logged = true;
    var finded = false; 
    $scope.lookSubc = function () {
        if (finded == true) {
            name = $scope.name;
            specification = $scope.specification;
            price = $scope.price;
            location = $scope.location;
            $route.reload();
        }
    }
    $scope.findSubcstegories = function () {
        $http.get("http://localhost:58135/api/subcategory/get/" + $scope.selectedCategory.trim()).then(function (response) {
            $scope.subcategories = response.data
            finded = true;
        });
    }

    $scope.create = function () {
        var selectedCatg = $scope.selectedCategory;
        var selectedSubc = $scope.selectedSubcategory;
        if (selectedCatg != undefined) selectedCatg = selectedCatg.trim();
        if (selectedSubc != undefined) selectedSubc = selectedSubc.trim();
        var data = {
            "Name": $scope.name,
            "Specification": $scope.specification,
            "Price": $scope.price,
            "Category": selectedCatg,
            "Subcategory": selectedSubc,
            "Location": $scope.location,
            "Owner": tempUserId
        };
        $http.post('http://localhost:58135/api/realEstate/newRealEstate', data)
            .then(
            function (response) {
                alert("Added");
                $scope.name = "";
                $scope.specification = "";
                $scope.price = "";
                $scope.category = "";
                $scope.subcategory = "";
                $scope.owner = "";
                $scope.location = "";
            },
            function (response) {
                $scope.showWarningMessage = true;
                if (!response.data.Message.includes('No HTTP resource was found') &&
                    !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                else $scope.warningMessage = "Correct your entered data!"
            });
    };
    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        window.location.href = "http://localhost:58352/index.html#!/realEstatesView";
    };
});

app.controller('realEstatesView', function ($scope, $http, $route) {
    $scope.selectedCategory = tempCategory;
    $scope.selectedSubcategory = tempSubcategory;
    $scope.searchField = tempSearchField;
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    
    $scope.owner = false;
    var tempRealEstateId = 0;
    if (tempUserLogin != "") $scope.logged = true;

    $http.get("http://localhost:58135/api/category").then(function (response) {
        $scope.categories = response.data;
    });
    
    if (tempCategory != "" || tempSubcategory != "" || tempSearchField != "") {
        $http.get("http://localhost:58135/api/realEstate/GetRealEstatesBySearch?category=" + tempCategory +
                "&subcategory=" + tempSubcategory +
                "&keyword=" + tempSearchField)
            .then(function (response) {
                $scope.realEstates = response.data;
            });
    }
    else {
        $http.get("http://localhost:58135/api/realEstate/GetConfirmedRealEstates")
            .then(function (response) {
                $scope.realEstates = response.data;
            });
    }

    $scope.editRealEstate = function () {
        $http.put("http://localhost:58135/api/realEstate/change/" + $scope.name + "/" + $scope.specification + "/" + tempRealEstateId)
            .then(
            function (response) {
                alert("Sucsses");
                $route.reload();
            },
            function (response) {
                $scope.showWarningMessage = true;
                if (!response.data.Message.includes('No HTTP resource was found') &&
                    !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                else $scope.warningMessage = "Correct your entered data!"
            });
    };

    $scope.deleteRealEstate = function () {
        $http.delete("http://localhost:58135/api/realEstate/detete/" + tempRealEstateId);
        $route.reload();
    };


    $scope.search = function () {
        tempCategory = $scope.selectedCategory.trim();
        tempSubcategory = $scope.selectedSubcategory.trim();
        tempSearchField = $scope.searchField.trim();
        $route.reload();
    };

    $scope.clearSearch = function () {
        tempCategory = "";
        tempSubcategory = "";
        tempSearchField = "";
        $route.reload();
    };

    $scope.showSpecification = function (Name, Specification, Location, OwnerId, RealEstateId) {
        $scope.name = Name;
        $scope.specification = Specification;
        $scope.location = Location;
        if (tempUserId == OwnerId) $scope.owner = true;
        else $scope.owner = false;
        tempRealEstateId = RealEstateId;
    };

    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        $route.reload();
    };

    var finded = false;
    $scope.lookSubc = function () {
        if (finded == true) {
            name = $scope.name;
            specification = $scope.specification;
            price = $scope.price;
            location = $scope.location;
            $route.reload();
        }
    }
    $scope.findSubcstegories = function () {
        $http.get("http://localhost:58135/api/subcategory/get/" + $scope.selectedCategory.trim()).then(function (response) {
            $scope.subcategories = response.data
            finded = true;
        });
    }
});


app.controller('managerMenu', function ($scope, $http, $route) {
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    $scope.specification = '';
    if (tempUserLogin != "") $scope.logged = true;
    $http.get("http://localhost:58135/api/realEstate/GetUnconfirmedRealEstates").then(function (response) {
        $scope.unconfirmedRealEstates = response.data;
    });

    $scope.confirmRealEstates = function (RealEstatesId) {
        $http.put('http://localhost:58135/api/realEstate/confirm/' + RealEstatesId).then(function (response) {
            $route.reload();
        });
    };

    $scope.deleteRealEstates = function (RealEstatesId) {
        $http.delete("http://localhost:58135/api/realEstate/detete/" + RealEstatesId).then(function (response) {
            $route.reload();
        });
    };

    $scope.showSpecification = function (Specification) {
        $scope.specification = Specification;
    };

    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        window.location.href = "http://localhost:58352/index.html#!/realEstatesView";
    };
});

app.controller('adminMenu', function ($scope, $http, $route) {
    $scope.manager = isManager;
    $scope.admin = isAdmin;
    if (tempUserLogin != "") $scope.logged = true;

    $http.get("http://localhost:58135/api/subcategory").then(function (response) {
        $scope.subcategories = response.data;
    });
    $http.get("http://localhost:58135/api/category").then(function (response) {
        $scope.categories = response.data;
    });

    $scope.saveSubcategory = function () {
        var categ = $scope.selectedCategory;
        if (categ == undefined) categ = "";
        $http.post("http://localhost:58135/api/saveSubcategory/" + categ.trim() + "/" + $scope.subcategName)
            .then(function (response) {
                alert(response.data);
                $route.reload();
            }, function (response) {
                alert(response.data.Message);
            });
    };

    $scope.deleteSubcategory = function (Id) {
        $http.delete("http://localhost:58135/api/deleteSubcategory/" + Id).then(function (response) {
            $route.reload();
        });
    };

    $scope.saveCategory = function () {
        $http.post("http://localhost:58135/api/saveCategory/" + $scope.Name).then(function (response) {
            alert(response.data);
            $route.reload();
        }, function (response) {
            alert(response.data.Message);
        });
    };

    $scope.deleteCategory = function (Id) {
        $http.delete('http://localhost:58135/api/deleteCaregory/' + Id).then(function (response) {
            $route.reload();
        });
    };

    $scope.editSubcategory = function (CName) {
        $scope.categName = CName;
    };

    $scope.logout = function () {
        tempUserLogin = "";
        tempUserId = "";
        isManager = false; isAdmin = false;
        window.location.href = "http://localhost:58352/index.html#!/realEstatesView";
    };
});