var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent()
    },
    registerEvent: function () {
        $('#frmCheckout').validate({
            rules: {
                txtName: "required",
                txtAddress: "required",
                txtEmail: {
                    required: true,
                    email : true
                },
                txtPhone: {
                    required: true,
                    number: true
                },
                txtMessage : "required"
            },
            messages: {
                txtName: "Vui lòng nhập tên!",
                txtAddress: "Vui lòng nhập địa chỉ!",
                txtEmail: {
                    required: "Vui lòng nhập email",
                    email: "Email không đúng định dạng"
                },
                txtPhone: {
                    required: "Vui lòng nhập số điện thoại!",
                    number: "Số điện thoại không đúng!"
                },
                txtMessage: "Vui lòng nhập ghi chú!"
            }
        });
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.qty-dec-btn').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            var productPrice = parseInt($(this).data('price'));
            var quantity = parseInt($('.qty-input-' + productId).val());
            if (quantity - 1 > 0) {
                quantity -= 1;
                var amount = quantity * productPrice;
                $('.qty-input-' + productId).val(quantity);
                $('.amount-' + productId).text(numeral(amount).format('0,0'));
            }
            $('#totalAmount').text(cart.getTotalOrder());
            cart.updateAll();
        });
        $('.qty-inc-btn').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            var productPrice = parseInt($(this).data('price'));
            var quantity = parseInt($('.qty-input-' + productId).val());
            quantity += 1;
            var amount = quantity * productPrice;
            $('.qty-input-' + productId).val(quantity);
            $('.amount-' + productId).text(numeral(amount).format('0,0'));
            $('#totalAmount').text(cart.getTotalOrder());
            cart.updateAll();
        });
        // Delete All
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll()
        });
        // Buy Continue
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = '/';
        });
        // Update Cart
        $('#btnUpdateCart').off('click').on('click', function (e) {
            e.preventDefault();
            cart.updateAll();
        });
        // Check Out
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#checkout').show();
        });
        // Using info user to checkout after login
        $('#chkUserLoginInfo').off('click').on('click', function (e) {
            if (e.target.checked) {
                cart.getLoginUser();
            }
        }); 
        $('#btnFinishCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmCheckout').valid();
            if (isValid) {
                cart.createOrder();
            }
        })
    },
    getLoginUser : function(){
        $.ajax({
            url: '/Cart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var user = res.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder : function(){
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            PaymentStatus: "Thanh toán tiền mặt",
            Status: false
        }
        $.ajax({
            url: '/Cart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (res) {
                if (res.status) {
                    $('#checkout').hide();
                    cart.deleteAll(true);
                    $('.cart').html('<div class="alert alert-success">Bạn đã đặt hàng thành công! chúng tôi sẽ liên hệ trong thời gian sớm nhất</div>');
                }
            }
        });
    },
    updateAll : function(){
        var cartList = [];
        $.each($('.qty-input'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/Cart/Update',
            type: 'POST',
            data: {
                cartJsonData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    // alert('Cập nhật giỏ hàng thành công');
                }
            }
        });
    },
    getTotalOrder: function(){
        var list = $('.qty-input');
        var total = 0;
        $.each(list, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return numeral(total).format('0,0');
    },
    deleteAll : function(checked){
        $.ajax({
            url: '/Cart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData(checked);
                }
            }
        });
    },
    addItem: function(productId){
        $.ajax({
            url: '/Cart/Add',
            type: 'POST',
            data: {
                productId: productId
            },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    alert('Thêm sản phẩm thành công');
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/Cart/DeleteItem',
            type: 'POST',
            data: {
                productId: productId
            },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    loadData: function (checked) {
        $.ajax({
            url: '/Cart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        var price = item.Product.PromotionPrice > 0 ? item.Product.PromotionPrice : item.Price;
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            ProductImage: item.Product.ThumbnailImage,
                            ProductPrice: price,
                            ProductPriceF: numeral(price).format('0,0'),
                            ProductQuantity: item.Quantity,
                            ProductTotal: numeral(price * item.Quantity).format('0,0')
                        });
                    });
                    if (html == '' && !checked) {
                        $('.cart').html('<div class="alert alert-danger">Chưa có sản phẩm trong giỏ hàng</div>');
                    }
                    $('#cartBody').html(html);
                    $('#totalAmount').text(cart.getTotalOrder()); 
                    $('#totalAmountTwo').text(cart.getTotalOrder());
                    cart.registerEvent();
                }
            }
        })
    }
}
cart.init();