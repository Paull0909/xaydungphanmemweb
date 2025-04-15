// Bộ đếm cho các input hình ảnh mới
let newImageCounter = 1;
let currentNewImageElement = null;
let newVariantIndex = 0;
let newSizeIndices = {};

// Hàm hiển thị modal xác nhận xóa cho hình ảnh từ cơ sở dữ liệu
function showDeleteModal(type, imageId, productId, returnUrl) {
    if (type === 'db') {
        // Cho hình ảnh từ cơ sở dữ liệu
        document.getElementById('deleteImageId').value = imageId;
        document.getElementById('deleteProductId').value = productId;
        document.getElementById('deleteReturnUrl').value = returnUrl;

        const modal = document.getElementById('deleteDbModal');
        const modalContent = document.getElementById('dbModalContent');

        // Hiển thị modal với hiệu ứng
        modal.classList.remove('hidden');
        setTimeout(() => {
            modalContent.classList.remove('scale-95', 'opacity-0');
            modalContent.classList.add('scale-100', 'opacity-100');
        }, 10);
    } else {
        console.log('Đang tìm hình ảnh có ID:', imageId);
        // Gán phần tử hình ảnh mới cho currentNewImageElement
        currentNewImageElement = document.querySelector(`[data-is-new-image="true"][data-new-image-id="${imageId}"]`);

        // Kiểm tra xem currentNewImageElement đã được gán đúng chưa
        console.log('Phần tử hình ảnh hiện tại:', currentNewImageElement);

        if (!currentNewImageElement) {
            console.log('Không tìm thấy phần tử hình ảnh để xóa');
            return; // Nếu không tìm thấy phần tử, kết thúc hàm
        }

        const modal = document.getElementById('deleteNewModal');
        const modalContent = document.getElementById('newModalContent');

        // Hiển thị modal với hiệu ứng
        modal.classList.remove('hidden');
        setTimeout(() => {
            modalContent.classList.remove('scale-95', 'opacity-0');
            modalContent.classList.add('scale-100', 'opacity-100');
        }, 10);
    }
}

// Hàm đóng modal xác nhận xóa cho hình ảnh từ cơ sở dữ liệu
function closeDeleteDbModal() {
    const modal = document.getElementById('deleteDbModal');
    const modalContent = document.getElementById('dbModalContent');

    // Ẩn modal với hiệu ứng
    modalContent.classList.remove('scale-100', 'opacity-100');
    modalContent.classList.add('scale-95', 'opacity-0');

    setTimeout(() => {
        modal.classList.add('hidden');
    }, 300);
}

// Hàm đóng modal xác nhận xóa cho hình ảnh mới
function closeDeleteNewModal() {
    const modal = document.getElementById('deleteNewModal');
    const modalContent = document.getElementById('newModalContent');

    // Ẩn modal với hiệu ứng
    modalContent.classList.remove('scale-100', 'opacity-100');
    modalContent.classList.add('scale-95', 'opacity-0');

    setTimeout(() => {
        modal.classList.add('hidden');
        currentNewImageElement = null;
    }, 300);
}

// Hàm xác nhận xóa hình ảnh từ cơ sở dữ liệu
function confirmDeleteDbImage() {
    document.getElementById('deleteImageForm').submit();
}

function confirmDeleteNewImage() {
    if (currentNewImageElement) {
        console.log('Phần tử hình ảnh hiện tại:', currentNewImageElement);  // Kiểm tra giá trị của currentNewImageElement
        removeNewImageInput(currentNewImageElement);
        closeDeleteNewModal();
    } else {
        console.log('Không có phần tử hình ảnh để xóa');
    }
}

