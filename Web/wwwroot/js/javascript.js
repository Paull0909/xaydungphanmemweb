//Navbar scroll menu
document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.getElementById("menu-toggle");
    const mobileMenu = document.getElementById("mobile-menu");
    const closeMenu = document.getElementById("close-menu");
  
    menuToggle.addEventListener("click", () => {
      mobileMenu.classList.toggle("hidden");
    });
  
    closeMenu.addEventListener("click", () => {
      mobileMenu.classList.add("hidden");
    });
  
    // Close menu when clicking outside
    document.addEventListener("click", (event) => {
      if (
        !mobileMenu.contains(event.target) &&
        !menuToggle.contains(event.target)
      ) {
        mobileMenu.classList.add("hidden");
      }
    });
  });
  
  
function changeImage(src) {
  document.getElementById("mainImage").src = src;
}

document.addEventListener("DOMContentLoaded", function () {
  var sizeButtons = document.querySelectorAll(".size-button");
  var addToCartButton = document.getElementById("add-to-cart");

  sizeButtons.forEach(function (button) {
    button.addEventListener("click", function () {
      // Loại bỏ lớp 'active' từ tất cả các nút
      sizeButtons.forEach(function (btn) {
        btn.classList.remove("active");
      });

      // Thêm lớp 'active' vào nút được chọn
      this.classList.add("active");

      // Bật nút "ADD TO CART" và thêm lớp 'enabled'
      addToCartButton.removeAttribute("disabled");
      addToCartButton.classList.add("enabled");
    });
  });
});

function toggleSizeInfo() {
  var sizeInfo = document.getElementById("sizeInfo");
  if (sizeInfo.classList.contains("hidden")) {
    sizeInfo.classList.remove("hidden");
  } else {
    sizeInfo.classList.add("hidden");
  }
}
function toggleSection(id) {
  const section = document.getElementById(id);
  const arrow = document.querySelector(`[onclick="toggleSection('${id}')"]`);

  section.classList.toggle("hidden");
  arrow.classList.toggle("active");
}

//Slide show
document.addEventListener("DOMContentLoaded", function () {
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
  });


  
//Login and Register form
document.addEventListener("DOMContentLoaded", function () {
  function togglePasswordVisibility(inputId, buttonId) {
    const input = document.getElementById(inputId);
    const button = document.getElementById(buttonId);
    const icon = button.querySelector("i");

    button.addEventListener("click", function () {
      if (input.type === "password") {
        input.type = "text";
        icon.classList.remove("fa-eye");
        icon.classList.add("fa-eye-slash");
      } else {
        input.type = "password";
        icon.classList.remove("fa-eye-slash");
        icon.classList.add("fa-eye");
      }
    });
  }

  togglePasswordVisibility("password", "togglePassword");
  togglePasswordVisibility("confirmPassword", "toggleConfirmPassword");
});