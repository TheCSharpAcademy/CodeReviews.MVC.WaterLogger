function calculate() {
    var table = document.getElementById("records");
    var resultArea = document.getElementById("result");

    var result = 0;

    for (var i = 1; i < table.rows.length; i++) {
        result = result + +table.rows[i].cells[1].innerHTML;
    }

    resultArea.append(`${result}`);
}