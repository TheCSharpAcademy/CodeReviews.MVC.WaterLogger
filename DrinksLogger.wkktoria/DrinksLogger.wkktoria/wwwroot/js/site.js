function search() {
    let input = document.getElementById("searchbox");
    let keyword = input.value.toUpperCase();
    let table = document.getElementById("drinks");
    let rows = table.getElementsByTagName("tr");

    for (let i = 1; i < rows.length; i++) {
        let columns = rows[i].getElementsByTagName("td");

        for (let j = 0; j < columns.length - 2; j++) {
            if (columns[j]) {
                let textValue = columns[j].innerText.trim().toUpperCase();
                console.log(textValue)

                if (textValue.indexOf(keyword) > -1) {
                    rows[i].style.display = "";
                    break;
                } else {
                    rows[i].style.display = "none";
                }
            }
        }
    }
}