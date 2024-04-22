// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let orderByHabit = false;
let orderByDate = false;
let isOrderedAsc = false;
let sortingDisplayed = false;

document.addEventListener("DOMContentLoaded", () => {
    changeNavbarPillColor();
    clearForms();

    if (document.title == "Record habit - HabitHub"
        || document.title == "Add habit - HabitHub"
        || document.title == "View habits - HabitHub") {
        getHabitFromDropdown();
    }

    if (document.title == "View habits - HabitHub") {
        populateEditModal();
        getOrderFromDropdown();
    }
});

// Changes navbar pill color on hover.
function changeNavbarPillColor() {
    const navbarPills = document.getElementsByClassName("nav-link text-white");

    for (let i = 0; i < navbarPills.length; i++) {
        navbarPills[i].addEventListener("mouseover", (event) => {
            event.target.className = "nav-link text-black";
        });

        navbarPills[i].addEventListener("mouseout", (event) => {
            event.target.className = "nav-link text-white";
        });
    }
}

// Clears all forms on loading.
function clearForms() {
    const forms = document.getElementsByClassName("form-control");

    for (let i = 0; i < forms.length; i++) {
        // Don't clear the warning
        if (document.title == "Add habit - HabitHub") {
            if (document.getElementById("habit-warning").innerHTML != "") {
                continue;
            }
        }
        forms[i].value = null;
    }

    // Clear warning when clicking on the input form
    if (document.title == "Add habit - HabitHub") {
        document.getElementById("habit-add-input").addEventListener("click", (event) => {
            document.getElementById("habit-warning").innerHTML = "";
        });
    }
}

// Retrieves habit name from the dropdown menu.
function getHabitFromDropdown() {
    if (document.title == "View habits - HabitHub") {

        // Filter by habit dropdown
        const dropdownItemsFilter = document.getElementById("habit-dropdown-filter").getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItemsFilter.length; i++) {
            dropdownItemsFilter[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button-filter").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input-filter").value = event.target.innerHTML;
            });
        }

        // Modal habit dropdown
        const dropdownItemsModal = document.getElementById("table-edit-record").getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItemsModal.length; i++) {
            dropdownItemsModal[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input").value = event.target.innerHTML;
            });
        }
    }
    else {
        // Regular habit dropdown
        const dropdownItems = document.getElementsByClassName("dropdown-item");

        for (let i = 0; i < dropdownItems.length; i++) {
            dropdownItems[i].addEventListener("click", (event) => {
                document.getElementById("habit-dropdown-button").innerHTML = event.target.innerHTML;
                document.getElementById("habit-input").value = event.target.innerHTML;
            });
        }
    }
}

// Populates the edit modal with the currently selected record data.
function populateEditModal() {
    let recordsTable = document.getElementById("table-view-records");
    const editButtons = recordsTable.getElementsByTagName("button");

    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].addEventListener("click", (event) => {

            let editRecordRow = event.target.id;

            document.getElementById("edit-record-id").value = editButtons[i].value;
            document.getElementById("habit-dropdown-button").innerHTML = recordsTable.rows[editRecordRow].cells[0].innerHTML;
            document.getElementById("edit-record-amount").value = recordsTable.rows[editRecordRow].cells[1].innerHTML;
            document.getElementById("edit-record-unit").value = recordsTable.rows[editRecordRow].cells[2].innerHTML;

            let rawDate = new Date(recordsTable.rows[editRecordRow].cells[3].innerHTML);
            let convertedDate = convertToDatetimeLocal(rawDate);

            document.getElementById("edit-record-date").value = convertedDate;

        });
    }
}

// Converts the given date to the YYYY-MM-DDTHH:MM format.
function convertToDatetimeLocal(dateToConvert) {
    let year = dateToConvert.getFullYear();

    let month = dateToConvert.getMonth();
    month++; // Account for zero-indexing
    month = padWithZero(month);

    let day = dateToConvert.getDate();
    day = padWithZero(day);

    let hour = dateToConvert.getHours();
    hour = padWithZero(hour);

    let minutes = dateToConvert.getMinutes();
    minutes = padWithZero(minutes);

    let convertedDate = `${year}-${month}-${day}T${hour}:${minutes}`;

    return convertedDate;
}

// Pads the given string with a leading 0 if single-digit.
function padWithZero(itemToFormat) {
    if (String(itemToFormat).length >= 2) {
        return itemToFormat;
    }
    return `0${itemToFormat}`;
}

