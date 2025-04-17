
// Simple Alpine.js-like functionality
document.addEventListener('DOMContentLoaded', function () {
    // Find all elements with x-data attribute
    document.querySelectorAll('[x-data]').forEach(el => {
        // Extract the data object
        const dataAttr = el.getAttribute('x-data');
        const data = dataAttr ? eval(`(${dataAttr})`) : {};

        el.querySelectorAll('[\\@@click]').forEach(clickEl => {
            const clickAttr = clickEl.getAttribute('@@click');
            clickEl.addEventListener('click', (e) => {
                // Simple evaluation of the click attribute
                if (clickAttr.includes('open = !open')) {
                    data.open = !data.open;

                    // Find elements with x-show attribute
                    el.querySelectorAll('[x-show]').forEach(showEl => {
                        if (data.open) {
                            showEl.classList.remove('hidden');
                        } else {
                            showEl.classList.add('hidden');
                        }
                    });
                }
            });
        });

        // Initialize x-show elements
        el.querySelectorAll('[x-show]').forEach(showEl => {
            showEl.classList.add('hidden');
        });

        // Handle click away
        document.addEventListener('click', (e) => {
            if (data.open && !el.contains(e.target)) {
                data.open = false;
                el.querySelectorAll('[x-show]').forEach(showEl => {
                    showEl.classList.add('hidden');
                });
            }
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const menuToggle = document.getElementById('menu-toggle');
    const closeMenu = document.getElementById('close-menu');
    const mobileMenu = document.getElementById('mobile-menu');
    const mobileBackdrop = document.getElementById('mobile-backdrop');

    menuToggle.addEventListener('click', function () {
        mobileMenu.classList.remove('hidden');
        document.body.classList.add('overflow-hidden');
    });

    closeMenu.addEventListener('click', function () {
        mobileMenu.classList.add('hidden');
        document.body.classList.remove('overflow-hidden');
    });

    mobileBackdrop.addEventListener('click', function () {
        mobileMenu.classList.add('hidden');
        document.body.classList.remove('overflow-hidden');
    });
});