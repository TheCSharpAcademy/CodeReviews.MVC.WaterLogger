// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showWeightHistory(logs = []) {
    if (logs.length === 0) {
        return;
    }

    const weightHistoryLogs = logs
        .sort(
            (logA, logB) =>
                new Date(logA.loggedDate) - new Date(logB.loggedDate)
        )
        .reduce((acc, log) => {
            const key = new Date(log.loggedDate).toLocaleDateString();

            if (!acc[key]) {
                acc[key] = 0;
            }

            acc[key] += log.weightValue ?? 0;

            return acc;
        }, {});

    new Chart(document.getElementById("weightChart"), {
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
                    text: "Weight history",
                },
            },
        },
        data: {
            labels: Object.keys(weightHistoryLogs),
            datasets: [
                {
                    label: `Weight /*(${logs[0].unitName})*/`,
                    data: Object.values(weightHistoryLogs),
                    fill: true,
                    cubicInterpolationMode: "monotone",
                    tension: 0.4,
                },
            ],
        },
    });
}


//function populateEditForm(id, loggedDate, weightValue) {
//    document.getElementById('editId').value = id;
//    document.getElementById('editDate').value = loggedDate;
//    document.getElementById('editWeight').value = weightValue;
//}