// Hàm thêm input hình ảnh mới
function addNewImageInput() {
    // Ẩn thông báo "không có hình ảnh" nếu tồn tại
    const noImagesMessage = document.getElementById('noImagesMessage');
    if (noImagesMessage) {
        noImagesMessage.style.display = 'none';
    }

    // Tạo container hình ảnh mới
    const newImageContainer = document.createElement('div');
    newImageContainer.className = 'border border-gray-200 dark:border-gray-700 rounded-lg p-4 relative group hover:shadow-md transition-all duration-300 bg-white dark:bg-gray-800';

    // Gán data-new-image-id với giá trị chính xác của newImageCounter trước khi tăng
    newImageContainer.dataset.isNewImage = 'true';
    newImageContainer.dataset.newImageId = `new-${newImageCounter}`;

    // Lưu giá trị hiện tại của newImageCounter để truyền vào showDeleteModal
    const currentImageId = `new-${newImageCounter}`;

    // Tăng newImageCounter sau khi gán
    newImageCounter++;

    // Tạo vùng xem trước hình ảnh với giao diện cải tiến
    const previewContainer = document.createElement('div');
    previewContainer.className = 'h-36 w-full rounded-lg overflow-hidden mb-3 bg-gray-50 dark:bg-gray-700 flex items-center justify-center';
    previewContainer.innerHTML = `
            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 text-gray-300 dark:text-gray-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                <rect x="3" y="3" width="18" height="18" rx="2" ry="2"></rect>
                <circle cx="8.5" cy="8.5" r="1.5"></circle>
                <polyline points="21 15 16 10 5 21"></polyline>
            </svg>
        `;

    // Tạo input cho URL hình ảnh với giao diện cải tiến
    const urlInputWrapper = document.createElement('div');
    urlInputWrapper.className = 'mb-2';

    const urlInput = document.createElement('input');
    urlInput.type = 'text';
    urlInput.name = `ProductImages[${newImageCounter}].ImagePath`;
    urlInput.className = 'w-full p-2.5 border border-gray-300 dark:border-gray-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent text-sm transition-all bg-white dark:bg-gray-700 text-gray-900 dark:text-white';
    urlInput.placeholder = 'Nhập URL hình ảnh...';

    urlInputWrapper.appendChild(urlInput);

    // Tạo input cho chú thích
    const captionWrapper = document.createElement('div');
    captionWrapper.className = 'relative';

    const captionLabel = document.createElement('label');
    captionLabel.className = 'block mb-1 text-xs font-medium text-gray-600 dark:text-gray-400';
    captionLabel.textContent = 'Chú Thích';

    const captionInput = document.createElement('input');
    captionInput.type = 'text';
    captionInput.name = `ProductImages[${newImageCounter}].Caption`;
    captionInput.className = 'w-full p-2.5 border border-gray-300 dark:border-gray-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent text-sm transition-all bg-white dark:bg-gray-700 text-gray-900 dark:text-white';
    captionInput.placeholder = 'Nhập chú thích cho hình ảnh...';

    captionWrapper.appendChild(captionLabel);
    captionWrapper.appendChild(captionInput);

    // Thêm input ẩn product_id
    const productIdInput = document.createElement('input');
    productIdInput.type = 'hidden';
    productIdInput.name = `ProductImages[${newImageCounter}].product_id`;
    productIdInput.value = '@Model.product_id';

    // Tạo nút xóa với giao diện cải tiến - kiểu khác cho hình ảnh mới
    const deleteButton = document.createElement('button');
    deleteButton.type = 'button';
    deleteButton.className = 'absolute top-2 right-2 bg-red-500 text-white rounded-full p-2 shadow-md hover:bg-red-600 hover:scale-105 transform transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2';
    deleteButton.innerHTML = `
            <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <line x1="18" y1="6" x2="6" y2="18"></line>
                <line x1="6" y1="6" x2="18" y2="18"></line>
             </svg>
        `;
    deleteButton.onclick = function () {
        showDeleteModal('new', currentImageId);  // Gọi showDeleteModal và truyền ID chính xác
    };

    // Thêm trình nghe sự kiện để cập nhật xem trước khi URL thay đổi
    urlInput.addEventListener('input', function () {
        updateImagePreview(this, previewContainer);
    });

    // Lắp ráp container hình ảnh mới
    newImageContainer.appendChild(previewContainer);
    newImageContainer.appendChild(productIdInput);
    newImageContainer.appendChild(urlInputWrapper);
    newImageContainer.appendChild(captionWrapper);
    newImageContainer.appendChild(deleteButton);

    // Thêm vào container
    const newImagesContainer = document.getElementById('newImagesContainer');
    if (newImagesContainer) {
        newImagesContainer.appendChild(newImageContainer);
    }
}