// Retrieves ordering mode from the dropdown menu.
function getOrderFromDropdown() {

    // Order by habit
    document.getElementById("dropdown-item-order-by-habit").addEventListener("click", (event) => {
        document.getElementById("dropdown-button-order").innerHTML = event.target.innerHTML;
        document.getElementById("order-button-revert").style.display = "none";
        document.getElementById("order-apply-button").style.display = "inline";

        if (document.getElementById("order-asc-desc-buttons") != null) {
            document.getElementById("order-asc-desc-buttons").style.display = "none";
        }

        orderByHabit = true;
        orderByDate = false;
        listenForOrdering();
    });

    // Order by date
    document.getElementById("dropdown-item-order-by-date").addEventListener("click", (event) => {
        document.getElementById("dropdown-button-order").innerHTML = event.target.innerHTML;
        document.getElementById("order-button-revert").style.display = "none";
        document.getElementById("order-apply-button").style.display = "inline";

        if (document.getElementById("order-asc-desc-buttons") != null) {
            document.getElementById("order-asc-desc-buttons").style.display = "none";
        }

        orderByDate = true;
        orderByHabit = false;
        listenForOrdering();
    });
}

// Listens for applying the selected ordering mode.
function listenForOrdering() {
    document.getElementById("order-apply-button").addEventListener("click", (event) => {
        document.getElementById("order-apply-button").style.display = "none";
        document.getElementById("order-asc-desc-buttons").style.display = "inline";
        document.getElementById("order-button-revert").style.display = "inline";
        orderRecords(true);
    });
}

// Orders records in the habits table depending on the mode selected.
function orderRecords(orderAsc) {
    let recordsTableRows = document.getElementById("table-view-records").rows;
    let indexAndHabit = new Map();

    // Order by habit
    if (orderByHabit) {
        indexAndHabit = getOrderByHabit(indexAndHabit, recordsTableRows, orderAsc);
        // Order by date
    } else if (orderByDate) {
        indexAndHabit = getOrderByDate(indexAndHabit, recordsTableRows, orderAsc);
    } else {
        isOrderedAsc = false;
        orderByHabit = false;
        orderByDate = false;
        return;
    }

    let tmpTableRows = [];

    // Populate the temporary "rows" with the data from the table
    for (let i = 1; i < recordsTableRows.length; i++) {

        tmpTableRows[i] = [];
        tmpTableRows[i][0] = recordsTableRows[i].cells[0].innerHTML;
        tmpTableRows[i][1] = recordsTableRows[i].cells[1].innerHTML;
        tmpTableRows[i][2] = recordsTableRows[i].cells[2].innerHTML;
        tmpTableRows[i][3] = recordsTableRows[i].cells[3].innerHTML;
    }

    // Populate the table with the data from the temporary "rows" according to the new order
    for (let i = 1; i < tmpTableRows.length; i++) {
        recordsTableRows[i].cells[0].innerHTML = tmpTableRows[indexAndHabit[i - 1][0]][0];
        recordsTableRows[i].cells[1].innerHTML = tmpTableRows[indexAndHabit[i - 1][0]][1];
        recordsTableRows[i].cells[2].innerHTML = tmpTableRows[indexAndHabit[i - 1][0]][2];
        recordsTableRows[i].cells[3].innerHTML = tmpTableRows[indexAndHabit[i - 1][0]][3];
    }

    listenForSorting();
}

// Handles ordering records in the habits table by habit.
function getOrderByHabit(indexAndHabit, recordsTableRows, orderAsc) {
    for (let i = 0; i < recordsTableRows.length; i++) {
        // Bypass the 1st row (table header)
        if (i == 0) {
            continue;
        }
        // Save the habit (value) from each row paired with its current index (key)
        indexAndHabit.set(i, recordsTableRows[i].cells[0].innerHTML);
    }

    // Sort in ascending order by habit (value)
    // Proper comparison method from: https://www.basedash.com/blog/how-to-sort-a-map-in-javascript
    indexAndHabit = Array.from(indexAndHabit).sort((a, b) => a[1].localeCompare(b[1]));

    if (orderAsc) {
        isOrderedAsc = true;
        return indexAndHabit;
    }
    else {
        isOrderedAsc = false;
        // Reverse if the order should be descending
        return indexAndHabit.reverse();
    }
}

// Handles ordering records in the habits table by date.
function getOrderByDate(indexAndHabit, recordsTableRows, orderAsc) {
    for (let i = 0; i < recordsTableRows.length; i++) {
        // Bypass the 1st row (table header)
        if (i == 0) {
            continue;
        }
        // Save the date (value) from each row paired with its current index (key)
        indexAndHabit.set(i, Date.parse(recordsTableRows[i].cells[3].innerHTML));
    }

    // Sort in ascending order by date (value)
    indexAndHabit = Array.from(indexAndHabit).sort((a, b) => a[1] - b[1]);

    if (orderAsc) {
        isOrderedAsc = true;
        return indexAndHabit;
    }
    else {
        isOrderedAsc = false;
        // Reverse if the order should be descending
        return indexAndHabit.reverse();
    }
}

// Listens for additional sorting after ordering the records in the habits table.
function listenForSorting() {
    if (sortingDisplayed) {
        return;
    }

    document.getElementById("order-asc-button").addEventListener("click", (event) => {
        orderRecords(true);
    });

    document.getElementById("order-desc-button").addEventListener("click", (event) => {
        orderRecords(false);
    });

    // Make sure the listening is applied only once
    sortingDisplayed = true;
}