function toggleDropdown(event) {
    const dropdown = event.currentTarget.nextElementSibling;
    dropdown.classList.toggle("hidden");
}
let imageCount = 0; // Biến đếm hình ảnh

function addImageField() {
    const container = document.getElementById("imageFormContainer");

    // Tạo một div mới chứa trường nhập liệu cho hình ảnh và caption
    const newFieldContainer = document.createElement("div");
    newFieldContainer.className = "mb-4 col-span-2";
    newFieldContainer.id = `image-field-${imageCount}`; // Thêm id để dễ dàng xóa

    // Tạo một trường mới cho hình ảnh
    newFieldContainer.innerHTML = `
           <label class="block mb-2 text-sm font-medium">Hình ảnh sản phẩm</label>
           <input type="text"
                  class="w-full p-3 border border-gray-300 rounded-lg focus:outline-none focus:border-blue-500"
                  name="img[${imageCount}].ImagePath"
                  placeholder="Nhập tên hình ảnh" />
           <span asp-validation-for="img[${imageCount}].ImagePath" class="text-danger"></span>

           <label class="block mb-2 text-sm font-medium">Caption</label>
           <input type="text"
                  class="w-full p-3 border border-gray-300 rounded-lg"
                  name="img[${imageCount}].Caption"
                  placeholder="Nhập Caption cho hình ảnh" />
           <span asp-validation-for="img[${imageCount}].Caption" class="text-danger"></span>

           <!-- Thêm nút xóa -->
           <button type="button" class="px-4 mt-8 py-2 bg-red-500 text-white rounded-lg" onclick="removeImageField(${imageCount})">Xóa</button>
        `;

    // Thêm trường mới vào container
    container.appendChild(newFieldContainer);

    // Tăng biến đếm hình ảnh sau mỗi lần thêm
    imageCount++;
}
function removeImageField(count) {
    const fieldToRemove = document.getElementById(`image-field-${count}`);
    if (fieldToRemove) {
        fieldToRemove.remove(); // Xóa phần tử DOM
    }
    // Thêm sự kiện click cho toàn bộ tài liệu
    //document.addEventListener("click", function (event) {
    //    const dropdowns = document.querySelectorAll("#dropdownMenu");
    //    dropdowns.forEach((dropdown) => {
    //        const button = dropdown.previousElementSibling; // Nút tương ứng với dropdown
    //        // Kiểm tra xem nhấn vào nút hoặc dropdown không
    //        if (
    //            !button.contains(event.target) &&
    //            !dropdown.contains(event.target)
    //        ) {
    //            dropdown.classList.add("hidden"); // Ẩn dropdown nếu nhấn ra ngoài
    //        }
    //    });
    //});

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
    function addColorSizeField() {
        const container = document.getElementById("colorSizeContainer");
        const index = container.children.length;  // Xác định chỉ số cho trường mới

        // Tạo phần tử div mới chứa các trường nhập liệu
        const newField = document.createElement("div");
        newField.className = "flex items-center gap-4 mb-2";

        // Tạo HTML cho các trường nhập Màu sắc, Size và Số lượng, gán name đúng theo mảng
        newField.innerHTML = `
        <input type="text" 
               class="w-1/4 p-3 border border-gray-300 rounded-lg" 
               placeholder="Nhập màu sắc" name="variants[${index}].Name" />
        <input type="text" 
               class="w-1/4 p-3 border border-gray-300 rounded-lg" 
               placeholder="Nhập size" name="variants[${index}].Size[0].Name" />
        <input type="number" min="0" 
               class="w-1/4 p-3 border border-gray-300 rounded-lg" 
               placeholder="Nhập số lượng" name="variants[${index}].Size[0].quantity" />
        <button type="button" class="px-4 py-2 bg-red-500 text-white rounded-lg" onclick="removeColorSizeField(this)">Xóa</button>
    `;
        container.appendChild(newField);
    }


    function removeColorSizeField(button) {
        button.parentElement.remove();
    }

    function removeColorSizeField(element) {
        element.parentElement.remove();
    }

    function openDetailModal() {
        document.getElementById("detailModal").classList.remove("hidden");
    }
    //function closeDetailModal() {
    //    document.getElementById("detailModal").classList.add("hidden");
    //}
    //function saveDetail() {
    //    closeDetailModal();
    //}
    function showProductDetails() {
        openDetailModal();
    }
    document.querySelector('form').onsubmit = function () {
        const captionInput = document.getElementById('caption');
        const dateCreatedInput = document.createElement('input');
        dateCreatedInput.type = 'hidden';
        dateCreatedInput.name = 'img[0].DateCreated';
        dateCreatedInput.value = new Date().toISOString();
        captionInput.parentElement.appendChild(dateCreatedInput);
    };