// Hàm cập nhật xem trước hình ảnh với xử lý lỗi cải tiến
function updateImagePreview(input, previewContainer) {
    const imageUrl = input.value.trim();

    if (imageUrl) {
        // Tạo phần tử hình ảnh
        const img = document.createElement('img');
        img.className = 'h-full w-full object-cover transition-transform duration-300 group-hover:scale-105';
        img.alt = 'Hình ảnh sản phẩm';

        // Hiển thị chỉ báo đang tải
        previewContainer.innerHTML = `
                <div class="flex flex-col items-center justify-center">
                    <svg class="animate-spin h-8 w-8 text-purple-500" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                        <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                        <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                    </svg>
                    <span class="text-xs text-gray-500 dark:text-gray-400 mt-2">Đang tải...</span>
                </div>
            `;

        img.onerror = function () {
            // Nếu hình ảnh không tải được, hiển thị placeholder lỗi
            previewContainer.innerHTML = `
                    <div class="flex flex-col items-center justify-center text-center p-2">
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-10 w-10 text-red-400 mb-1" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                            <circle cx="12" cy="12" r="10"></circle>
                            <line x1="12" y1="8" x2="12" y2="12"></line>
                            <line x1="12" y1="16" x2="12.01" y2="16"></line>
                        </svg>
                        <span class="text-xs text-red-500">URL hình ảnh không hợp lệ</span>
                    </div>
                `;
        };

        img.onload = function () {
            // Nếu hình ảnh tải thành công, hiển thị nó
            previewContainer.innerHTML = '';
            previewContainer.appendChild(img);
        };

        img.src = imageUrl;
    } else {
        // Nếu URL trống, hiển thị placeholder
        previewContainer.innerHTML = `
                <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 text-gray-300 dark:text-gray-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                    <rect x="3" y="3" width="18" height="18" rx="2" ry="2"></rect>
                    <circle cx="8.5" cy="8.5" r="1.5"></circle>
                    <polyline points="21 15 16 10 5 21"></polyline>
                </svg>
            `;
    }
}

// Hàm xóa input hình ảnh mới với xử lý trạng thái trống cải tiến
function removeNewImageInput(element) {
    if (!element) return;

    const container = typeof element === 'object' ? element : element.closest('[data-is-new-image="true"]');

    if (container) {
        // Thêm hiệu ứng mờ dần
        container.classList.add('opacity-0');
        container.style.transition = 'opacity 0.3s ease';

        // Xóa sau khi hoàn thành hiệu ứng
        setTimeout(() => {
            container.remove();

            // Hiển thị thông báo "không có hình ảnh" nếu không còn hình ảnh nào
            const dbImages = document.querySelectorAll('[data-is-db-image="true"]');
            const newImages = document.querySelectorAll('[data-is-new-image="true"]');

            if (dbImages.length === 0 && newImages.length === 0) {
                const noImagesMessage = document.getElementById('noImagesMessage');
                if (noImagesMessage) {
                    noImagesMessage.style.display = 'block';
                } else {
                    // Tạo thông báo nếu nó không tồn tại
                    const message = document.createElement('div');
                    message.id = 'noImagesMessage';
                    message.className = 'col-span-full text-center py-8 text-gray-500 dark:text-gray-400 bg-gray-50 dark:bg-gray-700 rounded-lg border border-dashed border-gray-300 dark:border-gray-600';
                    message.innerHTML = `
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 mx-auto text-gray-400 dark:text-gray-500 mb-2" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                                <rect x="3" y="3" width="18" height="18" rx="2" ry="2"></rect>
                                <circle cx="8.5" cy="8.5" r="1.5"></circle>
                                <polyline points="21 15 16 10 5 21"></polyline>
                            </svg>
                            <p>Không có hình ảnh bổ sung.</p>
                        `;
                    document.getElementById('ProductImagesContainer').appendChild(message);
                }
            }
        }, 300);
    }
}

