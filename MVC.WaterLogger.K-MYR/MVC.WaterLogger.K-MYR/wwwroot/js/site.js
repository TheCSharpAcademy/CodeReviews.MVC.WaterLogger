document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.habitCard').forEach(function(card) { 
        card.addEventListener('click', function (event) { 
            const id = card.dataset.habitId;
            if (!event.target.matches('img.img-fluid.delete-icon, img.img-fluid.update-icon ')) {                               
                window.location.href = '/Details/' + id;
            }            
        });
    });


    $('#activity_add').attr('checked', true);
});

function openDeleteHabitModal(id, name) {
    $('#deleteHabitLabel').text('Delete ' + name );
    $('#deleteConfirmationMessage').text('Are you sure you want to delete "' + name + '"? All records will be deleted permanently!');    
    $('#input-id_delete').val(id);
    $('#delete-habit').modal('show');
}

function openUpdateHabitModal(id, name, icon, measurement) {   
    
    $('#' + icon.replace(".svg", "") + "_update").prop('checked', true);
    $('#input-name_update').val(name);
    $('#input-measurement_update').val(measurement);
    $('#input-id_update').val(id);  
    $('#updateHabitLabel').text('Edit ' + name);  
    $('#update-habit').modal('show');
}

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
    $('#updateRecord').modal('show');
}

$(document).ready(function() {
    setTimeout(function() {
        $("#successMessage").alert("close");
        $("#errorMessage").alert("close")}, 3000);
});

