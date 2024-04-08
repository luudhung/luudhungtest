import validation from './validation.js'; // Nhập các chức năng kiểm tra từ tệp validation.js

// Lấy các phần tử DOM và thêm sự kiện submit
const formLogin = document.querySelector('.form-login-cus'); // Lấy form đăng nhập
const password = document.getElementById('password-cus'); // Lấy ô nhập mật khẩu
const email = document.getElementById('email-cus'); // Lấy ô nhập email

formLogin.addEventListener('submit', function (e) {
    e.preventDefault(); // Ngăn chặn hành động mặc định của form khi submit

    // Kiểm tra các trường dữ liệu để xác nhận chúng không được bỏ trống
    let checkEmpty = validation.checkRequired([email, password]);

    // Nếu các trường không bỏ trống, thực hiện việc submit form
    if (checkEmpty) formLogin.submit();
});
