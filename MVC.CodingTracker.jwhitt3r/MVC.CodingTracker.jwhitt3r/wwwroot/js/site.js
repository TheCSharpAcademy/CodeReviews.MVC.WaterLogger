// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function calculate() {
    var tbl = document.getElementById("records");
    var resultArea = document.getElementById("result");

    var result = 0;

    for (var i = 1; i < tbl.rows.length; i++) {
        result = result + +tbl.rows[i].cells[1].innerHTML;
    }

    resultArea.append(`${result}`)
}