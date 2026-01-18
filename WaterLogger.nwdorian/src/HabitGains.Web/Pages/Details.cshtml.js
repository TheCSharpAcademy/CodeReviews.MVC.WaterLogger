document.addEventListener("DOMContentLoaded", () => {
  const canvas = document.getElementById("entries-chart");
  if (!canvas) return;

  const labels = JSON.parse(canvas.dataset.labels);
  const values = JSON.parse(canvas.dataset.values);

  new Chart(canvas, {
    type: "bar",
    data: {
      labels: labels,
      datasets: [
        {
          label: "Quantity",
          data: values,
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
    },
  });
});
