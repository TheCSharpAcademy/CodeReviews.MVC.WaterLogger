document.addEventListener('DOMContentLoaded', function () {
    setTimeout(function () {
        $("#successMessage").alert("close");
        $("#errorMessage").alert("close")
    }, 2500);
});

function openDeleteRecordModal(id, date) {
    $('#deleteRecordLabel').text('Delete ' + date);
    $('#deleteConfirmationMessage').text('Are you sure you want to delete the record of the"' + date + '"?');
    $('#input-id_deleteRecord').val(id);
    $('#deleteRecord').modal('show');
}

function openUpdateRecordModal(id, date, dateValue, quantity) {

    $('#input-date_updateRecord').val(dateValue);
    $('#input-quantity_updateRecord').val(quantity);
    $('#input-id_updateRecord').val(id);
    $('#updateRecordLabel').text('Edit ' + date);
    $('#updateRecord').modal('show')
};
