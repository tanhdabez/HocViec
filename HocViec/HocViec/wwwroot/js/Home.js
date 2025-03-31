$(document).ready(function () {
    $('.add-to-cart').click(function () {
        const productId = $(this).data('product-id');
        const quantity = 1;
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                console.log(response);
                if (response.success) {
                    toastr.success(response.message, "Thành công");
                    updateCartCount();
                }
                else if (response.warning) {
                    toastr.error(response.message, "Cảnh Báo");
                    updateCartCount();
                }
                else {
                    toastr.warning(response.message, "Lỗi");
                }
            },
            error: function (error) {
                toastr.error('Đã xảy ra lỗi khi kết nối đến server.', 'Lỗi');
                console.error("Error adding to cart:", error);
            }
        });
    });
});
