// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.
var updateTotal = function () {
    console.log(endpage.value)
    console.log(startpage.value)
    console.log(totalpages.value)
    totalpages.value = endpage.value - startpage.value
}

var startpage = document.getElementById("StartPage");
var endpage = document.getElementById("EndPage");
var totalpages = document.getElementById("TotalPages");

startpage.addEventListener("input", updateTotal, false);
endpage.addEventListener("input", updateTotal, false);




function deleteSession(id) {
    fetch(`/Home/Delete/${id}`, {
        method: 'POST'
    })
        .then(response => {
            window.location.href = '/Home/Index';
        })
}

