$(document).ready(function () {
    let pageSize = 10;
    function loadPage(pageNumber, pageSize) {
        $.ajax({
            url: '/HoaDon/GetHoaDonPage',
            type: 'GET',
            data: {
                pageNumber: pageNumber,
                pageSize: pageSize,
                startDate: $('#startDate').val(),
                endDate: $('#endDate').val(),
                trangThai: $('#statusFilter').val(),
                maHD: $('#invoiceCode').val()
            },
            success: function (data) {
                $('#hoaDonTableContainer').html(data);
            },
            error: function (error) {
                console.error('Lỗi khi tải trang:', error);
            }
        });
    }
    window.loadPage = loadPage;

    $('.hoa-don-row').click(function () {
        let hoaDonId = $(this).data('id');
        $.ajax({
            url: `/HoaDon/GetHoaDonDetails/${hoaDonId}`,
            type: 'GET',
            success: function (data) {
                $('#hoaDonModalBody').html(data);
                $('#hoaDonModal').modal('show');
            },
            error: function () {
                alert('Lỗi khi tải dữ liệu chi tiết hóa đơn.');
            }
        });
    });

    $('#hoaDonModal').on('hidden.bs.modal', function () {
        $(this).find('.modal-body').empty();
    });

    $('.pagination').on('click', '.page-link', function () {
        let pageNumber = $(this).data('page-number');
        if (pageNumber) {
            loadPage(pageNumber);
        }
    });
});