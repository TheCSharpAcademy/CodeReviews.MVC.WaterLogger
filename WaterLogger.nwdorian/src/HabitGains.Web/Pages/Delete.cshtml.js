const deleteButton = document.getElementById("confirm-delete-button");
const deleteForm = document.getElementById("delete-form");

const submitForm = () => {
  deleteForm.submit();
};

deleteButton.addEventListener("click", submitForm);
