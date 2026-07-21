window.otpManager = {
    init: function () {
        const inputs = document.querySelectorAll('.otp-input');

        inputs.forEach((input, index) => {
            // رویداد ورودی
            input.addEventListener('input', function (e) {
                const value = this.value;
                if (value.length > 1) {
                    this.value = value.slice(0, 1);
                }

                // فقط اعداد مجاز
                if (this.value && !/^\d$/.test(this.value)) {
                    this.value = '';
                    return;
                }

                // حرکت به باکس بعدی
                if (this.value && index < 5) {
                    inputs[index + 1].focus();
                }

                // بررسی کامل بودن
                window.otpManager.checkComplete();
            });

            // رویداد کیبورد
            input.addEventListener('keydown', function (e) {
                // Backspace
                if (e.key === 'Backspace' && !this.value && index > 0) {
                    inputs[index - 1].focus();
                    inputs[index - 1].select();
                }

                // Arrow Left
                if (e.key === 'ArrowLeft' && index > 0) {
                    inputs[index - 1].focus();
                    e.preventDefault();
                }

                // Arrow Right
                if (e.key === 'ArrowRight' && index < 5) {
                    inputs[index + 1].focus();
                    e.preventDefault();
                }

                // Delete
                if (e.key === 'Delete' && this.value) {
                    this.value = '';
                    window.otpManager.checkComplete();
                }

                // Enter
                if (e.key === 'Enter') {
                    window.otpManager.verifyOtp();
                }
            });

            // رویداد کلیک
            input.addEventListener('click', function () {
                this.select();
            });

            // رویداد فوکوس
            input.addEventListener('focus', function () {
                this.select();
            });
        });
    },

    checkComplete: function () {
        const inputs = document.querySelectorAll('.otp-input');
        let complete = true;
        let code = '';

        inputs.forEach(input => {
            if (!input.value) {
                complete = false;
            }
            code += input.value || '';
        });

        // به‌روزرسانی وضعیت در Blazor
        DotNet.invokeMethodAsync('BeautySalonBooking.WebApp', 'UpdateOtpStatus', complete, code);

        return complete;
    },

    getOtpCode: function () {
        const inputs = document.querySelectorAll('.otp-input');
        let code = '';
        inputs.forEach(input => {
            code += input.value || '';
        });
        return code;
    },

    clearOtp: function () {
        const inputs = document.querySelectorAll('.otp-input');
        inputs.forEach(input => {
            input.value = '';
        });
        inputs[0].focus();
        window.otpManager.checkComplete();
    },

    verifyOtp: function () {
        const code = window.otpManager.getOtpCode();
        if (code.length === 6) {
            DotNet.invokeMethodAsync('BeautySalonBooking.WebApp', 'VerifyOtp', code);
        }
    },

    focusFirst: function () {
        const inputs = document.querySelectorAll('.otp-input');
        if (inputs.length > 0) {
            inputs[0].focus();
        }
    }
};

// اجرای خودکار هنگام لود صفحه
document.addEventListener('DOMContentLoaded', function () {
    if (document.querySelector('.otp-input')) {
        window.otpManager.init();
        window.otpManager.focusFirst();
    }
});