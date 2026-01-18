const tableRows = document.querySelectorAll(".clickable-row");
const optionButtons = document.querySelectorAll(".option-btn");

tableRows.forEach((row) => {
  row.addEventListener("click", () => {
    window.location.href = row.dataset.href;
  });
});

optionButtons.forEach((button) => {
  button.addEventListener("click", (e) => e.stopPropagation());
});
