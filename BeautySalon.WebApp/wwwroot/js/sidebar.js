let activeMenu = null;

function toggleMenu(element, event) {
    // جلوگیری از انتشار رویداد به لینک‌های فرزند
    event.stopPropagation();

    // پیدا کردن منوی فرزند (بعد از .menu-header)
    const parentGroup = element.closest('.menu-group');
    if (!parentGroup) return;

    const children = parentGroup.querySelector('.menu-children');
    const arrow = element.querySelector('.arrow');

    if (children) {
        if (children.classList.contains('open')) {
            children.classList.remove('open');
            if (arrow) arrow.classList.remove('open');
        } else {
            children.classList.add('open');
            if (arrow) arrow.classList.add('open');
        }
    }
}

function setActiveMenu(element) {
    // حذف active از همه منوها
    document.querySelectorAll('.menu-link').forEach(link => {
        link.classList.remove('active');
    });
    document.querySelectorAll('.menu-header').forEach(header => {
        header.classList.remove('active');
    });

    // اضافه کردن active به منوی کلیک شده
    element.classList.add('active');

    // اگر منوی فرزند است، active را به والد هم اضافه کنید
    if (element.classList.contains('child')) {
        const parentGroup = element.closest('.menu-group');
        if (parentGroup) {
            const parentHeader = parentGroup.querySelector('.menu-header');
            if (parentHeader) {
                parentHeader.classList.add('active');
            }
        }
    }
}