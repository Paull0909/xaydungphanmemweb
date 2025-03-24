function toggleDropdown(event) {
    const dropdown = event.currentTarget.nextElementSibling;
    dropdown.classList.toggle("hidden");
}

// Thêm sự kiện click cho toàn bộ tài liệu
document.addEventListener("click", function (event) {
    const dropdowns = document.querySelectorAll("#dropdownMenu");
    dropdowns.forEach((dropdown) => {
        const button = dropdown.previousElementSibling; // Nút tương ứng với dropdown
        // Kiểm tra xem nhấn vào nút hoặc dropdown không
        if (
            !button.contains(event.target) &&
            !dropdown.contains(event.target)
        ) {
            dropdown.classList.add("hidden"); // Ẩn dropdown nếu nhấn ra ngoài
        }
    });
});

function openAddProductModal() {
    document.getElementById("addProductModal").classList.remove("hidden");
}

function closeAddProductModal() {
    document.getElementById("addProductModal").classList.add("hidden");
}

// Thêm sản phẩm
function addProduct() {
    const productName = document.getElementById("productName").value;
    const productPrice = document.getElementById("productPrice").value;
    const discount = document.getElementById("discount").value;
    const productStatus = document.getElementById("productStatus").value;
    const productHighlight =
        document.getElementById("productHighlight").value;
    const productCategory =
        document.getElementById("productCategory").value;
    const productDescription =
        document.getElementById("productDescription").value;
    const productAd = document.getElementById("productAd").value;

    // Lấy danh sách màu sắc, size và số lượng
    const colorSizeFields = document.querySelectorAll(
        "#colorSizeContainer .flex"
    );
    const colorSizeData = Array.from(colorSizeFields).map((field) => ({
        color: field.querySelector('input[placeholder="Nhập màu sắc"]').value,
        size: field.querySelector('input[placeholder="Nhập size"]').value,
        quantity: field.querySelector('input[placeholder="Nhập số lượng"]')
            .value,
    }));
}

function generateFields() {
    const cataProductCount = parseInt(document.getElementById("ProductCount").value) || 0;
    const sizeCount = parseInt(document.getElementById("sizeCount").value) || 0;
    const fieldsContainer = document.getElementById("fieldsContainer");
    fieldsContainer.innerHTML = "";

    // Tạo các biến thể sản phẩm (có thể thêm nhiều biến thể)
    for (let i = 0; i < cataProductCount; i++) {
        const productDiv = document.createElement("div");
        productDiv.classList.add(
            "bg-gray-50",
            "p-4",
            "rounded-lg",
            "shadow-md",
            "mb-4"
        );

        productDiv.innerHTML = `
                  <div class="flex justify-between items-center mb-2">
                      <h3 class="font-semibold">Biến thể sản phẩm ${i + 1}</h3>
                      <button onclick="removeProduct(this)" class="text-red-500 text-sm font-semibold">Xóa</button>
                  </div>
                  <!-- Màu sắc sản phẩm -->
                   <input type="text" name="[${i}].Name" class="w-full p-2 border rounded mb-3" required />    
                  <!-- Truyền product_id nếu cần -->
                  <input type="hidden" name="variants[${i}].product_id" value="/* Giá trị product_id */">

                  <div id="sizesContainer-${i}">
                      ${generateSizeFields(i, sizeCount)}
                  </div>
              `;

        fieldsContainer.appendChild(productDiv);
    }
}

function generateSizeFields(variantIndex, sizeCount) {
    let html = "";
    for (let j = 0; j < sizeCount; j++) {
        html += `
                  <div class="flex gap-2 mb-2">
                      <!-- Tên Size -->
                    <input type="text" name="[${variantIndex}].sizes[${j}].Name" class="flex-1 p-2 border rounded" placeholder="Tên size" />
                  <input type="number" name="[${variantIndex}].sizes[${j}].quantity" class="w-1/4 p-2 border rounded" placeholder="Số lượng" min="1" />
                      <button onclick="removeElement(this)" class="text-red-500 text-sm"><i class="fas fa-trash"></i></button>
                  </div>
              `;
    }
    return html;
}


function removeElement(element) {
    element.closest(".flex").remove(); // Xóa Size cụ thể
}

function removeProduct(element) {
    element.closest(".bg-gray-50").remove(); // Xóa toàn bộ Cata Product và Sizes
}

// Tự động tạo trường khi tải trang
document.addEventListener("DOMContentLoaded", generateFields);

function removeColorSizeField(element) {
    element.parentElement.remove();
}

function openDetailModal() {
    document.getElementById("detailModal").classList.remove("hidden");
}

function showProductDetails() {
    openDetailModal();
}