// Thiết lập trình nghe sự kiện khi tài liệu đã sẵn sàng
document.addEventListener('DOMContentLoaded', function () {
    // Kiểm tra nếu nút 'confirmDeleteDbBtn' tồn tại trước khi thêm trình nghe sự kiện
    const confirmDeleteDbBtn = document.getElementById('confirmDeleteDbBtn');
    if (confirmDeleteDbBtn) {
        confirmDeleteDbBtn.addEventListener('click', confirmDeleteDbImage);
    }

    // Kiểm tra nếu nút 'confirmDeleteNewBtn' tồn tại trước khi thêm trình nghe sự kiện
    const confirmDeleteNewBtn = document.getElementById('confirmDeleteNewBtn');
    if (confirmDeleteNewBtn) {
        confirmDeleteNewBtn.addEventListener('click', confirmDeleteNewImage);
    }

    // Kiểm tra nếu các container modal tồn tại trước khi thêm trình nghe sự kiện để đóng khi nhấp bên ngoài
    const deleteDbModal = document.getElementById('deleteDbModal');
    if (deleteDbModal) {
        deleteDbModal.addEventListener('click', function (e) {
            if (e.target === this) {
                closeDeleteDbModal();
            }
        });
    }

    const deleteNewModal = document.getElementById('deleteNewModal');
    if (deleteNewModal) {
        deleteNewModal.addEventListener('click', function (e) {
            if (e.target === this) {
                closeDeleteNewModal();
            }
        });
    }

    // Thêm hỗ trợ bàn phím cho modal (Escape để đóng)
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            if (deleteDbModal && !deleteDbModal.classList.contains('hidden')) {
                closeDeleteDbModal();
            }
            if (deleteNewModal && !deleteNewModal.classList.contains('hidden')) {
                closeDeleteNewModal();
            }
        }
    });
});

// Các hàm cho biến thể sản phẩm
function addVariant() {
    const container = document.getElementById('variantsContainer');
    const template = document.getElementById('newVariantTemplate').innerHTML;
    const newVariant = template.replace(/{variantIndex}/g, newVariantIndex);

    // Kiểm tra nếu có thông báo "không có biến thể" và xóa nó
    if (container.children.length === 1 && container.firstChild.tagName !== 'DIV') {
        container.innerHTML = '';
    }

    // Tạo một div cho biến thể mới và thêm vào container
    const variantDiv = document.createElement('div');
    variantDiv.innerHTML = newVariant;
    container.appendChild(variantDiv);

    // Khởi tạo bộ đếm chỉ số kích thước cho biến thể này
    newSizeIndices[newVariantIndex] = 0;

    newVariantIndex++;
}

function removeVariant(button) {
    const variantItem = button.closest('.variant-item');
    const variantId = variantItem.querySelector('input[name$=".Variant_Id"]')?.value;

    if (variantId) {
        // Thêm một input ẩn để đánh dấu biến thể này để xóa
        const form = button.closest('form');
        const deletedvariantsInput = document.createElement('input');
        deletedvariantsInput.type = 'hidden';
        deletedvariantsInput.name = 'DeletedVariantIds';
        deletedvariantsInput.value = variantId;
        form.appendChild(deletedvariantsInput);
    }

    // Xóa biến thể khỏi giao diện
    variantItem.remove();

    // Nếu không còn biến thể nào, hiển thị thông báo "không có biến thể"
    const container = document.getElementById('variantsContainer');
    if (container.children.length === 0) {
        container.innerHTML = `
                <div class="text-center py-8 text-gray-500 dark:text-gray-400">
                    Không có biến thể. Nhấn "Thêm Biến Thể" để thêm biến thể sản phẩm.
                </div>
            `;
    }
}

function removeNewVariant(button) {
    button.closest('.variant-item').remove();

    // Nếu không còn biến thể nào, hiển thị thông báo "không có biến thể"
    const container = document.getElementById('variantsContainer');
    if (container.children.length === 0) {
        container.innerHTML = `
                <div class="text-center py-8 text-gray-500 dark:text-gray-400">
                    Không có biến thể. Nhấn "Thêm Biến Thể" để thêm biến thể sản phẩm.
                </div>
            `;
    }
}

