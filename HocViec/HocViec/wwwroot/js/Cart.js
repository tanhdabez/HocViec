$(document).ready(function () {
    updateCartCount();
    function updateStockMessage(input) {
        const stockQuantity = parseInt(input.data('stock-quantity'));
        const stockMessage = input.closest('td').find('.stock-message');
        const row = input.closest('tr');
        const isOutOfStock = stockQuantity === 0;

        stockMessage.text(isOutOfStock ? `Sản phẩm đã hết hàng` : (stockQuantity <= 5 && stockQuantity > 0 ? `Chỉ còn ${stockQuantity} sản phẩm.` : '')).toggle(stockQuantity <= 5);
        row.find('.quantity-input, .quantity-decrease, .quantity-increase').prop('disabled', isOutOfStock);
        row.find('.product-checkbox').prop('disabled', isOutOfStock); // Disable checkbox
        row.toggleClass('out-of-stock', isOutOfStock); // Thêm/xóa lớp CSS
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
        $('.product-checkbox').each(function () {
            var row = $(this).closest('tr');
            if (!row.hasClass('out-of-stock')) {
                $(this).prop('checked', $('#selectAll').prop('checked'));
            }
        });
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

    // Xử lý sự kiện nút "Thanh toán"
    $('#checkoutButton').click(function (e) {
        e.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>

        var requests = [];

        $('.product-checkbox:checked').each(function () {
            var productId = $(this).data('product-id');
            var quantity = parseInt($(this).closest('tr').find('.quantity-input').val());

            requests.push({
                SanPhamId: productId,
                SoLuong: quantity
            });
        });

        if (requests.length > 0) {
            // Chuyển hướng đến trang thanh toán với danh sách sản phẩm, số lượng
            var jsonRequests = JSON.stringify(requests);
            window.location.href = '/Checkout/CheckOut?requests=' + encodeURIComponent(jsonRequests);
        } else {
            toastr.error('Vui lòng chọn sản phẩm để thanh toán.');
        }
    });

});