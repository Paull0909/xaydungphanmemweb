let selectedColor = '';
let selectedSize = '';
let availableSizeCount = 0;


function selectColor(color) {
    selectedColor = color;

    // Cập nhật giá trị cho cả 2 input
    const colorInput1 = document.getElementById("selectedColorInput");
    const colorInput2 = document.getElementById("selectedColorInputLoai");
    if (colorInput1 && colorInput2) {
        colorInput1.value = color;
        colorInput2.value = color;
    }

    // Ẩn/hiện size group tương ứng
    updateSizeGroups(color);

    // Thông báo cho người dùng
    document.getElementById("quantity-info").textContent = "Vui lòng chọn size để xem số lượng sản phẩm.";

    checkForm();
}
document.addEventListener("DOMContentLoaded", function () {
    checkForm();
});


function selectSize(size, stock) {
    selectedSize = size;
    availableStock = stock;
    document.getElementById("quantity-info").textContent = `Size ${size} còn lại ${stock} sản phẩm.`;
    document.getElementById('Quantity').setAttribute('max', availableStock);
    let quantityInput = document.getElementById('Quantity');
    if (parseInt(quantityInput.value) > availableStock) {
        quantityInput.value = availableStock;
    }
    document.getElementById('selectedSizeInput').value = size;

    checkForm();
}

document.getElementById('Quantity').addEventListener('input', function () {
    const maxQuantity = parseInt(this.getAttribute('max'));
    let currentQuantity = parseInt(this.value);
    if (currentQuantity > maxQuantity) {
        alert(`Số lượng không thể vượt quá ${maxQuantity}.`);
        this.value = maxQuantity;
    }
});


document.addEventListener("DOMContentLoaded", function () {
    const submitButton = document.querySelector("#addToCart");
    if (submitButton) {
        submitButton.disabled = true;
    }
});

function updateSizeGroups(color) {
    document.querySelectorAll(".size-group").forEach(el => el.classList.add("hidden"));
    const sizeGroup = document.querySelector(`.size-group[data-color="${color}"]`);
    if (sizeGroup) sizeGroup.classList.remove("hidden");
}
document.addEventListener("DOMContentLoaded", function () {
    let userId = document.getElementById("userIdInput").value.trim();

    console.log("User ID từ input:", userId);

    document.getElementById("cartForm").addEventListener("submit", function (event) {
        if (!userId) {
            event.preventDefault();
            document.getElementById("loginModal").classList.remove("hidden");
        }
    });
});
document.getElementById("okButton").onclick = function () {
    window.location.href = "/Account/Login";
};


// Đồng bộ giá trị khi trang load xong
document.addEventListener("DOMContentLoaded", function () {
    const visibleInput = document.getElementById('Quantity');
    const hiddenInput = document.getElementById('soluongHidden');

    // Đảm bảo cả 2 input tồn tại
    if (visibleInput && hiddenInput) {
        // Cập nhật giá trị ban đầu
        hiddenInput.value = visibleInput.value;

        // Theo dõi thay đổi trên input hiển thị
        visibleInput.addEventListener('input', function () {
            hiddenInput.value = this.value;

            // Kiểm tra số lượng tối đa nếu cần
            const max = parseInt(this.getAttribute('max')) || Infinity;
            if (parseInt(this.value) > max) {
                alert(`Số lượng không thể vượt quá ${max}`);
                this.value = max;
                hiddenInput.value = max;
            }
        });
    }
});