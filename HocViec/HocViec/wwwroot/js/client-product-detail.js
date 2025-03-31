$(document).ready(function () {
    $('#addToCartButton').on('click', function () {
        const productId = $(this).data('product-id');
        const quantity = parseInt($('#productQuantity').val());
        if (quantity <= 0) {
            toastr.error("Số lượng không hợp lệ.", "Lỗi");
            return;
        }
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message, "Thành công");
                    updateCartCount();
                }
                else if (response.warning) {
                    toastr.error(response.message, "Cảnh Báo");
                    updateCartCount();
                } else {
                    toastr.error(response.message, "Lỗi");
                }
            },
            error: function (error) {
                toastr.error('Đã xảy ra lỗi khi kết nối đến server.', 'Lỗi');
                console.error("Error adding to cart:", error);
            }
        });
    });

    // Lấy giá trị maxQuantity từ thuộc tính data-max-quantity
    const maxQuantity = parseInt($('#productDetails').data('max-quantity'));

    // Xử lý nút tăng
    $('.quantity-increase').click(function () {
        let quantityInput = $('#productQuantity');
        let currentQuantity = parseInt(quantityInput.val());
        if (currentQuantity < maxQuantity) {
            quantityInput.val(currentQuantity + 1);
        } else {
            toastr.error('Số lượng sản phẩm đã đạt tối đa.');
        }
    });

    // Xử lý nút giảm
    $('.quantity-decrease').click(function () {
        let quantityInput = $('#productQuantity');
        let currentQuantity = parseInt(quantityInput.val());
        if (currentQuantity > 1) {
            quantityInput.val(currentQuantity - 1);
        } else {
            toastr.error('Số lượng sản phẩm không thể nhỏ hơn 1.');
        }
    });

    // Xử lý khi người dùng nhập số lượng trực tiếp
    $('#productQuantity').on('input', function () {
        let enteredQuantity = parseInt($(this).val());
        if (enteredQuantity > maxQuantity) {
            $(this).val(maxQuantity);
            toastr.error('Số lượng sản phẩm không thể vượt quá ' + maxQuantity);
        } else if (enteredQuantity < 1) {
            $(this).val(1);
            toastr.error('Số lượng sản phẩm không thể nhỏ hơn 1.');
        }
    });
});