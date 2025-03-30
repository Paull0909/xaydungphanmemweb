document.addEventListener("DOMContentLoaded", function () {
    checkForm();
});

let selectedColor = '';
let selectedSize = '';
let availableSizeCount = 0;
function selectColor(color) {
    selectedColor = color;
    document.getElementById("selectedColorInput").value = color;

    document.querySelectorAll(".size-group").forEach(el => el.classList.add("hidden"));

    let sizeGroup = document.querySelector(`.size-group[data-color="${color}"]`);
    if (sizeGroup) {
        sizeGroup.classList.remove("hidden");
    }

    document.getElementById("quantity-info").textContent = `Vui lòng chọn size để xem số lượng sản phẩm.`;

    checkForm();
}
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

function checkForm() {
    const submitButton = document.querySelector("#addToCart");

    if (selectedColor && selectedSize) {
        submitButton.removeAttribute("disabled");
        submitButton.classList.remove("bg-gray-300", "text-gray-600");
        submitButton.classList.add("bg-blue-500", "text-white");
    } else {
        submitButton.setAttribute("disabled", "true");
        submitButton.classList.add("bg-gray-300", "text-gray-600");
        submitButton.classList.remove("bg-blue-500", "text-white");
    }
}
document.addEventListener("DOMContentLoaded", function () {
    const submitButton = document.querySelector("#addToCart");
    if (submitButton) {
        submitButton.disabled = true;
    }
});
