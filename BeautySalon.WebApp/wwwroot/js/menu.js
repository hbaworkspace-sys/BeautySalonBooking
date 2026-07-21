// menu.js
window.menuFunctions = {
    initialize: function () {
        console.log('Menu system initialized');
        this.setActiveMenuByUrl();
    },

    removeAllActiveClasses: function () {
        document.querySelectorAll('.menu-link').forEach(link => {
            link.classList.remove('active');
        });
    },

    setActiveMenuByUrl: function () {
        const currentPath = window.location.pathname;
        document.querySelectorAll('.menu-link').forEach(link => {
            const href = link.getAttribute('href');
            if (href && currentPath === href) {
                link.classList.add('active');
                const parentGroup = link.closest('.menu-children');
                if (parentGroup) {
                    parentGroup.classList.add('open');
                    const arrow = parentGroup.previousElementSibling?.querySelector('.arrow');
                    if (arrow) arrow.classList.add('open');
                }
            }
        });
    },

    addActiveClass: function (elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.classList.add('active');
        }
    }
};

// اجرای خودکار هنگام لود صفحه
document.addEventListener('DOMContentLoaded', function () {
    window.menuFunctions.initialize();
});