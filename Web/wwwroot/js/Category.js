// Hàm để chuyển đổi trạng thái hiển thị của modal
const toggleModal = (modalId) => {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.classList.toggle("hidden");
    }
};


// Các hàm để mở các modal cụ thể
const addCategoryModal = () => {
    toggleModal("addCategoryModal");
};
const editCategoryModal = (id, name) => {
    toggleModal("editCategoryModal");  // Hiển thị modal
    document.getElementById("category-id").value = id;  // Điền ID vào trường ẩn
    document.getElementById("category-name").value = name;  // Điền tên vào trường nhập
};


const deleteCategoryModal = () => {
    // Tạo hộp thoại xác nhận sử dụng hàm confirm có sẵn của trình duyệt
    if (confirm("Bạn có chắc chắn muốn xóa danh mục này không?")) {
        // Xử lý hành động xóa tại đây
        alert("Đã xóa danh mục thành công");
    }
};

// Đóng bất kỳ modal nào đang mở
const closeModal = (modalId) => {
    toggleModal(modalId);
};

// Xử lý các hành động lưu cho các biểu mẫu khác nhau
const saveCategory = (modalId) => {
    // Thông thường bạn sẽ lấy dữ liệu biểu mẫu và xử lý nó tại đây
    // Hiện tại, chỉ hiển thị thông báo thành công và đóng modal
    alert("Đã lưu thông tin danh mục thành công");
    toggleModal(modalId);
};

// Lắng nghe sự kiện cho các nút đóng trong các modal
document.querySelectorAll('[data-modal-hide]').forEach(button => {
    button.addEventListener('click', () => {
        const modalId = button.getAttribute('data-modal-hide');
        toggleModal(modalId);
    });
});

const closeDetailModal = () => {
    // Trước tiên kiểm tra modal nào đang mở
    const addModal = document.getElementById("addCategoryModal");
    const editModal = document.getElementById("editCategoryModal");

    // Kiểm tra nếu phần tử modal tồn tại và không có lớp "hidden"
    if (addModal && !addModal.classList.contains("hidden")) {
        closeModal("addCategoryModal");  // Đóng modal addCategoryModal
    } else if (editModal && !editModal.classList.contains("hidden")) {
        closeModal("editCategoryModal"); // Đóng modal editCategoryModal
    }
};

const saveDetail = () => {
    // Trước tiên kiểm tra modal nào đang mở
    const addModal = document.getElementById("addCategoryModal");
    const editModal = document.getElementById("editCategoryModal");

    if (!addModal.classList.contains("hidden")) {
        saveCategory("addCategoryModal");
    } else if (!editModal.classList.contains("hidden")) {
        saveCategory("editCategoryModal");
    }
};