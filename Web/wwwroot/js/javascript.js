document.addEventListener("DOMContentLoaded", function () {
    // ============ NAVBAR MENU ============
    const menuToggle = document.getElementById("menu-toggle");
    const mobileMenu = document.getElementById("mobile-menu");
    const closeMenu = document.getElementById("close-menu");

    if (menuToggle && mobileMenu) {
        menuToggle.addEventListener("click", (e) => {
            e.stopPropagation();
            mobileMenu.classList.toggle("hidden");
        });
    }

    if (closeMenu && mobileMenu) {
        closeMenu.addEventListener("click", () => {
            mobileMenu.classList.add("hidden");
        });
    }

    // Close menu when clicking outside
    if (mobileMenu && menuToggle) {
        document.addEventListener("click", (event) => {
            if (!mobileMenu.contains(event.target) && !menuToggle.contains(event.target)) {
                mobileMenu.classList.add("hidden");
            }
        });
    }

    // ============ PRODUCT IMAGE GALLERY ============
    function changeImage(src) {
        const mainImage = document.getElementById("mainImage");
        if (mainImage) mainImage.src = src;
    }

    // ============ SIZE SELECTION ============

    // ============ SIZE INFO TOGGLE ============
    function toggleSizeInfo() {
        const sizeInfo = document.getElementById("sizeInfo");
        if (sizeInfo) sizeInfo.classList.toggle("hidden");
    }

    // ============ SECTION TOGGLE ============
    function toggleSection(id) {
        const section = document.getElementById(id);
        const arrow = document.querySelector(`[onclick="toggleSection('${id}')"]`);

        if (section) section.classList.toggle("hidden");
        if (arrow) arrow.classList.toggle("active");
    }

    // ============ SLIDER ============
    const slider = document.querySelector(".mySwiper");
    if (slider) {
        var swiper = new Swiper(".mySwiper", {
            loop: true,
            autoplay: {
                delay: 3000,
                disableOnInteraction: false,
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
        });
    }

    // ============ PASSWORD TOGGLE ============
    function setupPasswordToggle(inputId, buttonId) {
        const input = document.getElementById(inputId);
        const button = document.getElementById(buttonId);

        if (input && button) {
            const icon = button.querySelector("i");
            button.addEventListener("click", function () {
                if (input.type === "password") {
                    input.type = "text";
                    icon?.classList.replace("fa-eye", "fa-eye-slash");
                } else {
                    input.type = "password";
                    icon?.classList.replace("fa-eye-slash", "fa-eye");
                }
            });
        }
    }

    setupPasswordToggle("password", "togglePassword");
    setupPasswordToggle("confirmPassword", "toggleConfirmPassword");
});

// Make functions available globally
window.changeImage = changeImage;
window.toggleSizeInfo = toggleSizeInfo;
window.toggleSection = toggleSection;

