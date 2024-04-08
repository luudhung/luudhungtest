const fullname = document.getElementById('fullname');
const email = document.getElementById('email');
const phone = document.getElementById('phone');
const address = document.getElementById('address');

const city = document.getElementById('province'); //thanh pho
const district = document.getElementById('district'); //quan/huyen
const ward = document.getElementById('village');//

fetch('/data.json')
    .then((response) => response.json())
    .then((data) => renderCity(data));

function renderCity(data) {
    for (var item of data) {
        // khoi tao ra doi tuong cac tinh thanh pho
        city.options[city.options.length] = new Option(item.Name, item.Id);
    }

    // xu ly khi thay doi tinh thanh thi se hien thi ra quan huyen thuoc tinh thanh do
    city.onchange = () => {
        district.length = 1;

        console.log(city.value);
        // kiem tra gia tri value xem co rong khong, neu rong thi khong thuc hien render cac quan ra
        if (city.value != '') {
            // loc ra du lieu khi nguoi dung chon tinh thanh pho
            const result = data.filter((n) => n.Id === city.value);
            // nguyen nhan result[0].District
            // giai thich :
            // sau khi loc ta loc du lieu result xong thi ket qua no se tra ve mot mang
            // trong mang do chua mot doi tuong [{}]
            // tuc la phai doi tuong minh goi trong dau ? dang o index = 0 thi minh phai goi no ra
            // la
            //   result[0] tuc la luc nay no ra  object{} tuc trong object minh goi den thuoc tinh la DISTRICTS
            //     => result[0].Districts
            for (var item of result[0].Districts) {
                district.options[district.options.length] = new Option(
                    item.Name,
                    item.Id
                );
            }
        } else {
            // do nothing
        }
    };

    district.onchange = () => {
        ward.length = 1;
        const result = data.filter((el) => el.Id === city.value);
        if (district.value != ' ') {
            // lay du lieu quan ve trong du lieu quan chi ton tai trong duong
            const resultDistrict = result[0].Districts.filter(
                (el) => el.Id === district.value
            );

            for (var item of resultDistrict[0].Wards) {
                ward.options[ward.options.length] = new Option(item.Name, item.id);
            }
        }
    };
}

//submit form
import validation from './validation.js';

const listCart = JSON.parse(window.localStorage.getItem('cart'));
if (!listCart || listCart.length == 0) {
    window.location.href = '/';
}

function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}
function GenerateId() {
    const d = new Date();
    let ms = d.getTime();
    return ms;
}

$('#save_btn').click(() => {
    let checkEmpty = validation.checkRequired([fullname, email, phone, address]);
    let checkEmailInvalid = validation.checkEmail(email);
    let checkPhoneInvalid = validation.checkNumberPhone(phone);
    let checkAddressInvalid = validation.checkAddress([city, district, ward]);

    if (
        checkEmpty &&
        checkEmailInvalid &&
        checkPhoneInvalid &&
        !checkAddressInvalid
    ) {
        var dsChiTietDH = [];
        const idBill = "DH" + GenerateId();
        let total = 0;
        let totalQty = 0;
        listCart.forEach((el) => {
            let idDetail = uuidv4();
            let converNumberAmount = Number(el.amount);
            let intoMoney = el.price * converNumberAmount;
            totalQty += Number(el.amount);
            total += intoMoney;
            var ctdh = {
                idDetailBill: idDetail, idProduct: el.idFood, idBill: idBill, qty: converNumberAmount, intoMoney: intoMoney
            };
            dsChiTietDH.push(ctdh);
        });

        const idUser = null;
        const customData = {
            idBill: idBill,
            idUser: idUser !== null ? idUser : null,
            Shipping: 50,
            Total: total,
            PTTT: "Tien Mat",
            status: 0,
            detailBill: dsChiTietDH
        };

        let thanhpho = $("#province option:selected").text();
        let quan = $("#village option:selected").text();
        let phuong = $("#district option:selected").text();
        let diachi = address.value + ', ' + quan + ', ' + phuong + ', ' + thanhpho;
        let dienthoai = Number(phone.value);

        $.ajax('/Bill/PostBill', {
            data: {
                idBill: idBill,
                idUser: idUser !== null ? idUser : null,
                Shipping: 50,
                Total: total,
                totalQty: totalQty,
                nameBook: fullname.value,
                email: email.value,
                phone: dienthoai,
                address: diachi,
                PTTT: "Tien Mat",
                detailBill: dsChiTietDH,
                status: false,
            },
            dataType: 'json',
            method: 'Post',
            success: function (res) {
                alert(res);
                window.localStorage.removeItem('cart');
                window.location.replace('/home');
            }
        });
    }
});
