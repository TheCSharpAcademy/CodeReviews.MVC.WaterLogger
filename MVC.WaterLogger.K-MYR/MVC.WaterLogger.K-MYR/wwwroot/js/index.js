document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.habitCard').forEach(function (card) {
        card.addEventListener('click', function (event) {
            const id = card.dataset.habitId;
            if (!event.target.matches('img.img-fluid.delete-icon, img.img-fluid.update-icon ')) {
                window.location.href = '/Details/' + id;
            }
        });
    });

    $('#activity_add').attr('checked', true);
    setTimeout(function () {
        $("#successMessage").alert("close");
        $("#errorMessage").alert("close")
    }, 2500);
});

function openDeleteHabitModal(id, name) {
    $('#deleteHabitLabel').text('Delete ' + name);
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
