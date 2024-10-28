function calculate() {
    var tbl = document.getElementById("records");
    var resultArea = document.getElementById("result");

    var result = 0;

    for (var i = 1; i < tbl.rows.length; i++) {
        result = result + +tbl.rows[i].cells[1].innerHTML;
    }

    resultArea.append(`${result}`)
}