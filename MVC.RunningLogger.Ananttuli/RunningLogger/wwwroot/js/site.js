function showRunningHistoryChart(logs = []) {
  if (logs.length === 0) {
    return;
  }

  const dateRunningLogs = logs
    .sort(
      (dateA, dateB) =>
        new Date(dateA.startDateTime) < new Date(dateB.startDateTime)
    )
    .reduce((acc, log) => {
      const key = new Date(log.startDateTime).toLocaleDateString();

      if (!acc[key]) {
        acc[key] = 0;
      }

      acc[key] += log.quantity ?? 0;

      return acc;
    }, {});

  new Chart(document.getElementById("running-chart"), {
    type: "line",
    options: {
      responsive: true,
      animation: true,
      plugins: {
        legend: {
          display: true,
        },
        tooltip: {
          enabled: true,
        },
        title: {
          display: true,
          text: "Running history",
        },
      },
    },
    data: {
      labels: Object.keys(dateRunningLogs),
      datasets: [
        {
          label: `Run distance (${logs[0].unitName})`,
          data: Object.values(dateRunningLogs),
          fill: true,
          cubicInterpolationMode: "monotone",
          tension: 0.4,
        },
      ],
    },
  });
}
