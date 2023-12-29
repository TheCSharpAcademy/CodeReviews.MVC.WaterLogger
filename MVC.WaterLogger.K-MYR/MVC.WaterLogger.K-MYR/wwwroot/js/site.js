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
    $('#deleteForm #input-id_delete').val(id);
    $('#delete-habit').modal('show');
}

function openUpdateHabitModal(id, name, icon, measurement) {   
    
    $('#update-habit #' + icon.replace(".svg", "") + "_update").prop('checked', true);
    $('#update-habit #input-name_update').val(name);
    $('#update-habit #input-measurement_update').val(measurement);
    $('#updateForm #input-id_update').val(id);  
    $('#updateHabitLabel').text('Edit ' + name);  
    $('#update-habit').modal('show');
}

function openDeleteRecordModal(id, date) {
    $('#deleteHabitLabel').text('Delete ' + date);
    $('#deleteConfirmationMessage').text('Are you sure you want to delete the records from"' + date + '"?');
    $('#deleteRecordForm #input-id_delete').val(id);
    $('#delete-habit').modal('show');
}

function openUpdateRecordModal(id, date, quantity) {
        
    $('#update-record #input-date_update').val(date);
    $('#update-record #input-quantity_update').val(quantity);
    $('#updateForm #input-id_update').val(id);
    $('#updateRecordLabel').text('Edit ' + date);
    $('#update-record').modal('show');
}