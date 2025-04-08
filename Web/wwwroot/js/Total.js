// ========== Phần biểu đồ doanh thu theo tháng ==========
var revenueDataByMonth = new Array(12).fill(0);
var orderCountByMonth = new Array(12).fill(0);

@foreach(var item in Model)
{
    var month = item.date.Month - 1;
    @: revenueDataByMonth[@month] += @item.tongdoanhthu;
    @: orderCountByMonth[@month] += 1;
}

const year = @Model.FirstOrDefault()?.date.Year;
document.getElementById("yearTitle").innerText = year || "N/A";

new Chart(document.getElementById("revenueChart"), {
    type: "bar",
    data: {
        labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
            "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
        datasets: [{
            label: "Doanh thu (VND)",
            data: revenueDataByMonth,
            backgroundColor: "rgba(66, 135, 245, 0.5)",
            borderColor: "rgba(66, 135, 245, 1)",
            borderWidth: 2,
            borderRadius: 8,
            barPercentage: 0.6,
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    callback: function (value) {
                        return value.toLocaleString("vi-VN", {
                            style: "currency",
                            currency: "VND",
                            maximumFractionDigits: 0
                        });
                    },
                },
            },
        },
        plugins: {
            legend: { display: true, position: "top" },
            tooltip: {
                callbacks: {
                    label: function (context) {
                        return context.dataset.label + ': ' +
                            context.parsed.y.toLocaleString('vi-VN') + ' VND';
                    }
                }
            }
        },
    },
});

// ========== Phần thống kê chi tiết ==========
// Tạo dữ liệu thống kê theo tháng
const monthlyStats = [];
for (let i = 0; i < 12; i++) {
    if (revenueDataByMonth[i] > 0) { // Chỉ hiển thị tháng có doanh thu
        monthlyStats.push({
            month: i + 1,
            year: year,
            orderCount: orderCountByMonth[i],
            totalRevenue: revenueDataByMonth[i],
            avgOrderValue: orderCountByMonth[i] > 0
                ? revenueDataByMonth[i] / orderCountByMonth[i]
                : 0
        });
    }
}

function calculatePercentageChange(current, previous) {
    if (previous === 0) return 0;
    return (((current - previous) / previous) * 100).toFixed(2);
}

const container = document.getElementById("statistics-container");

for (let i = 0; i < monthlyStats.length; i++) {
    const monthData = monthlyStats[i];
    const monthName = "Tháng " + monthData.month;

    let percentChange = "";
    if (i > 0) {
        const previousRevenue = monthlyStats[i - 1].totalRevenue;
        const change = calculatePercentageChange(monthData.totalRevenue, previousRevenue);
        const changeClass = change >= 0 ? "text-green-600" : "text-red-600";
        percentChange = `<div class="${changeClass} text-sm mt-2">Thay đổi so với tháng trước: ${change}%</div>`;
    }

    const statItem = document.createElement("div");
    statItem.className = "border-b border-gray-200 py-4 last:border-0";
    statItem.innerHTML = `
            <div class="text-lg font-semibold text-blue-600 mb-3">${monthData.year}/${monthName}</div>
            <div class="text-gray-700 grid grid-cols-2 gap-4">
                <div>
                    <span class="font-medium">Số đơn hàng:</span> ${monthData.orderCount.toLocaleString()}
                </div>
                <div>
                    <span class="font-medium">Doanh thu trung bình:</span> ${Math.round(monthData.avgOrderValue).toLocaleString('vi-VN')} VND
                </div>
                <div class="col-span-2">
                    <span class="font-medium">Tổng doanh thu:</span> ${monthData.totalRevenue.toLocaleString('vi-VN')} VND
                </div>
                ${percentChange}
            </div>
        `;
    container.appendChild(statItem);
}

// Cập nhật iframe blog
if (year) {
    document.getElementById("blogFrame").src = `doanhthublog.html?year=${year}`;
}