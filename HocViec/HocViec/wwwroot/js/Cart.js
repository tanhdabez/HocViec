$(document).ready(function () {
    $('.quantity-increase').click(function () {
        let quantity = parseInt($('#productQuantity').val());
        $('#productQuantity').val(quantity + 1);
    });

    $('.quantity-decrease').click(function () {
        let quantity = parseInt($('#productQuantity').val());
        if (quantity > 1) {
            $('#productQuantity').val(quantity - 1);
        }
    });

    $('#addToCartButton').off('click').on('click', function () {
        const productId = $(this).data('product-id');
        const quantity = parseInt($('#productQuantity').val());

        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                toastr.success("Thêm thành công", "Success");
            },
            error: function (error) {
                toastr.error('Thêm thất bại', 'Error');
                console.error("Error adding to cart:", error);
            }
        });
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
            const price = parseFloat($(this).closest('tr').find('.text-success').data('price'));
            total += price * quantity;
        });
        $('#selectedTotal').text(total.toLocaleString());
    }

    $(document).off('click', '.quantity-increase').on('click', '.quantity-increase', function () {
        const productId = $(this).data('product-id');
        let quantity = parseInt($(this).closest('tr').find('.quantity-input').val());
        quantity++;
        $(this).closest('tr').find('.quantity-input').val(quantity);
        updateQuantity(productId, quantity, $(this).closest('tr'));
    });
    $(document).off('click', '.quantity-decrease').on('click', '.quantity-decrease', function () {
        const productId = $(this).data('product-id');
        let quantity = parseInt($(this).closest('tr').find('.quantity-input').val());
        if (quantity > 1) {
            quantity--;
            $(this).closest('tr').find('.quantity-input').val(quantity);
            updateQuantity(productId, quantity, $(this).closest('tr'));
        }
    });

    function updateQuantity(productId, quantity, tr) {
        $.ajax({
            url: '/Cart/UpdateCartItem',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                const price = parseFloat(tr.find('.text-success').data('price'));
                const totalPrice = price * quantity;
                tr.find('.text-danger').text(totalPrice.toLocaleString() + " VND"); // Cập nhật số tiền sản phẩm
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
    function updateCartCount() {
        $.ajax({
            url: "/Cart/GetCartItemCount", // Gọi API lấy số lượng giỏ hàng
            type: "GET",
            success: function (response) {
                $("#cart-count").text(response.count); // Cập nhật số trên giỏ hàng
                console.log("Error removing from cart:", response.count);
            },
            error: function () {
                console.error("Lỗi khi lấy số lượng giỏ hàng!");
            }
        });
    }

    // Gọi hàm cập nhật ngay khi trang load
    $(document).ready(function () {
        updateCartCount();
    });
});