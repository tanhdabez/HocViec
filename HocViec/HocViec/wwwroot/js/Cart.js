$(document).ready(function () {
    $.ajax({
        url: '/Cart/GetCartData',
        type: 'GET',
        success: function (data) {
            // Cập nhật nội dung của div#cart-data với dữ liệu JSON
            $('#cart-data').html(JSON.stringify(data));
        },
        error: function (error) {
            console.error("Error fetching cart data:", error);
        }
    });
    function updateStockMessage(input) {
        const stockQuantity = parseInt(input.data('stock-quantity'));
        const stockMessage = input.closest('td').find('.stock-message');
        if (stockQuantity <= 5 && stockQuantity > 0) {
            stockMessage.text(`Chỉ còn ${stockQuantity} sản phẩm.`).show();
        } else {
            stockMessage.hide();
        }
    }
    $('.quantity-input').each(function () {
        updateStockMessage($(this));
    });

     $('.quantity-increase').click(function () {
        const input = $(this).siblings('.quantity-input'); // Lấy input số lượng
        let quantity = parseInt(input.val());
        const stockQuantity = parseInt(input.data('stock-quantity')); // Lấy số lượng tồn kho
        const productId = input.data('product-id');
        if (quantity < stockQuantity) {
            input.val(quantity + 1);
            updateQuantity(productId, quantity + 1, $(this).closest('tr'));
        } else {
            alert('Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này.');
        }
    });

    $('.quantity-decrease').click(function () {
        const input = $(this).siblings('.quantity-input');
        let quantity = parseInt(input.val());
        const productId = input.data('product-id');
        if (quantity > 1) {
            input.val(quantity - 1);
            updateQuantity(productId, quantity - 1, $(this).closest('tr'));
        }
    });
    $('.quantity-input').on('change', function () {
        const input = $(this);
        let quantity = parseInt(input.val());
        const stockQuantity = parseInt(input.data('stock-quantity'));
        const productId = input.data('product-id');
        const errorMessage = $(this).closest('td').find('.error-message');

        if (quantity > stockQuantity) {
            alert('Số lượng bạn chọn đã vượt quá số lượng tồn kho.');
            input.val(stockQuantity); // Đặt lại giá trị về số lượng tồn kho
            updateQuantity(productId, stockQuantity, $(this).closest('tr'));
        } else if (quantity < 1) {
            alert('Số lượng không được nhỏ hơn 1.');
            input.val(1);
            updateQuantity(productId, 1, $(this).closest('tr'));
        } else {
            updateQuantity(productId, quantity, $(this).closest('tr'));
        }
    });
   
    $('#selectAll').change(function () {
        $('.product-checkbox').prop('checked', this.checked);
        calculateSelectedTotal();
    });

    $('.product-checkbox').change(function () {
        calculateSelectedTotal();
    });

    function calculateSelectedTotal() {
        let total = 0;
        $('.product-checkbox:checked').each(function () {
            const productId = $(this).data('product-id');
            const quantity = parseInt($(this).closest('tr').find('.quantity-input').val());
            const productTotalText = $(this).closest('tr').find('.product-total').text();
            // Xóa "VND" và khoảng trắng, sau đó chuyển thành số
            const productTotal = parseFloat(productTotalText.replace(/[^\d.-]/g, ''));
            total += productTotal;
        });
        $('#selectedTotal').text(total.toLocaleString());
    }

    function updateQuantity(productId, quantity, tr) {
        $.ajax({
            url: '/Cart/UpdateCartItem',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                const price = parseFloat(tr.find('.text-success').data('price'));
                const totalPrice = price * quantity;
                tr.find('.product-total').text(totalPrice.toLocaleString() + " VND"); // Cập nhật số tiền sản phẩm
                tr.find('.product-checkbox').data('quantity', quantity); // Cập nhật quantity trong data
                calculateSelectedTotal();
            },
            error: function (error) {
                toastr.error('Cập nhật thất bại', 'Error');
                console.error("Error updating quantity:", error);
            }
        });
    }

    $('.remove-from-cart').click(function () {
        const productId = $(this).data('product-id');
        $.ajax({
            url: '/Cart/RemoveFromCart',
            type: 'POST',
            data: { productId: productId },
            success: function (response) {
                location.reload();
            },
            error: function (error) {
                toastr.error('Xóa thất bại', 'Error');
                console.error("Error removing from cart:", error);
            }
        });
    });
    //function updateCartCount() {
    //    $.ajax({
    //        url: "/Cart/GetCartItemCount", // Gọi API lấy số lượng giỏ hàng
    //        type: "GET",
    //        success: function (response) {
    //            $("#cart-count").text(response.count); // Cập nhật số trên giỏ hàng
    //        },
    //        error: function () {
    //            console.error("Lỗi khi lấy số lượng giỏ hàng!");
    //        }
    //    });
    //}

    // Gọi hàm cập nhật ngay khi trang load
    $(document).ready(function () {
        updateCartCount();
    });
});