// Các hàm cho kích thước
function addSize(button, variantIndex) {
    const sizesContainer = button.closest('.variant-item').querySelector('.sizes-container');
    const noSizesMessage = sizesContainer.querySelector('.text-center');

    if (noSizesMessage) {
        noSizesMessage.remove();
    }

    // Tính toán chỉ số kích thước mới dựa trên số lượng kích thước hiện tại trong container
    const sizeIndex = sizesContainer.children.length;

    // Tạo một mục kích thước mới
    const sizeItem = document.createElement('div');
    sizeItem.className = 'size-item flex items-center gap-3 p-2 bg-white dark:bg-gray-800 rounded-lg border border-gray-100 dark:border-gray-700';
    sizeItem.innerHTML = `
            <div class="flex-1">
                <input type="text" name="variants[${variantIndex}].Size[${sizeIndex}].Name" class="w-full p-2 border border-gray-300 dark:border-gray-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent transition bg-white dark:bg-gray-700 text-gray-900 dark:text-white" placeholder="Tên kích thước (VD: S, M, L, XL)" />
            </div>
            <div class="w-24">
                <input type="number" name="variants[${variantIndex}].Size[${sizeIndex}].quantity" class="w-full p-2 border border-gray-300 dark:border-gray-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 focus:border-transparent transition bg-white dark:bg-gray-700 text-gray-900 dark:text-white" placeholder="Số lượng" min="0" value="0" />
            </div>
            <button type="button" onclick="removeSize(this)" class="p-1 text-red-500 hover:text-red-700 dark:text-red-400 dark:hover:text-red-300 transition">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <line x1="18" y1="6" x2="6" y2="18"></line>
                    <line x1="6" y1="6" x2="18" y2="18"></line>
                </svg>
            </button>
        `;

    sizesContainer.appendChild(sizeItem);
}

function addNewSize(button, variantIndex) {
    const sizesContainer = button.closest('.variant-item').querySelector('.sizes-container');
    const template = document.getElementById('newSizeTemplate').innerHTML;

    // Lấy chỉ số kích thước hiện tại cho biến thể này
    if (!newSizeIndices[variantIndex]) {
        newSizeIndices[variantIndex] = 0;
    }

    const sizeIndex = newSizeIndices[variantIndex];
    const newSize = template
        .replace(/{variantIndex}/g, variantIndex)
        .replace(/{sizeIndex}/g, sizeIndex);

    // Tạo một div cho kích thước mới và thêm vào container
    const sizeDiv = document.createElement('div');
    sizeDiv.innerHTML = newSize;
    sizesContainer.appendChild(sizeDiv);

    // Tăng chỉ số kích thước cho biến thể này
    newSizeIndices[variantIndex]++;
}

function removeSize(button) {
    const sizeItem = button.closest('.size-item');
    const sizeId = sizeItem.querySelector('input[name$=".Id"]')?.value;

    if (sizeId) {
        // Thêm một input ẩn để đánh dấu kích thước này để xóa
        const form = button.closest('form');
        const deletedSizesInput = document.createElement('input');
        deletedSizesInput.type = 'hidden';
        deletedSizesInput.name = 'DeletedSizeIds';
        deletedSizesInput.value = sizeId;
        form.appendChild(deletedSizesInput);
    }

    // Xóa kích thước khỏi giao diện
    sizeItem.remove();

    // Nếu không còn kích thước nào, hiển thị thông báo "không có kích thước"
    const sizesContainer = button.closest('.sizes-container');
    if (sizesContainer.children.length === 0) {
        sizesContainer.innerHTML = `
                <div class="text-center py-3 text-gray-500 dark:text-gray-400 text-sm">
                    Không có kích thước. Nhấn "Thêm Kích Thước" để thêm kích thước cho biến thể này.
                </div>
            `;
    }
}

function removeNewSize(button) {
    const sizeItem = button.closest('.size-item');
    sizeItem.remove();

    // Nếu không còn kích thước nào, hiển thị thông báo "không có kích thước"
    const sizesContainer = button.closest('.sizes-container');
    if (sizesContainer.children.length === 0) {
        sizesContainer.innerHTML = `
                <div class="text-center py-3 text-gray-500 dark:text-gray-400 text-sm">
                    Không có kích thước. Nhấn "Thêm Kích Thước" để thêm kích thước cho biến thể này.
                </div>
            `;
    }
}

// Hàm xác nhận xóa
async function confirmDeleteSize(sizeId) {
    const isConfirmed = confirm("Bạn có chắc chắn muốn xóa kích thước này?");
    if (!isConfirmed) return;

    try {
        // Gọi API DELETE bằng fetch
        const response = await fetch(`/Size/Delete/${sizeId}`, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            // Tải lại trang nếu xóa thành công
            location.reload();  // Tải lại trang hiện tại
        } else {
            alert("Xóa không thành công!");
        }
    } catch (error) {
        console.error("Lỗi:", error);
    }
}