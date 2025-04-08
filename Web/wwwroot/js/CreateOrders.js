// Định nghĩa các phương thức thanh toán
const paymentMethods = ['card', 'cod', 'paypal'];

// Xử lý khi chọn phương thức thanh toán
paymentMethods.forEach(method => {
    const option = document.getElementById(`option-${method}`);
    const radio = document.getElementById(`radio-${method}`);
    const input = document.getElementById(`input-${method}`);

    option.addEventListener('click', function () {
        // Ẩn tất cả các chi tiết
        paymentMethods.forEach(m => {
            document.getElementById(`${m}-details`).classList.add('hidden');
            document.getElementById(`option-${m}`).classList.remove('selected');
            document.getElementById(`radio-${m}`).classList.remove('selected');
            document.getElementById(`input-${m}`).checked = false;
        });

        // Hiển thị chi tiết của phương thức đã chọn
        document.getElementById(`${method}-details`).classList.remove('hidden');
        option.classList.add('selected');
        radio.classList.add('selected');
        input.checked = true;
    });
});

// Chọn phương thức mặc định (chuyển khoản)
document.getElementById('option-card').click();